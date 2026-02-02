# CefSharp - 将 Chromium 嵌入到 .NET 应用程序中

[![CefSharp Logo](logo.png)](https://cefsharp.github.io/ "CefSharp - 嵌入式 Chromium for .NET")

[![Build status](https://ci.appveyor.com/api/projects/status/9g4mcuqruc283g66/branch/master?svg=true)](https://ci.appveyor.com/project/cefsharp/cefsharp/branch/master)

## 目录

- [简介](#简介)
- [主要特性](#主要特性)
- [系统要求](#系统要求)
- [快速开始](#快速开始)
- [NuGet 包](#nuget-包)
- [基础使用示例](#基础使用示例)
  - [WinForms 集成](#winforms-集成)
  - [WPF 集成](#wpf-集成)
  - [OffScreen 渲染](#offscreen-渲染)
- [核心功能详解](#核心功能详解)
  - [JavaScript 绑定](#javascript-绑定)
  - [JavaScript 调用 C#](#javascript-调用-c)
  - [C# 调用 JavaScript](#c-调用-javascript)
  - [自定义请求处理](#自定义请求处理)
  - [自定义 Scheme Handler](#自定义-scheme-handler)
  - [下载处理](#下载处理)
  - [右键菜单](#右键菜单)
  - [Cookie 管理](#cookie-管理)
  - [代理设置](#代理设置)
  - [截图功能](#截图功能)
  - [打印 PDF](#打印-pdf)
- [常见问题](#常见问题)
- [项目结构](#项目结构)
- [文档资源](#文档资源)
- [社区支持](#社区支持)
- [许可证](#许可证)

## 简介

[CefSharp](https://cefsharp.github.io/) 是一个轻量级的 .NET 包装器，它可以让你在 .NET 应用程序中嵌入 Chromium 浏览器。CefSharp 基于 [Chromium Embedded Framework (CEF)](https://bitbucket.org/chromiumembedded/cef)，由 Marshall A. Greenblatt 开发。

CefSharp 的约 30% 代码使用 C++/CLI 编写，其余大部分使用 C# 编写。它可以被 C#、VB 或其他任何 CLR 语言使用。CefSharp 提供了 WPF 和 WinForms 两种 Web 浏览器控件实现。

## 主要特性

- ✅ **完整的 Chromium 内核** - 支持最新的 HTML5、CSS3、JavaScript 特性
- ✅ **跨平台支持** - 支持 .NET Framework 4.6.2+ 和 .NET 6+
- ✅ **多种集成方式** - WinForms、WPF、OffScreen 渲染
- ✅ **JavaScript 互操作** - C# 与 JavaScript 双向通信
- ✅ **自定义请求处理** - 拦截和处理网络请求
- ✅ **离屏渲染** - 无 UI 的后台页面渲染和截图
- ✅ **多进程架构** - 稳定性和安全性更高
- ✅ **开源免费** - BSD 许可证，可用于商业和开源项目

## 系统要求

### .NET Framework 项目

- **操作系统**: Windows 7 SP1 或更高版本
- **.NET 版本**: .NET Framework 4.6.2 或更高版本
- **Visual C++ 运行时**:
  - 对于 CefSharp 版本 138 及以上: VC++ 2022 Redistributable
  - 对于 CefSharp 版本 92-137: VC++ 2019 Redistributable
  - 对于 CefSharp 版本 91 及以下: VC++ 2015 Redistributable
- **架构**: x86、x64 或 ARM64

### .NET Core / .NET 6+ 项目

- **操作系统**: Windows 7 SP1 或更高版本
- **.NET 版本**: .NET 6.0 或更高版本
- **Visual C++ 运行时**: 同上
- **架构**: x64 或 ARM64

## 快速开始

### 1. 安装 NuGet 包

根据你的项目类型选择相应的 NuGet 包：

**WinForms 应用程序 (.NET Framework 4.8):**
```powershell
Install-Package CefSharp.WinForms
```

**WPF 应用程序 (.NET Framework 4.8):**
```powershell
Install-Package CefSharp.Wpf
```

**OffScreen 渲染 (.NET Framework 4.8):**
```powershell
Install-Package CefSharp.OffScreen
```

### 2. 配置项目属性

在你的项目中，确保设置了正确的平台目标：

- 右键点击项目 → 属性
- 在 "生成" 选项卡中，将 "平台目标" 设置为 `x86`、`x64` 或 `ARM64`（不要使用 `Any CPU`）

### 3. 设置应用程序清单

为了最佳兼容性，建议添加应用程序清单文件（app.manifest），并启用 DPI 感知：

```xml
<application xmlns="urn:schemas-microsoft-com:asm.v3">
  <windowsSettings>
    <dpiAware xmlns="http://schemas.microsoft.com/SMI/2005/WindowsSettings">true</dpiAware>
    <dpiAwareness xmlns="http://schemas.microsoft.com/SMI/2016/WindowsSettings">PerMonitorV2, PerMonitor</dpiAwareness>
  </windowsSettings>
</application>
```

## NuGet 包

### 稳定版本

- [![CefSharp.WinForms](https://img.shields.io/nuget/v/CefSharp.WinForms.svg?style=flat&label=WinForms)](https://www.nuget.org/packages/CefSharp.WinForms/)
- [![CefSharp.Wpf](https://img.shields.io/nuget/v/CefSharp.Wpf.svg?style=flat&label=Wpf)](https://www.nuget.org/packages/CefSharp.Wpf/)
- [![CefSharp.OffScreen](https://img.shields.io/nuget/v/CefSharp.OffScreen.svg?style=flat&label=OffScreen)](https://www.nuget.org/packages/CefSharp.OffScreen/)
- [![CefSharp.Wpf.HwndHost](https://img.shields.io/nuget/v/CefSharp.Wpf.HwndHost.svg?style=flat&label=Wpf.HwndHost)](https://www.nuget.org/packages/CefSharp.Wpf.HwndHost/)

### .NET Core / .NET 6+ 包

- [![CefSharp.WinForms.NETCore](http://img.shields.io/nuget/v/CefSharp.WinForms.NETCore.svg?style=flat&label=WinForms.NETCore)](http://www.nuget.org/packages/CefSharp.WinForms.NETCore/)
- [![CefSharp.Wpf.NETCore](http://img.shields.io/nuget/v/CefSharp.Wpf.NETCore.svg?style=flat&label=Wpf.NETCore)](http://www.nuget.org/packages/CefSharp.Wpf.NETCore/)
- [![CefSharp.OffScreen.NETCore](http://img.shields.io/nuget/v/CefSharp.OffScreen.NETCore.svg?style=flat&label=OffScreen.NETCore)](http://www.nuget.org/packages/CefSharp.OffScreen.NETCore/)

## 基础使用示例

### WinForms 集成

#### 最简单的示例

```csharp
using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace CefSharpWinFormsExample
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            // 初始化 CefSharp 设置
            var settings = new CefSettings();
            
            // 初始化 Cef
            Cef.Initialize(settings);
            
            // 创建表单
            var form = new Form
            {
                Width = 1200,
                Height = 900,
                Text = "CefSharp WinForms 浏览器"
            };
            
            // 创建浏览器控件
            var browser = new ChromiumWebBrowser("https://www.google.com")
            {
                Dock = DockStyle.Fill
            };
            
            // 添加浏览器到表单
            form.Controls.Add(browser);
            
            // 运行应用程序
            Application.Run(form);
            
            // 清理
            Cef.Shutdown();
        }
    }
}
```

#### 完整的 WinForms 示例

```csharp
using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace CefSharpWinFormsExample
{
    public partial class BrowserForm : Form
    {
        private ChromiumWebBrowser browser;
        
        public BrowserForm()
        {
            InitializeComponent();
            InitializeBrowser();
        }
        
        private void InitializeBrowser()
        {
            // 创建浏览器
            browser = new ChromiumWebBrowser("https://www.google.com")
            {
                Dock = DockStyle.Fill
            };
            
            // 添加到表单
            this.Controls.Add(browser);
            
            // 订阅页面加载完成事件
            browser.LoadingStateChanged += OnLoadingStateChanged;
            
            // 订阅地址改变事件
            browser.AddressChanged += OnAddressChanged;
            
            // 订阅标题改变事件
            browser.TitleChanged += OnTitleChanged;
        }
        
        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                // 页面加载完成
                this.Invoke(new Action(() =>
                {
                    toolStripStatusLabel.Text = "页面加载完成";
                }));
            }
        }
        
        private void OnAddressChanged(object sender, AddressChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                addressTextBox.Text = e.Address;
            }));
        }
        
        private void OnTitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                this.Text = e.Title;
            }));
        }
        
        private void GoButton_Click(object sender, EventArgs e)
        {
            browser.Load(addressTextBox.Text);
        }
        
        private void BackButton_Click(object sender, EventArgs e)
        {
            if (browser.CanGoBack)
            {
                browser.Back();
            }
        }
        
        private void ForwardButton_Click(object sender, EventArgs e)
        {
            if (browser.CanGoForward)
            {
                browser.Forward();
            }
        }
        
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            browser.Reload();
        }
    }
    
    public class Program
    {
        [STAThread]
        static void Main()
        {
            // 配置 CefSettings
            var settings = new CefSettings
            {
                // 设置缓存路径
                CachePath = System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "CefSharp\\Cache"
                ),
                
                // 启用远程调试（可选）
                RemoteDebuggingPort = 8088
            };
            
            // 初始化 CefSharp
            Cef.Initialize(settings);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BrowserForm());
            
            // 关闭 CefSharp
            Cef.Shutdown();
        }
    }
}
```

### WPF 集成

#### 最简单的 WPF 示例

**MainWindow.xaml:**
```xml
<Window x:Class="CefSharpWpfExample.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:wpf="clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf"
        Title="CefSharp WPF 浏览器" Height="600" Width="800">
    <Grid>
        <wpf:ChromiumWebBrowser x:Name="Browser" Address="https://www.google.com"/>
    </Grid>
</Window>
```

**MainWindow.xaml.cs:**
```csharp
using System.Windows;
using CefSharp;

namespace CefSharpWpfExample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // 订阅事件
            Browser.LoadingStateChanged += OnLoadingStateChanged;
        }
        
        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                // 页面加载完成
                Dispatcher.Invoke(() =>
                {
                    Title = $"CefSharp WPF 浏览器 - {Browser.Title}";
                });
            }
        }
    }
}
```

**App.xaml.cs:**
```csharp
using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace CefSharpWpfExample
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // 配置 CefSettings
            var settings = new CefSettings
            {
                CachePath = System.IO.Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData),
                    "CefSharp\\Cache"
                )
            };
            
            // 初始化 CefSharp
            Cef.Initialize(settings);
            
            base.OnStartup(e);
        }
        
        protected override void OnExit(ExitEventArgs e)
        {
            Cef.Shutdown();
            base.OnExit(e);
        }
    }
}
```

#### 完整的 WPF 示例（带导航栏）

**MainWindow.xaml:**
```xml
<Window x:Class="CefSharpWpfExample.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:wpf="clr-namespace:CefSharp.Wpf;assembly=CefSharp.Wpf"
        Title="CefSharp WPF 浏览器" Height="700" Width="1000">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        
        <!-- 导航栏 -->
        <ToolBar Grid.Row="0">
            <Button Content="后退" Click="BackButton_Click" Width="60"/>
            <Button Content="前进" Click="ForwardButton_Click" Width="60"/>
            <Button Content="刷新" Click="RefreshButton_Click" Width="60"/>
            <TextBox x:Name="AddressTextBox" Width="600" Margin="5,0"/>
            <Button Content="转到" Click="GoButton_Click" Width="60"/>
        </ToolBar>
        
        <!-- 浏览器 -->
        <wpf:ChromiumWebBrowser x:Name="Browser" Grid.Row="1" Address="https://www.google.com"/>
        
        <!-- 状态栏 -->
        <StatusBar Grid.Row="2">
            <StatusBarItem>
                <TextBlock x:Name="StatusText" Text="就绪"/>
            </StatusBarItem>
        </StatusBar>
    </Grid>
</Window>
```

**MainWindow.xaml.cs:**
```csharp
using System.Windows;
using CefSharp;

namespace CefSharpWpfExample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // 订阅事件
            Browser.LoadingStateChanged += OnLoadingStateChanged;
            Browser.AddressChanged += OnAddressChanged;
            Browser.TitleChanged += OnTitleChanged;
        }
        
        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                StatusText.Text = e.IsLoading ? "正在加载..." : "加载完成";
            });
        }
        
        private void OnAddressChanged(object sender, AddressChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                AddressTextBox.Text = e.Address;
            });
        }
        
        private void OnTitleChanged(object sender, TitleChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Title = $"CefSharp WPF 浏览器 - {e.Title}";
            });
        }
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoBack)
            {
                Browser.Back();
            }
        }
        
        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoForward)
            {
                Browser.Forward();
            }
        }
        
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Browser.Reload();
        }
        
        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            Browser.Load(AddressTextBox.Text);
        }
    }
}
```

### OffScreen 渲染

OffScreen 渲染允许你在没有 UI 的情况下加载网页、执行 JavaScript、截图等：

```csharp
using System;
using System.IO;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.OffScreen;

namespace CefSharpOffScreenExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 配置 CefSettings
            var settings = new CefSettings
            {
                CachePath = Path.Combine(Path.GetTempPath(), "CefSharp\\Cache")
            };
            
            // 初始化 Cef
            Cef.Initialize(settings);
            
            // 创建 OffScreen 浏览器
            var browserSettings = new BrowserSettings
            {
                WindowlessFrameRate = 1 // 降低帧率以节省资源
            };
            
            using (var browser = new ChromiumWebBrowser("https://www.google.com", browserSettings))
            {
                // 等待页面加载完成
                await browser.WaitForInitialLoadAsync();
                
                Console.WriteLine("页面加载完成！");
                
                // 执行 JavaScript
                var result = await browser.EvaluateScriptAsync("document.title");
                Console.WriteLine($"页面标题: {result.Result}");
                
                // 获取页面内容大小
                var contentSize = await browser.GetContentSizeAsync();
                Console.WriteLine($"页面大小: {contentSize.Width} x {contentSize.Height}");
                
                // 截图
                var screenshot = await browser.CaptureScreenshotAsync();
                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var screenshotPath = Path.Combine(desktopPath, "screenshot.png");
                
                File.WriteAllBytes(screenshotPath, screenshot);
                Console.WriteLine($"截图已保存到: {screenshotPath}");
            }
            
            // 清理
            Cef.Shutdown();
            
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
    }
}
```

## 核心功能详解

### JavaScript 绑定

CefSharp 提供了强大的 JavaScript 与 C# 互操作能力。

#### 将 C# 对象绑定到 JavaScript

**1. 创建一个 C# 类：**

```csharp
public class BoundObject
{
    public string MyProperty { get; set; }
    
    public void ShowMessage(string message)
    {
        MessageBox.Show(message, "来自 JavaScript 的消息");
    }
    
    public string GetCurrentTime()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
    
    public async Task<string> GetDataAsync()
    {
        await Task.Delay(1000); // 模拟异步操作
        return "异步数据已返回";
    }
}
```

**2. 注册对象到浏览器：**

```csharp
// 创建浏览器实例
var browser = new ChromiumWebBrowser("https://www.example.com");

// 注册对象（在浏览器创建后立即注册）
browser.JavascriptObjectRepository.Register("boundObject", new BoundObject(), 
    isAsync: true, // 设置为异步绑定
    options: BindingOptions.DefaultBinder);
```

**3. 在 JavaScript 中调用：**

```javascript
// 异步绑定需要先等待对象可用
(async function() {
    // 等待对象绑定完成
    await CefSharp.BindObjectAsync("boundObject");
    
    // 调用同步方法
    boundObject.showMessage("Hello from JavaScript!");
    
    // 调用返回值的方法
    var time = await boundObject.getCurrentTime();
    console.log("当前时间: " + time);
    
    // 调用异步方法
    var data = await boundObject.getDataAsync();
    console.log("异步数据: " + data);
    
    // 访问属性
    boundObject.myProperty = "新值";
    console.log(boundObject.myProperty);
})();
```

#### 同步绑定（不推荐，已过时）

```csharp
// 同步绑定（不推荐）
browser.JavascriptObjectRepository.Register("boundObject", new BoundObject(), 
    isAsync: false);
```

```javascript
// 同步调用（旧方式）
boundObject.showMessage("Hello!");
```

### JavaScript 调用 C#

除了对象绑定，还可以使用 `RegisterJsObject` 方法（旧版本）或通过消息传递：

#### 使用 PostMessage

**C# 端：**

```csharp
browser.JavascriptMessageReceived += (sender, e) =>
{
    // 接收来自 JavaScript 的消息
    if (e.Message is string message)
    {
        MessageBox.Show($"收到消息: {message}");
    }
    else if (e.Message is IDictionary<string, object> dict)
    {
        // 处理 JSON 对象
        var type = dict["type"].ToString();
        var data = dict["data"].ToString();
        Console.WriteLine($"类型: {type}, 数据: {data}");
    }
};
```

**JavaScript 端：**

```javascript
// 发送字符串消息
CefSharp.PostMessage("Hello from JavaScript!");

// 发送 JSON 对象
CefSharp.PostMessage({
    type: "userAction",
    data: "按钮被点击"
});
```

### C# 调用 JavaScript

#### 执行 JavaScript 代码

```csharp
// 简单的 JavaScript 执行
browser.ExecuteScriptAsync("alert('Hello from C#!');");

// 执行并获取返回值
var result = await browser.EvaluateScriptAsync("document.title");
if (result.Success)
{
    var title = result.Result.ToString();
    Console.WriteLine($"页面标题: {title}");
}

// 执行带参数的 JavaScript
var script = @"
    (function(name, age) {
        return `${name} 今年 ${age} 岁`;
    })('张三', 25)
";
var result2 = await browser.EvaluateScriptAsync(script);
Console.WriteLine(result2.Result); // 输出: 张三 今年 25 岁

// 修改 DOM
await browser.EvaluateScriptAsync(@"
    document.getElementById('myDiv').innerHTML = '内容已更新';
");
```

#### 执行异步 JavaScript

```csharp
// 等待 Promise 完成
var script = @"
    (async function() {
        const response = await fetch('https://api.example.com/data');
        const data = await response.json();
        return data;
    })()
";

var result = await browser.EvaluateScriptAsync(script, timeout: TimeSpan.FromSeconds(30));
if (result.Success)
{
    Console.WriteLine($"API 返回数据: {result.Result}");
}
```

### 自定义请求处理

使用 `IRequestHandler` 可以拦截和处理所有网络请求：

```csharp
public class CustomRequestHandler : RequestHandler
{
    protected override bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, 
        IFrame frame, IRequest request, bool userGesture, bool isRedirect)
    {
        // 在浏览器导航之前调用
        Console.WriteLine($"即将导航到: {request.Url}");
        
        // 返回 true 取消导航
        if (request.Url.Contains("blocked-site.com"))
        {
            MessageBox.Show("该网站已被屏蔽！");
            return true; // 取消导航
        }
        
        return false; // 允许导航
    }
    
    protected override IResourceRequestHandler GetResourceRequestHandler(
        IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, 
        IRequest request, bool isNavigation, bool isDownload, 
        string requestInitiator, ref bool disableDefaultHandling)
    {
        // 返回自定义资源请求处理器
        return new CustomResourceRequestHandler();
    }
}

public class CustomResourceRequestHandler : ResourceRequestHandler
{
    protected override CefReturnValue OnBeforeResourceLoad(
        IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, 
        IRequest request, IRequestCallback callback)
    {
        // 在资源加载之前调用
        Console.WriteLine($"加载资源: {request.Url}");
        
        // 可以修改请求头
        var headers = request.Headers;
        headers["Custom-Header"] = "CustomValue";
        request.Headers = headers;
        
        return CefReturnValue.Continue;
    }
    
    protected override IResponseFilter GetResourceResponseFilter(
        IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, 
        IRequest request, IResponse response)
    {
        // 返回响应过滤器来修改响应内容
        if (request.Url.EndsWith(".html"))
        {
            return new CustomResponseFilter();
        }
        
        return null;
    }
}

// 应用请求处理器
browser.RequestHandler = new CustomRequestHandler();
```

### 自定义 Scheme Handler

创建自定义 URL Scheme 来处理特殊协议（如 `myapp://`）：

```csharp
public class CustomSchemeHandlerFactory : ISchemeHandlerFactory
{
    public IResourceHandler Create(IBrowser browser, IFrame frame, 
        string schemeName, IRequest request)
    {
        return new CustomSchemeHandler();
    }
}

public class CustomSchemeHandler : ResourceHandler
{
    public override CefReturnValue ProcessRequestAsync(IRequest request, ICallback callback)
    {
        // 处理自定义协议请求
        var uri = new Uri(request.Url);
        
        if (uri.Host == "loadfile")
        {
            // 从本地文件系统加载文件
            var filePath = uri.AbsolutePath.TrimStart('/');
            var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            
            if (File.Exists(fullPath))
            {
                var bytes = File.ReadAllBytes(fullPath);
                var mimeType = GetMimeType(fullPath);
                
                Stream = new MemoryStream(bytes);
                StatusCode = 200;
                MimeType = mimeType;
            }
            else
            {
                StatusCode = 404;
                StatusText = "File Not Found";
            }
        }
        
        callback.Continue();
        return CefReturnValue.Continue;
    }
    
    private string GetMimeType(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        switch (extension)
        {
            case ".html": return "text/html";
            case ".css": return "text/css";
            case ".js": return "application/javascript";
            case ".png": return "image/png";
            case ".jpg":
            case ".jpeg": return "image/jpeg";
            default: return "application/octet-stream";
        }
    }
}

// 注册自定义 Scheme
var settings = new CefSettings();
settings.RegisterScheme(new CefCustomScheme
{
    SchemeName = "myapp",
    SchemeHandlerFactory = new CustomSchemeHandlerFactory()
});

Cef.Initialize(settings);

// 现在可以访问 myapp://loadfile/index.html
```

### 下载处理

```csharp
public class CustomDownloadHandler : IDownloadHandler
{
    public bool CanDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, 
        string url, string requestMethod)
    {
        // 返回 true 允许下载
        return true;
    }
    
    public bool OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, 
        DownloadItem downloadItem, IBeforeDownloadCallback callback)
    {
        // 在下载开始前调用
        if (!callback.IsDisposed)
        {
            var downloadPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                downloadItem.SuggestedFileName
            );
            
            callback.Continue(downloadPath, showDialog: true);
        }
        
        return true;
    }
    
    public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, 
        DownloadItem downloadItem, IDownloadItemCallback callback)
    {
        // 下载进度更新
        Console.WriteLine($"下载进度: {downloadItem.PercentComplete}%");
        
        if (downloadItem.IsComplete)
        {
            Console.WriteLine($"下载完成: {downloadItem.FullPath}");
        }
        else if (downloadItem.IsCancelled)
        {
            Console.WriteLine("下载已取消");
        }
    }
}

// 应用下载处理器
browser.DownloadHandler = new CustomDownloadHandler();
```

### 右键菜单

```csharp
public class CustomMenuHandler : IContextMenuHandler
{
    public void OnBeforeContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, 
        IFrame frame, IContextMenuParams parameters, IMenuModel model)
    {
        // 清除默认菜单项
        model.Clear();
        
        // 添加自定义菜单项
        model.AddItem((CefMenuCommand)26501, "在新标签页中打开");
        model.AddItem((CefMenuCommand)26502, "复制链接地址");
        model.AddSeparator();
        model.AddItem((CefMenuCommand)26503, "查看页面源代码");
        model.AddItem((CefMenuCommand)26504, "开发者工具");
    }
    
    public bool OnContextMenuCommand(IWebBrowser chromiumWebBrowser, IBrowser browser, 
        IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, 
        CefEventFlags eventFlags)
    {
        // 处理菜单项点击
        switch ((int)commandId)
        {
            case 26501:
                // 在新标签页中打开
                if (!string.IsNullOrEmpty(parameters.LinkUrl))
                {
                    // 打开新标签页的逻辑
                    Console.WriteLine($"在新标签页中打开: {parameters.LinkUrl}");
                }
                return true;
                
            case 26502:
                // 复制链接
                if (!string.IsNullOrEmpty(parameters.LinkUrl))
                {
                    Clipboard.SetText(parameters.LinkUrl);
                }
                return true;
                
            case 26503:
                // 查看源代码
                frame.ViewSource();
                return true;
                
            case 26504:
                // 开发者工具
                browser.GetHost().ShowDevTools();
                return true;
        }
        
        return false;
    }
    
    public void OnContextMenuDismissed(IWebBrowser chromiumWebBrowser, IBrowser browser, 
        IFrame frame)
    {
        // 菜单关闭
    }
    
    public bool RunContextMenu(IWebBrowser chromiumWebBrowser, IBrowser browser, 
        IFrame frame, IContextMenuParams parameters, IMenuModel model, 
        IRunContextMenuCallback callback)
    {
        // 返回 false 使用默认实现
        return false;
    }
}

// 应用菜单处理器
browser.MenuHandler = new CustomMenuHandler();
```

### Cookie 管理

```csharp
// 获取 Cookie Manager
var cookieManager = Cef.GetGlobalCookieManager();

// 设置 Cookie
var cookie = new Cookie
{
    Name = "myCookie",
    Value = "myValue",
    Domain = ".example.com",
    Path = "/",
    Expires = DateTime.Now.AddDays(30)
};

cookieManager.SetCookie("https://www.example.com", cookie);

// 访问 Cookie
var visitor = new CookieVisitor();
cookieManager.VisitAllCookies(visitor);

// 删除 Cookie
cookieManager.DeleteCookies("https://www.example.com", "myCookie");

// 清除所有 Cookie
cookieManager.DeleteCookies();

// Cookie 访问器实现
public class CookieVisitor : ICookieVisitor
{
    public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
    {
        Console.WriteLine($"Cookie: {cookie.Name} = {cookie.Value}");
        
        // 设置 deleteCookie = true 可以删除该 Cookie
        deleteCookie = false;
        
        // 返回 true 继续访问下一个
        return true;
    }
    
    public void Dispose()
    {
    }
}
```

### 代理设置

```csharp
// 全局代理设置（在 Cef.Initialize 之前）
var settings = new CefSettings();
settings.CefCommandLineArgs.Add("proxy-server", "http://proxy.example.com:8080");

// 或者使用请求上下文设置代理
var requestContextSettings = new RequestContextSettings();
using (var requestContext = new RequestContext(requestContextSettings))
{
    // 设置代理
    var proxyOptions = new ProxyOptions
    {
        Scheme = "http",
        Host = "proxy.example.com",
        Port = 8080
    };
    
    var success = await requestContext.SetProxyAsync(null, proxyOptions);
    
    // 使用此请求上下文创建浏览器
    var browser = new ChromiumWebBrowser("https://www.google.com");
    browser.RequestContext = requestContext;
}

// 清除代理
await requestContext.SetProxyAsync(null, null);
```

### 截图功能

```csharp
// WinForms 截图
public async Task<Bitmap> TakeScreenshotAsync(ChromiumWebBrowser browser)
{
    var screenshot = await browser.CaptureScreenshotAsync();
    using (var ms = new MemoryStream(screenshot))
    {
        return new Bitmap(ms);
    }
}

// OffScreen 截图
public async Task TakeScreenshotOffScreenAsync()
{
    using (var browser = new ChromiumWebBrowser("https://www.example.com"))
    {
        await browser.WaitForInitialLoadAsync();
        
        // 获取页面内容大小
        var contentSize = await browser.GetContentSizeAsync();
        
        // 设置视口
        var viewport = new Viewport
        {
            Width = contentSize.Width,
            Height = contentSize.Height,
            Scale = 1.0
        };
        
        // 截图
        var screenshot = await browser.CaptureScreenshotAsync(viewport: viewport);
        
        // 保存
        var path = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.Desktop), "screenshot.png");
        File.WriteAllBytes(path, screenshot);
        
        Console.WriteLine($"截图已保存: {path}");
    }
}

// 截取特定元素
public async Task<Bitmap> CaptureElementAsync(ChromiumWebBrowser browser, string selector)
{
    // 获取元素位置和大小
    var script = $@"
        (function() {{
            var element = document.querySelector('{selector}');
            if (element) {{
                var rect = element.getBoundingClientRect();
                return {{
                    x: rect.left,
                    y: rect.top,
                    width: rect.width,
                    height: rect.height
                }};
            }}
            return null;
        }})()
    ";
    
    var result = await browser.EvaluateScriptAsync(script);
    if (result.Success && result.Result != null)
    {
        dynamic rect = result.Result;
        
        // 截取整个页面
        var fullScreenshot = await browser.CaptureScreenshotAsync();
        using (var ms = new MemoryStream(fullScreenshot))
        using (var fullBitmap = new Bitmap(ms))
        {
            // 裁剪指定区域
            var cropRect = new Rectangle(
                (int)rect.x,
                (int)rect.y,
                (int)rect.width,
                (int)rect.height
            );
            
            return fullBitmap.Clone(cropRect, fullBitmap.PixelFormat);
        }
    }
    
    return null;
}
```

### 打印 PDF

```csharp
// 打印页面为 PDF
public async Task PrintToPdfAsync(ChromiumWebBrowser browser, string outputPath)
{
    // 等待页面加载完成
    await browser.WaitForInitialLoadAsync();
    
    // 配置 PDF 打印设置
    var settings = new PdfPrintSettings
    {
        MarginTop = 10,
        MarginBottom = 10,
        MarginLeft = 10,
        MarginRight = 10,
        PageWidth = 210000, // A4 宽度（微米）
        PageHeight = 297000, // A4 高度（微米）
        BackgroundsEnabled = true,
        HeaderFooterEnabled = false,
        SelectionOnly = false,
        Landscape = false
    };
    
    // 打印为 PDF
    var success = await browser.PrintToPdfAsync(outputPath, settings);
    
    if (success)
    {
        Console.WriteLine($"PDF 已保存到: {outputPath}");
    }
    else
    {
        Console.WriteLine("PDF 打印失败");
    }
}

// 使用示例
var pdfPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
    "page.pdf"
);
await PrintToPdfAsync(browser, pdfPath);
```

## 常见问题

### 1. **为什么不能使用 AnyCPU 平台？**

CefSharp 包含本机 C++ 代码，需要指定具体的平台（x86、x64 或 ARM64）。

**解决方案：**
- 右键点击项目 → 属性 → 生成
- 将 "平台目标" 设置为 `x64`（推荐）或 `x86`

### 2. **应用程序启动时崩溃或显示"无法加载 DLL"错误**

这通常是因为缺少 Visual C++ 运行时。

**解决方案：**
- 安装相应版本的 VC++ Redistributable
- CefSharp 138+: [VC++ 2022](https://aka.ms/vs/17/release/vc_redist.x64.exe)
- CefSharp 92-137: [VC++ 2019](https://aka.ms/vs/16/release/vc_redist.x64.exe)

### 3. **页面显示空白**

可能的原因和解决方案：

**GPU 加速问题：**
```csharp
var settings = new CefSettings();
settings.CefCommandLineArgs.Add("disable-gpu");
settings.CefCommandLineArgs.Add("disable-gpu-compositing");
Cef.Initialize(settings);
```

**多进程模式问题：**
确保 `CefSharp.BrowserSubprocess.exe` 及其依赖文件存在于输出目录。

### 4. **如何调试 JavaScript？**

启用远程调试：

```csharp
var settings = new CefSettings
{
    RemoteDebuggingPort = 8088
};
Cef.Initialize(settings);
```

然后在 Chrome 浏览器中访问 `http://localhost:8088` 进行调试。

### 5. **如何处理 SSL 证书错误？**

```csharp
public class CustomRequestHandler : RequestHandler
{
    protected override bool OnCertificateError(IWebBrowser chromiumWebBrowser, 
        IBrowser browser, CefErrorCode errorCode, string requestUrl, 
        ISslInfo sslInfo, IRequestCallback callback)
    {
        // 警告：仅在开发环境中忽略证书错误
        callback.Continue(true); // 继续请求
        return true; // 表示已处理
    }
}
```

### 6. **浏览器控件在设计器中不显示**

CefSharp 控件不支持设计时渲染，必须在代码中动态创建。

### 7. **内存泄漏问题**

确保正确释放资源：

```csharp
// 在窗体关闭时
protected override void OnFormClosing(FormClosingEventArgs e)
{
    browser.Dispose();
    base.OnFormClosing(e);
}

// 应用程序退出时
Cef.Shutdown();
```

### 8. **多个浏览器实例共享 Cookie 和缓存**

默认情况下，所有浏览器实例共享全局请求上下文。要隔离：

```csharp
var requestContextSettings = new RequestContextSettings
{
    CachePath = "cache\\isolated"
};
var requestContext = new RequestContext(requestContextSettings);
var browser = new ChromiumWebBrowser("https://www.example.com");
browser.RequestContext = requestContext;
```

### 9. **如何在 ClickOnce 部署中使用 CefSharp？**

CefSharp 不支持 ClickOnce 部署，因为它包含大量本机文件。建议使用：
- NSIS 安装程序
- WiX Toolset
- Squirrel.Windows

### 10. **如何升级 CefSharp 版本？**

1. 更新 NuGet 包
2. 查看 [ChangeLog](https://github.com/cefsharp/CefSharp/wiki/ChangeLog) 了解破坏性更改
3. 更新代码以适应 API 变化
4. 测试所有功能

## 项目结构

本仓库包含以下主要项目：

```
CefSharp/
├── CefSharp/                      # 核心库（.NET 接口定义）
├── CefSharp.Core/                 # C++/CLI 实现层
├── CefSharp.WinForms/             # WinForms 控件
├── CefSharp.Wpf/                  # WPF 控件
├── CefSharp.OffScreen/            # OffScreen 渲染
├── CefSharp.BrowserSubprocess/    # 浏览器子进程
├── CefSharp.Example/              # 共享示例代码
├── CefSharp.WinForms.Example/     # WinForms 完整示例
├── CefSharp.Wpf.Example/          # WPF 完整示例
└── CefSharp.OffScreen.Example/    # OffScreen 完整示例
```

**运行示例项目：**

1. 克隆仓库
2. 使用 Visual Studio 2022 打开 `CefSharp3.sln`
3. 将示例项目设为启动项目
4. 编译并运行（选择 x64 或 x86 平台）

## 文档资源

- **官方网站**: [https://cefsharp.github.io/](https://cefsharp.github.io/)
- **GitHub Wiki**: [https://github.com/cefsharp/CefSharp/wiki](https://github.com/cefsharp/CefSharp/wiki)
- **API 文档**: [https://cefsharp.github.io/api/](https://cefsharp.github.io/api/)
- **快速开始指南**: [Quick Start Guide](https://github.com/cefsharp/CefSharp/wiki/Quick-Start)
- **通用使用指南**: [General Usage Guide](https://github.com/cefsharp/CefSharp/wiki/General-Usage)
- **常见问题**: [FAQ](https://github.com/cefsharp/CefSharp/wiki/Frequently-asked-questions)
- **更新日志**: [ChangeLog](https://github.com/cefsharp/CefSharp/wiki/ChangeLog)
- **最小示例项目**: [CefSharp.MinimalExample](https://github.com/cefsharp/CefSharp.MinimalExample/)

## 社区支持

有问题？需要帮助？

- **GitHub Discussions**: [提问和讨论](https://github.com/cefsharp/CefSharp/discussions) - CefSharp 特定问题的首选地
- **Stack Overflow**: [cefsharp 标签](https://stackoverflow.com/questions/tagged/cefsharp) - HTML/JavaScript/C# 通用问题
- **CEF 论坛**: [Chromium Embedded Framework Forum](https://magpcss.org/ceforum/viewforum.php?f=18) - CEF 底层问题
- **问题跟踪**: [GitHub Issues](https://github.com/cefsharp/CefSharp/issues) - 仅用于报告 Bug

**提问前请：**
1. 搜索已有的问题和讨论
2. 查阅 FAQ 和 Wiki
3. 准备可重现的最小示例代码

## 许可证

CefSharp 使用 [BSD 3-Clause 许可证](LICENSE)，可以自由用于商业和开源项目。

## 支持项目

如果 CefSharp 对你的项目有帮助，请考虑赞助项目维护者：

- **GitHub Sponsors**: [赞助 @amaitland](https://github.com/sponsors/amaitland)
- **PayPal**: [一次性捐赠](https://paypal.me/AlexMaitland)

即使是小额月度捐赠（如 $25）也能为项目持续发展提供巨大帮助。

## 相关项目

- **CEF**: [Chromium Embedded Framework](https://bitbucket.org/chromiumembedded/cef)
- **CefGlue**: [基于 P/Invoke 的 CEF 封装](https://gitlab.com/xiliumhq/chromiumembedded/cefglue)
- **Chromely**: [使用 CEF 构建跨平台桌面应用](https://github.com/chromelyapps/Chromely)
- **SharpBrowser**: [基于 CefSharp 的完整浏览器](https://github.com/sharpbrowser/SharpBrowser)

---

## 快速参考

### 初始化模板

```csharp
var settings = new CefSettings
{
    CachePath = Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.LocalApplicationData), "MyApp\\Cache"),
    RemoteDebuggingPort = 8088,
    UserAgent = "MyApp/1.0"
};

Cef.Initialize(settings);
```

### 创建浏览器

```csharp
// WinForms
var browser = new ChromiumWebBrowser("https://www.google.com")
{
    Dock = DockStyle.Fill
};
form.Controls.Add(browser);

// WPF
<wpf:ChromiumWebBrowser x:Name="Browser" Address="https://www.google.com"/>
```

### 导航

```csharp
browser.Load("https://www.example.com");
browser.Back();
browser.Forward();
browser.Reload();
browser.Stop();
```

### JavaScript 执行

```csharp
// 执行不需要返回值
browser.ExecuteScriptAsync("console.log('Hello')");

// 执行并获取返回值
var result = await browser.EvaluateScriptAsync("document.title");
if (result.Success)
{
    Console.WriteLine(result.Result);
}
```

### 对象绑定

```csharp
// 注册
browser.JavascriptObjectRepository.Register("api", new MyApi(), isAsync: true);

// JavaScript 中使用
await CefSharp.BindObjectAsync("api");
var result = await api.myMethod();
```

---

希望这份中文使用说明能帮助你快速上手 CefSharp！

如有任何问题，欢迎在 [GitHub Discussions](https://github.com/cefsharp/CefSharp/discussions) 中提问。
