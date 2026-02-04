// Copyright © 2024 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CefSharp.Example.CommercialProxy
{
    /// <summary>
    /// Commercial proxy service client for services like Siyetian (思叶天), Zhima, Kuai, etc.
    /// This example uses Siyetian API format as reference.
    /// </summary>
    public class CommercialProxyClient
    {
        private readonly string _apiUrl;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiUrl">The API URL to fetch proxies from (e.g., https://api.siyetian.com/get?type=http&num=1&key=YOUR_KEY)</param>
        public CommercialProxyClient(string apiUrl)
        {
            _apiUrl = apiUrl ?? throw new ArgumentNullException(nameof(apiUrl));
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        /// <summary>
        /// Fetch a proxy from the API
        /// </summary>
        /// <returns>Proxy address or null if failed</returns>
        public async Task<ProxyAddress> GetProxyAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync(_apiUrl);

                // Try to parse as JSON first
                if (response.TrimStart().StartsWith("{") || response.TrimStart().StartsWith("["))
                {
                    return ParseJsonResponse(response);
                }
                else
                {
                    // Plain text format: IP:PORT
                    return ParseTextResponse(response);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to fetch proxy: {ex.Message}");
                return null;
            }
        }

        private ProxyAddress ParseJsonResponse(string response)
        {
            try
            {
                // Try parsing as single object
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var proxyData = JsonSerializer.Deserialize<ProxyApiResponse>(response, options);

                if (proxyData != null && !string.IsNullOrEmpty(proxyData.Ip))
                {
                    return new ProxyAddress
                    {
                        Host = proxyData.Ip,
                        Port = proxyData.Port > 0 ? proxyData.Port : 8080,
                        Scheme = "http",
                        ExpireTime = ParseExpireTime(proxyData.Expire)
                    };
                }

                // Try parsing as array
                var proxyArray = JsonSerializer.Deserialize<List<ProxyApiResponse>>(response, options);
                if (proxyArray != null && proxyArray.Any())
                {
                    var first = proxyArray.First();
                    return new ProxyAddress
                    {
                        Host = first.Ip,
                        Port = first.Port > 0 ? first.Port : 8080,
                        Scheme = "http",
                        ExpireTime = ParseExpireTime(first.Expire)
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to parse JSON response: {ex.Message}");
            }

            return null;
        }

        private ProxyAddress ParseTextResponse(string response)
        {
            try
            {
                // Format: IP:PORT or IP:PORT:EXPIRE
                var lines = response.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length == 0)
                    return null;

                var parts = lines[0].Split(':');
                if (parts.Length >= 2)
                {
                    return new ProxyAddress
                    {
                        Host = parts[0].Trim(),
                        Port = int.TryParse(parts[1].Trim(), out int port) ? port : 8080,
                        Scheme = "http",
                        ExpireTime = parts.Length >= 3 ? ParseExpireTime(parts[2].Trim()) : null
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to parse text response: {ex.Message}");
            }

            return null;
        }

        private DateTime? ParseExpireTime(string expireStr)
        {
            if (string.IsNullOrEmpty(expireStr))
                return null;

            // Try parsing as timestamp (seconds)
            if (long.TryParse(expireStr, out long timestamp))
            {
                return DateTimeOffset.FromUnixTimeSeconds(timestamp).DateTime;
            }

            // Try parsing as datetime
            if (DateTime.TryParse(expireStr, out DateTime dateTime))
            {
                return dateTime;
            }

            return null;
        }

        /// <summary>
        /// Fetch multiple proxies from the API
        /// </summary>
        public async Task<List<ProxyAddress>> GetProxiesAsync(int count)
        {
            var proxies = new List<ProxyAddress>();

            for (int i = 0; i < count; i++)
            {
                var proxy = await GetProxyAsync();
                if (proxy != null)
                {
                    proxies.Add(proxy);
                }

                // Delay to avoid rate limiting
                if (i < count - 1)
                {
                    await Task.Delay(1000);
                }
            }

            return proxies;
        }
    }

    /// <summary>
    /// API response model for commercial proxy services
    /// </summary>
    public class ProxyApiResponse
    {
        [JsonPropertyName("ip")]
        public string Ip { get; set; }

        [JsonPropertyName("port")]
        public int Port { get; set; }

        [JsonPropertyName("expire_time")]
        public string Expire { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("province")]
        public string Province { get; set; }

        [JsonPropertyName("isp")]
        public string Isp { get; set; }
    }

    /// <summary>
    /// Proxy address information
    /// </summary>
    public class ProxyAddress
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Scheme { get; set; } = "http";
        public DateTime? ExpireTime { get; set; }
        public string City { get; set; }
        public string Province { get; set; }

        public bool IsExpired
        {
            get
            {
                return ExpireTime.HasValue && ExpireTime.Value <= DateTime.Now;
            }
        }

        public override string ToString()
        {
            return $"{Scheme}://{Host}:{Port}";
        }
    }

    /// <summary>
    /// Commercial proxy rotator with automatic refresh
    /// </summary>
    public class CommercialProxyRotator
    {
        private List<ProxyAddress> _proxies = new List<ProxyAddress>();
        private int _currentIndex = 0;
        private readonly CommercialProxyClient _client;
        private readonly object _lock = new object();

        public CommercialProxyRotator(string apiUrl)
        {
            _client = new CommercialProxyClient(apiUrl);
        }

        /// <summary>
        /// Load proxies from the API
        /// </summary>
        public async Task<bool> LoadProxiesAsync(int count = 5)
        {
            var proxies = await _client.GetProxiesAsync(count);

            lock (_lock)
            {
                _proxies.Clear();
                _proxies.AddRange(proxies.Where(p => p != null));
                _currentIndex = 0;
            }

            return _proxies.Any();
        }

        /// <summary>
        /// Get the next available proxy
        /// </summary>
        public ProxyAddress GetNextProxy()
        {
            lock (_lock)
            {
                if (!_proxies.Any())
                    return null;

                // Skip expired proxies
                var availableProxies = _proxies.Where(p => !p.IsExpired).ToList();

                if (!availableProxies.Any())
                {
                    // All proxies expired, return null to trigger refresh
                    return null;
                }

                _currentIndex = _currentIndex % availableProxies.Count;
                var proxy = availableProxies[_currentIndex];
                _currentIndex++;

                return proxy;
            }
        }

        /// <summary>
        /// Refresh a specific proxy or add a new one
        /// </summary>
        public async Task<bool> RefreshProxyAsync(ProxyAddress oldProxy)
        {
            var newProxy = await _client.GetProxyAsync();

            if (newProxy != null)
            {
                lock (_lock)
                {
                    if (oldProxy != null)
                    {
                        var index = _proxies.FindIndex(p => p.Host == oldProxy.Host && p.Port == oldProxy.Port);
                        if (index >= 0)
                        {
                            _proxies[index] = newProxy;
                        }
                        else
                        {
                            _proxies.Add(newProxy);
                        }
                    }
                    else
                    {
                        _proxies.Add(newProxy);
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Get count of loaded proxies
        /// </summary>
        public int ProxyCount
        {
            get
            {
                lock (_lock)
                {
                    return _proxies.Count;
                }
            }
        }

        /// <summary>
        /// Get count of non-expired proxies
        /// </summary>
        public int AvailableProxyCount
        {
            get
            {
                lock (_lock)
                {
                    return _proxies.Count(p => !p.IsExpired);
                }
            }
        }
    }
}
