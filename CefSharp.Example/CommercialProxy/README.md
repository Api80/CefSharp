# CefSharp.Example - Commercial Proxy Integration

This directory contains examples for integrating commercial proxy services (like Siyetian 思叶天) with CefSharp, including support for authenticated proxies.

## Overview

Commercial proxy services provide rotating IP addresses through APIs. This implementation handles:

- Fetching proxies from API endpoints
- Parsing both JSON and plain text responses
- **Authenticated proxies in `user:pass@host:port` format**
- Automatic proxy rotation
- Expired proxy detection
- Thread-safe proxy management

## Important: Authenticated Proxy Configuration

Many commercial proxy services (like Siyetian) provide proxy addresses in the format:
```
username:password@host:port
```

**Critical Note:** Chromium does **NOT** support `username:password@host:port` directly in command-line arguments. You must handle it in two steps:

1. **Set proxy server address** (`host:port`) via `CefSettings` or `RequestContext.SetProxyAsync`
2. **Handle authentication** (`username:password`) via `GetAuthCredentials` in a custom `RequestHandler`

### Example: Parsing and Using Authenticated Proxies

```csharp
using CefSharp.Example.CommercialProxy;

// Parse proxy string like "22VWD-1:531246@sk1.siyetian.com:6153"
var proxyInfo = ProxyAuthHelper.Parse("22VWD-1:531246@sk1.siyetian.com:6153");

// Step 1: Set proxy server address (without credentials)
var requestContext = new RequestContext();
await requestContext.SetProxyAsync("http", proxyInfo.Host, proxyInfo.Port);

// Step 2: Set up authentication handler
var browser = new ChromiumWebBrowser("https://www.example.com")
{
    RequestContext = requestContext,
    RequestHandler = new ProxyAuthRequestHandler(proxyInfo)
};
```

See `ProxyAuthHelper.cs` for the complete implementation.

## Files

- `CommercialProxyClient.cs` - Client for fetching proxies from commercial APIs
- `ProxyAuthHelper.cs` - Helper for parsing and managing authenticated proxies (`user:pass@host:port`)
- `README.md` - This file

## Supported Services

