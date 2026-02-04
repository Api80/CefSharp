# CefSharp Commercial Proxy Integration

This directory contains examples for integrating commercial proxy services (like Siyetian 思叶天) with CefSharp.

## Overview

Commercial proxy services provide rotating IP addresses through APIs. This implementation handles:

- Fetching proxies from API endpoints
- Parsing both JSON and plain text responses
- Automatic proxy rotation
- Expired proxy detection
- Thread-safe proxy management

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
