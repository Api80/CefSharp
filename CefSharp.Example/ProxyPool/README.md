# CefSharp Proxy Pool Example

This directory contains a complete implementation of an IP proxy pool for CefSharp applications.

## Features

- **Multiple Rotation Strategies**: Sequential, Random, Least Used, and Health-Based
- **Automatic Health Checking**: Periodic health checks to identify and disable unhealthy proxies
- **Failure Tracking**: Automatically track and handle proxy failures
- **File Loading**: Load proxy lists from text files
- **Authentication Support**: Support for proxies requiring username/password
- **Thread-Safe**: All operations are thread-safe
- **Statistics**: Get real-time statistics about proxy pool health

## Files

- `ProxyPool.cs` - Main proxy pool implementation with rotation and health checking

## Usage Examples

### Basic Usage

```csharp
using CefSharp.Example.ProxyPool;

// Create a proxy pool
var proxyPool = new ProxyPool
{
    Mode = ProxyPool.RotationMode.Sequential,
    MaxFailureCount = 3,
    EnableHealthCheck = true
};

// Add proxies
proxyPool.AddProxy("proxy1.example.com", 8080, "http");
proxyPool.AddProxy("proxy2.example.com", 8080, "http");

// Get next proxy
var proxy = proxyPool.GetNextProxy();
Console.WriteLine($"Using proxy: {proxy}");

// Report success or failure
proxyPool.ReportSuccess(proxy);
// or
proxyPool.ReportFailure(proxy);

// Get statistics
var stats = proxyPool.GetStatistics();
Console.WriteLine(stats);
```

### Loading from File

Create a file `proxies.txt`:
```
# HTTP Proxies
http://proxy1.example.com:8080
http://proxy2.example.com:8080

# SOCKS5 Proxies
socks5://proxy3.example.com:1080

# Proxies with authentication
http://username:password@proxy4.example.com:8080
```

Then load it:
```csharp
var proxyPool = new ProxyPool();
proxyPool.LoadFromFile("proxies.txt");
```

### Integration with CefSharp

```csharp
// Create a request context
var requestContext = new RequestContext();

// Get proxy from pool
var proxy = proxyPool.GetNextProxy();

// Set the proxy
var result = await requestContext.SetProxyAsync(
    proxy.Scheme, 
    proxy.Host, 
    proxy.Port
);

if (result.Success)
{
    // Create browser with this request context
    var browser = new ChromiumWebBrowser("https://www.example.com")
    {
        RequestContext = requestContext
    };
}
```

## Rotation Modes

1. **Sequential**: Rotates through proxies in order
2. **Random**: Randomly selects a proxy
3. **LeastUsed**: Selects the proxy that was used least recently
4. **HealthBased**: Selects the proxy with the lowest failure count

## Health Checking

The proxy pool automatically checks proxy health at regular intervals:

```csharp
var proxyPool = new ProxyPool
{
    EnableHealthCheck = true,
    HealthCheckInterval = 60000, // 1 minute
    HealthCheckUrl = "http://www.gstatic.com/generate_204",
    HealthCheckTimeout = 10 // seconds
};
```

## Best Practices

1. **Use Separate Request Contexts**: Each browser instance should have its own request context
2. **Handle Failures Gracefully**: Always call `ReportFailure()` when a proxy fails
3. **Monitor Statistics**: Regularly check pool statistics to ensure healthy proxies
4. **Adjust Health Check Frequency**: Set appropriate intervals based on your needs
5. **Clean Up Resources**: Always dispose the proxy pool when done

## Thread Safety

All public methods in `ProxyPool` are thread-safe and can be called from multiple threads simultaneously.

## License

This code is licensed under the BSD 3-Clause License, same as CefSharp.