While designed with Siyetian (https://www.siyetian.com/) as reference, this implementation can work with most commercial proxy services that provide:

- HTTP/HTTPS proxies
- API endpoints to fetch proxy addresses
- Standard response formats (JSON or plain text)

Compatible with:
- 思叶天 (Siyetian)
- 芝麻代理 (Zhima)
- 快代理 (Kuai)
- Other similar services

## Usage Examples

### Basic Usage

```csharp
using CefSharp.Example.CommercialProxy;

// 1. Create client with your API URL
using (var client = new CommercialProxyClient("https://api.siyetian.com/get?type=http&num=1&key=YOUR_KEY"))
{
    // 2. Fetch a proxy
    var proxy = await client.GetProxyAsync();

    if (proxy != null)
    {
        Console.WriteLine($"Got proxy: {proxy.Host}:{proxy.Port}");
        
        // 3. Configure CefSharp
        var requestContext = new RequestContext();
        await requestContext.SetProxyAsync(proxy.Scheme, proxy.Host, proxy.Port);
        
        // 4. Create browser with proxy
        var browser = new ChromiumWebBrowser("https://www.example.com")
        {
            RequestContext = requestContext
        };
    }
}
```

### Proxy Rotation

```csharp
// Create rotator
var rotator = new CommercialProxyRotator("YOUR_API_URL");

// Load multiple proxies
await rotator.LoadProxiesAsync(5);

// Get next proxy in rotation
var proxy = rotator.GetNextProxy();

// Check status
Console.WriteLine($"Loaded: {rotator.ProxyCount}, Available: {rotator.AvailableProxyCount}");
```

### Authenticated Proxies (user:pass@host:port)

Many commercial proxy services provide credentials in the format `username:password@host:port`. For example, Siyetian might provide:

```
22VWD-1:531246@sk1.siyetian.com:6153
```

**Important:** You cannot use this format directly in CefSharp. Instead:

#### Step 1: Parse the proxy string

```csharp
using CefSharp.Example.CommercialProxy;

string proxyString = "22VWD-1:531246@sk1.siyetian.com:6153";
var proxyInfo = ProxyAuthHelper.Parse(proxyString);

Console.WriteLine($"Server: {proxyInfo.Host}:{proxyInfo.Port}");
Console.WriteLine($"Username: {proxyInfo.Username}");
// Password is stored but not displayed for security
```

#### Step 2: Configure proxy server address

```csharp
// For global proxy (at startup)
var settings = new CefSettings();
settings.CefCommandLineArgs.Add("proxy-server", proxyInfo.GetServerAddress());
Cef.Initialize(settings);

// OR for dynamic proxy (runtime)
var requestContext = new RequestContext();
await requestContext.SetProxyAsync("http", proxyInfo.Host, proxyInfo.Port);
```

#### Step 3: Set up authentication handler

```csharp
// Create browser with authentication
var browser = new ChromiumWebBrowser("https://www.example.com")
{
    RequestContext = requestContext,  // Use the context with proxy configured
    RequestHandler = new ProxyAuthRequestHandler(proxyInfo)  // Handle authentication
};

// OR pass credentials directly
browser.RequestHandler = new ProxyAuthRequestHandler(
    proxyInfo.Username, 
    proxyInfo.Password
);
```

#### Complete Example

```csharp
public async Task SetupAuthenticatedProxy()
{
    // Parse the full proxy string
    var proxyInfo = ProxyAuthHelper.Parse("22VWD-1:531246@sk1.siyetian.com:6153");
    
    // Configure proxy server
    var requestContext = new RequestContext();
    var result = await requestContext.SetProxyAsync("http", proxyInfo.Host, proxyInfo.Port);
    
    if (!result.Success)
    {
        Console.WriteLine($"Failed to set proxy: {result.ErrorMessage}");
        return;
    }
    
    // Create browser with auth handler
    var browser = new ChromiumWebBrowser("https://www.baidu.com")
    {
        RequestContext = requestContext,
        RequestHandler = new ProxyAuthRequestHandler(proxyInfo)
    };
    
    Console.WriteLine("Proxy configured successfully!");
}
```

### Why Two Steps?

Chromium's security model prevents passing credentials via command-line arguments to avoid exposing passwords. The two-step process:

1. **Proxy Server Configuration**: Tells Chromium where to connect
2. **Authentication Handler**: Responds when the proxy server returns a 407 (Proxy Authentication Required) status

When the proxy server requests authentication:
- `GetAuthCredentials` is called with `isProxy=true`
- The handler provides the username and password
- Chromium completes the authentication automatically

### Automatic Failover

```csharp
private CommercialProxyRotator _rotator;
private ChromiumWebBrowser _browser;

// Setup browser with automatic proxy switching on error
_browser.LoadError += async (sender, e) =>
{
    if (e.ErrorCode != CefErrorCode.Aborted)
    {
        // Try next proxy
        var newProxy = _rotator.GetNextProxy();
        if (newProxy != null)
        {
            await _requestContext.SetProxyAsync(newProxy.Scheme, newProxy.Host, newProxy.Port);
            _browser.Reload();
        }
    }
};
```

## API Response Formats

### JSON Format (Single Proxy)
```json
{
  "ip": "123.45.67.89",
  "port": 8080,
  "expire_time": "1640000000",
  "city": "Beijing",
  "province": "Beijing",
  "isp": "Telecom"
}
```

### JSON Format (Array)
```json
[
  {"ip": "123.45.67.89", "port": 8080},
  {"ip": "98.76.54.32", "port": 8888}
]
```

### Plain Text Format
```
123.45.67.89:8080
98.76.54.32:8888
```

## Siyetian Specific Configuration

### Getting Your API URL

1. Visit https://www.siyetian.com/
2. Register/Login to your account
3. Go to user center (用户中心)
4. Find your API extraction link (API提取链接)

### API Parameters

Common Siyetian API parameters:

- `type`: Proxy type (http, https, socks5)
- `num`: Number of proxies to fetch
- `key`: Your API key
- `time`: Proxy validity duration (optional)
- `format`: Response format (json, txt)

Example URL:
```
https://api.siyetian.com/get?type=http&num=5&key=YOUR_KEY&time=60&format=json
```

### Important Notes

1. **IP Whitelist**: Add your server's IP to the whitelist in Siyetian dashboard
2. **Rate Limiting**: Respect API rate limits (usually 1-5 requests/second)
3. **Proxy Expiration**: Proxies have time limits, check `ExpireTime` property
4. **Traffic Billing**: Monitor your proxy traffic usage
5. **Error Handling**: Always implement retry logic for failed proxies

## Integration with CefSharp

### Complete WinForms Example

See `README.zh-CN.md` in the repository root for a complete WinForms application example with UI that shows:

- Loading proxies from API
- Displaying current proxy
- Manual and automatic proxy rotation
- Error handling and retry logic

### Key Integration Points

1. **Request Context**: Each browser instance should use its own RequestContext
2. **Proxy Setting**: Use `SetProxyAsync` to configure proxy before loading pages
3. **Error Handling**: Monitor `LoadError` event for proxy failures
4. **Resource Cleanup**: Properly dispose RequestContext and browser instances

## Thread Safety

All classes in this implementation are thread-safe:

- `CommercialProxyClient`: Can be called from multiple threads; implements IDisposable for proper resource cleanup
- `CommercialProxyRotator`: Uses locks for safe concurrent access
- `ProxyAddress`: Immutable data class

## Performance Considerations

1. **Connection Pooling**: HttpClient is reused for API calls
2. **Timeout Settings**: 10-second timeout prevents hanging
3. **Delayed Requests**: 1-second delay between batch proxy fetches
4. **Lazy Evaluation**: Proxies checked for expiration only when accessed
5. **UTC Timestamps**: All time comparisons use UTC for consistency across timezones
6. **Resource Disposal**: CommercialProxyClient implements IDisposable for proper cleanup

## Troubleshooting

### Proxy Not Working

1. Verify IP is whitelisted in proxy service dashboard
2. Check API response format matches expected structure
3. Ensure proxy hasn't expired (check `IsExpired` property)
4. Test proxy directly with curl or Postman first

### API Errors

1. Verify API URL is correct and includes all required parameters
2. Check API key is valid and not expired
3. Ensure you haven't exceeded rate limits
4. Review API documentation for any service-specific requirements

### CefSharp Integration Issues

1. Make sure to call `SetProxyAsync` before loading any pages
2. Use separate RequestContext for proxy isolation
3. Check that CefSettings allow proxy changes (not set via command line)
4. Monitor console for any CefSharp error messages

## License

This code is part of CefSharp and licensed under the BSD 3-Clause License.
