// Copyright © 2024 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using CefSharp;
using CefSharp.Handler;

namespace CefSharp.Example.CommercialProxy
{
    /// <summary>
    /// Helper class for parsing and managing authenticated proxies in the format: user:pass@host:port
    /// This is commonly used by commercial proxy services like Siyetian (思叶天).
    /// 
    /// Note: Chromium does NOT support username:password@host:port format directly in command-line arguments.
    /// You must split it into two steps:
    /// 1. Set proxy server address (host:port) via CefSettings or RequestContext
    /// 2. Handle authentication (user:pass) via GetAuthCredentials in RequestHandler
    /// </summary>
    public class ProxyAuthHelper
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
        /// Username for proxy authentication
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password for proxy authentication
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Parse a proxy string in the format: user:pass@host:port
        /// Example: "22VWD-1:531246@sk1.siyetian.com:6153"
        /// </summary>
        /// <param name="proxyString">Proxy string to parse</param>
        /// <returns>ProxyAuthHelper with parsed information</returns>
        /// <exception cref="ArgumentException">Thrown when format is invalid</exception>
        public static ProxyAuthHelper Parse(string proxyString)
        {
            if (string.IsNullOrWhiteSpace(proxyString))
                throw new ArgumentException("Proxy string cannot be empty", nameof(proxyString));

            // Split by @ to separate auth and server parts
            var parts = proxyString.Split('@');
            if (parts.Length != 2)
                throw new ArgumentException("Invalid proxy format. Expected format: user:pass@host:port", nameof(proxyString));

            var authPart = parts[0];    // user:pass
            var serverPart = parts[1];  // host:port

            // Parse authentication
            var authParts = authPart.Split(':');
            if (authParts.Length < 2)
                throw new ArgumentException("Invalid auth format. Expected format: username:password", nameof(proxyString));

            // Handle passwords that contain colons by joining remaining parts
            var username = authParts[0];
            var password = string.Join(":", authParts, 1, authParts.Length - 1);
            
            // Validate username is not empty
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty", nameof(proxyString));

            // Parse server address
            var serverParts = serverPart.Split(':');
            if (serverParts.Length != 2)
                throw new ArgumentException("Invalid server format. Expected format: host:port", nameof(proxyString));

            if (!int.TryParse(serverParts[1], out int port))
                throw new ArgumentException("Invalid port number", nameof(proxyString));

            return new ProxyAuthHelper
            {
                Username = username,
                Password = password,
                Host = serverParts[0],
                Port = port
            };
        }

        /// <summary>
        /// Get the server address without authentication info
        /// Format: host:port
        /// </summary>
        public string GetServerAddress()
        {
            return $"{Host}:{Port}";
        }

        /// <summary>
        /// Get the full proxy string with authentication
        /// Format: user:pass@host:port
        /// </summary>
        public string GetFullProxyString()
        {
            return $"{Username}:{Password}@{Host}:{Port}";
        }

        /// <summary>
        /// String representation
        /// </summary>
        public override string ToString()
        {
            return $"{Username}:***@{Host}:{Port}"; // Hide password in string representation
        }
    }

    /// <summary>
    /// Request handler for proxy authentication
    /// Automatically provides credentials when proxy server requests authentication (407 status)
    /// </summary>
    public class ProxyAuthRequestHandler : RequestHandler
    {
        private readonly string _username;
        private readonly string _password;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="username">Proxy username</param>
        /// <param name="password">Proxy password</param>
        public ProxyAuthRequestHandler(string username, string password)
        {
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        /// <summary>
        /// Constructor accepting ProxyAuthHelper
        /// </summary>
        /// <param name="proxyInfo">ProxyAuthHelper containing credentials</param>
        public ProxyAuthRequestHandler(ProxyAuthHelper proxyInfo)
        {
            if (proxyInfo == null)
                throw new ArgumentNullException(nameof(proxyInfo));

            _username = proxyInfo.Username;
            _password = proxyInfo.Password;
        }

        /// <summary>
        /// Called when authentication is required
        /// </summary>
        protected override bool GetAuthCredentials(
            IWebBrowser chromiumWebBrowser,
            IBrowser browser,
            string originUrl,
            bool isProxy,
            string host,
            int port,
            string realm,
            string scheme,
            IAuthCallback callback)
        {
            // Key check: isProxy=true means proxy server is requesting authentication
            if (isProxy)
            {
                System.Diagnostics.Debug.WriteLine($"Proxy authentication requested for {host}:{port}");

                // Provide credentials
                callback.Continue(_username, _password);
                return true; // Indicate we handled the authentication
            }

            // For website authentication (not proxy), use default behavior
            return base.GetAuthCredentials(chromiumWebBrowser, browser, originUrl,
                isProxy, host, port, realm, scheme, callback);
        }
    }
}
