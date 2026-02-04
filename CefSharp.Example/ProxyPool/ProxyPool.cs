// Copyright © 2024 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CefSharp.Example.ProxyPool
{
    /// <summary>
    /// Proxy server information
    /// </summary>
    public class ProxyInfo
    {
        /// <summary>
        /// Proxy host address
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Proxy port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Proxy scheme (http, socks, socks4, socks5)
        /// </summary>
        public string Scheme { get; set; } = "http";

        /// <summary>
        /// Username for proxy authentication
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password for proxy authentication
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Number of consecutive failures
        /// </summary>
        public int FailureCount { get; set; }

        /// <summary>
        /// Last time this proxy was used
        /// </summary>
        public DateTime LastUsed { get; set; }

        /// <summary>
        /// Last time this proxy was health checked
        /// </summary>
        public DateTime LastChecked { get; set; }

        /// <summary>
        /// Whether this proxy is currently healthy
        /// </summary>
        public bool IsHealthy { get; set; } = true;

        /// <summary>
        /// Gets the proxy URL
        /// </summary>
        public string GetProxyUrl()
        {
            return $"{Scheme}://{Host}:{Port}";
        }

        /// <summary>
        /// String representation of the proxy
        /// </summary>
        public override string ToString()
        {
            return $"{Scheme}://{Host}:{Port}";
        }

        /// <summary>
        /// Equality comparison
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj is ProxyInfo other)
            {
                return Host == other.Host && Port == other.Port && Scheme == other.Scheme;
            }
            return false;
        }

        /// <summary>
        /// Hash code
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + (Host?.GetHashCode() ?? 0);
                hash = hash * 31 + Port.GetHashCode();
                hash = hash * 31 + (Scheme?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }

    /// <summary>
    /// Manages a pool of proxy servers with rotation and health checking capabilities
    /// </summary>
    public class ProxyPool : IDisposable
    {
        private List<ProxyInfo> _proxies = new List<ProxyInfo>();
        private int _currentIndex = 0;
        private readonly object _lock = new object();
        private readonly Random _random = new Random();
        private Timer _healthCheckTimer;

        /// <summary>
        /// Proxy rotation mode
        /// </summary>
        public enum RotationMode
        {
            /// <summary>Sequential rotation through proxy list</summary>
            Sequential,
            /// <summary>Random proxy selection</summary>
            Random,
            /// <summary>Select least recently used proxy</summary>
            LeastUsed,
            /// <summary>Select healthiest proxy (lowest failure count)</summary>
            HealthBased
        }

        /// <summary>
        /// Current rotation mode
        /// </summary>
        public RotationMode Mode { get; set; } = RotationMode.Sequential;

        /// <summary>
        /// Maximum failure count before marking proxy as unhealthy
        /// </summary>
        public int MaxFailureCount { get; set; } = 3;

        /// <summary>
        /// Whether to enable automatic health checking
        /// </summary>
        public bool EnableHealthCheck { get; set; } = true;

        /// <summary>
        /// Health check interval in milliseconds
        /// </summary>
        public int HealthCheckInterval { get; set; } = 60000; // 1 minute

        /// <summary>
        /// URL to use for health checks
        /// </summary>
        public string HealthCheckUrl { get; set; } = "http://www.gstatic.com/generate_204";

        /// <summary>
        /// Timeout for health checks in seconds
        /// </summary>
        public int HealthCheckTimeout { get; set; } = 10;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProxyPool()
        {
            // Start health check timer
            _healthCheckTimer = new Timer(
                async _ =>
                {
                    try
                    {
                        await PerformHealthCheckAsync();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Health check error: {ex.Message}");
                    }
                },
                null,
                HealthCheckInterval,
                HealthCheckInterval
            );
        }

        /// <summary>
        /// Add a proxy to the pool
        /// </summary>
        public void AddProxy(string host, int port, string scheme = "http",
            string username = null, string password = null)
        {
            lock (_lock)
            {
                var proxy = new ProxyInfo
                {
                    Host = host,
                    Port = port,
                    Scheme = scheme,
                    Username = username,
                    Password = password,
                    LastChecked = DateTime.Now
                };

                // Avoid duplicates
                if (!_proxies.Contains(proxy))
                {
                    _proxies.Add(proxy);
                }
            }
        }

        /// <summary>
        /// Add multiple proxies to the pool
        /// </summary>
        public void AddProxies(IEnumerable<ProxyInfo> proxies)
        {
            lock (_lock)
            {
                foreach (var proxy in proxies)
                {
                    if (!_proxies.Contains(proxy))
                    {
                        _proxies.Add(proxy);
                    }
                }
            }
        }

        /// <summary>
        /// Load proxies from a file
        /// Format: host:port or scheme://host:port or scheme://username:password@host:port
        /// Lines starting with # are comments
        /// </summary>
        public void LoadFromFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new System.IO.FileNotFoundException("Proxy file not found", filePath);
            }

            var lines = System.IO.File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("#"))
                    continue;

                try
                {
                    var proxy = ParseProxyString(line.Trim());
                    if (proxy != null)
                    {
                        AddProxies(new[] { proxy });
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Failed to parse proxy line '{line}': {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Parse proxy string into ProxyInfo object
        /// </summary>
        private ProxyInfo ParseProxyString(string proxyString)
        {
            var proxy = new ProxyInfo();

            // Handle scheme://username:password@host:port format
            if (proxyString.Contains("://"))
            {
                var parts = proxyString.Split(new[] { "://" }, StringSplitOptions.None);
                proxy.Scheme = parts[0].ToLower();
                proxyString = parts[1];
            }

            // Handle authentication
            if (proxyString.Contains("@"))
            {
                var authParts = proxyString.Split('@');
                var credentials = authParts[0].Split(':');
                proxy.Username = credentials[0];
                proxy.Password = credentials.Length > 1 ? credentials[1] : "";
                proxyString = authParts[1];
            }

            // Handle host:port
            var hostPort = proxyString.Split(':');
            if (hostPort.Length != 2)
            {
                throw new FormatException($"Invalid proxy format: {proxyString}");
            }

            proxy.Host = hostPort[0];
            proxy.Port = int.Parse(hostPort[1]);

            return proxy;
        }

        /// <summary>
        /// Get the next available proxy from the pool
        /// </summary>
        public ProxyInfo GetNextProxy()
        {
            lock (_lock)
            {
                if (_proxies.Count == 0)
                    return null;

                // Filter to healthy proxies
                var healthyProxies = _proxies.Where(p => p.IsHealthy).ToList();
                if (healthyProxies.Count == 0)
                {
                    // If no healthy proxies, reset all and try again
                    foreach (var proxy in _proxies)
                    {
                        proxy.IsHealthy = true;
                        proxy.FailureCount = 0;
                    }
                    healthyProxies = _proxies.ToList();
                }

                ProxyInfo selectedProxy;

                switch (Mode)
                {
                    case RotationMode.Random:
                        selectedProxy = healthyProxies[_random.Next(healthyProxies.Count)];
                        break;

                    case RotationMode.LeastUsed:
                        // Find proxy with earliest LastUsed time
                        selectedProxy = healthyProxies[0];
                        for (int i = 1; i < healthyProxies.Count; i++)
                        {
                            if (healthyProxies[i].LastUsed < selectedProxy.LastUsed)
                            {
                                selectedProxy = healthyProxies[i];
                            }
                        }
                        break;

                    case RotationMode.HealthBased:
                        // Find proxy with lowest failure count
                        selectedProxy = healthyProxies[0];
                        for (int i = 1; i < healthyProxies.Count; i++)
                        {
                            if (healthyProxies[i].FailureCount < selectedProxy.FailureCount)
                            {
                                selectedProxy = healthyProxies[i];
                            }
                        }
                        break;

                    case RotationMode.Sequential:
                    default:
                        _currentIndex = _currentIndex % healthyProxies.Count;
                        selectedProxy = healthyProxies[_currentIndex];
                        _currentIndex++;
                        break;
                }

                selectedProxy.LastUsed = DateTime.Now;
                return selectedProxy;
            }
        }

        /// <summary>
        /// Report a proxy failure
        /// </summary>
        public void ReportFailure(ProxyInfo proxy)
        {
            if (proxy == null)
                return;

            lock (_lock)
            {
                var targetProxy = _proxies.FirstOrDefault(p => p.Equals(proxy));

                if (targetProxy != null)
                {
                    targetProxy.FailureCount++;

                    if (targetProxy.FailureCount >= MaxFailureCount)
                    {
                        targetProxy.IsHealthy = false;
                        System.Diagnostics.Debug.WriteLine($"Proxy {targetProxy} marked as unhealthy");
                    }
                }
            }
        }

        /// <summary>
        /// Report a proxy success
        /// </summary>
        public void ReportSuccess(ProxyInfo proxy)
        {
            if (proxy == null)
                return;

            lock (_lock)
            {
                var targetProxy = _proxies.FirstOrDefault(p => p.Equals(proxy));

                if (targetProxy != null)
                {
                    targetProxy.FailureCount = 0;
                    targetProxy.IsHealthy = true;
                }
            }
        }

        /// <summary>
        /// Perform health check on all proxies
        /// </summary>
        private async Task PerformHealthCheckAsync()
        {
            if (!EnableHealthCheck)
                return;

            var tasks = new List<Task>();

            lock (_lock)
            {
                foreach (var proxy in _proxies.ToList())
                {
                    tasks.Add(CheckProxyHealthAsync(proxy));
                }
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Check health of a single proxy
        /// </summary>
        private async Task CheckProxyHealthAsync(ProxyInfo proxy)
        {
            try
            {
                var handler = new HttpClientHandler
                {
                    Proxy = new System.Net.WebProxy($"{proxy.Scheme}://{proxy.Host}:{proxy.Port}"),
                    UseProxy = true
                };

                using (var client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromSeconds(HealthCheckTimeout);
                    var response = await client.GetAsync(HealthCheckUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        ReportSuccess(proxy);
                    }
                    else
                    {
                        ReportFailure(proxy);
                    }
                }
            }
            catch
            {
                ReportFailure(proxy);
            }
            finally
            {
                proxy.LastChecked = DateTime.Now;
            }
        }

        /// <summary>
        /// Get statistics about the proxy pool
        /// </summary>
        public ProxyPoolStatistics GetStatistics()
        {
            lock (_lock)
            {
                return new ProxyPoolStatistics
                {
                    TotalProxies = _proxies.Count,
                    HealthyProxies = _proxies.Count(p => p.IsHealthy),
                    UnhealthyProxies = _proxies.Count(p => !p.IsHealthy),
                    AverageFailureCount = _proxies.Any() ? _proxies.Average(p => p.FailureCount) : 0
                };
            }
        }

        /// <summary>
        /// Get all proxies in the pool
        /// </summary>
        public List<ProxyInfo> GetAllProxies()
        {
            lock (_lock)
            {
                return _proxies.ToList();
            }
        }

        /// <summary>
        /// Remove a specific proxy from the pool
        /// </summary>
        public bool RemoveProxy(ProxyInfo proxy)
        {
            lock (_lock)
            {
                return _proxies.Remove(proxy);
            }
        }

        /// <summary>
        /// Clear all proxies from the pool
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                _proxies.Clear();
                _currentIndex = 0;
            }
        }

        /// <summary>
        /// Dispose resources
        /// </summary>
        public void Dispose()
        {
            _healthCheckTimer?.Dispose();
        }
    }

    /// <summary>
    /// Statistics about the proxy pool
    /// </summary>
    public class ProxyPoolStatistics
    {
        public int TotalProxies { get; set; }
        public int HealthyProxies { get; set; }
        public int UnhealthyProxies { get; set; }
        public double AverageFailureCount { get; set; }

        public override string ToString()
        {
            return $"Total: {TotalProxies}, Healthy: {HealthyProxies}, Unhealthy: {UnhealthyProxies}, Avg Failures: {AverageFailureCount:F2}";
        }
    }
}
