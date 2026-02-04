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
  - [商业代理服务配置](#商业代理服务配置)
  - [IP 代理池集成](#ip-代理池集成)
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
- ✅ **多版本 .NET 支持** - 支持 .NET Framework 4.6.2+ 和 .NET 6+
- ✅ **多种集成方式** - WinForms、WPF、OffScreen 渲染
- ✅ **JavaScript 互操作** - C# 与 JavaScript 双向通信
- ✅ **自定义请求处理** - 拦截和处理网络请求
- ✅ **离屏渲染** - 无 UI 的后台页面渲染和截图
- ✅ **多进程架构** - 稳定性和安全性更高
- ✅ **开源免费** - BSD 许可证，可用于商业和开源项目

## 系统要求

### .NET Framework 项目

- **操作系统**: Windows 7 SP1 或更高版本
- **.NET 版本**: .NET Framework 4.6.2 或更高版本（推荐 4.8）
- **Visual C++ 运行时**:
  - 对于 CefSharp 版本 138 及以上: VC++ 2022 Redistributable
  - 对于 CefSharp 版本 92-137: VC++ 2019 Redistributable
  - 对于 CefSharp 版本 91 及以下: VC++ 2015 Redistributable
- **架构**: x86、x64 或 ARM64

> **注意**: CefSharp 仅支持 Windows 操作系统。最低要求为 .NET Framework 4.6.2，但建议使用 .NET Framework 4.8 以获得更好的性能和兼容性。

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

### 商业代理服务配置

许多开发者使用商业代理服务（如思叶天、芝麻代理、快代理等）。以下是如何在 CefSharp 中配置这些服务的详细指南。

#### 1. 基本配置方法

**方法一：直接使用代理地址**

如果代理服务提供了固定的代理地址和端口：

```csharp
using CefSharp;
using CefSharp.WinForms;

public async Task ConfigureCommercialProxy()
{
    // 示例：思叶天代理地址格式通常为 IP:端口
    // 例如: 123.45.67.89:8080
    
    var requestContextSettings = new RequestContextSettings
    {
        CachePath = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CefSharp", "ProxyCache"
        )
    };
    
    var requestContext = new RequestContext(requestContextSettings);
    
    // 配置代理（替换为您从思叶天获取的实际代理地址）
    var proxyHost = "123.45.67.89";  // 您的代理IP
    var proxyPort = 8080;             // 您的代理端口
    
    var result = await requestContext.SetProxyAsync("http", proxyHost, proxyPort);
    
    if (result.Success)
    {
        Console.WriteLine("代理设置成功");
        
        // 创建浏览器并使用代理
        var browser = new ChromiumWebBrowser("https://www.baidu.com")
        {
            RequestContext = requestContext
        };
        
        // 添加到窗体
        // this.Controls.Add(browser);
    }
    else
    {
        Console.WriteLine($"代理设置失败: {result.ErrorMessage}");
    }
}
```

**方法二：使用代理认证**

如果代理需要用户名和密码认证（思叶天通常支持白名单IP，也可以使用用户名密码认证）：

```csharp
using CefSharp;
using CefSharp.Handler;

public class CommercialProxyRequestHandler : RequestHandler
{
    private string _username;
    private string _password;
    
    public CommercialProxyRequestHandler(string username, string password)
    {
        _username = username;
        _password = password;
    }
    
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
        if (isProxy)
        {
            // 提供代理认证信息
            callback.Continue(_username, _password);
            return true;
        }
        
        return false;
    }
}

// 使用方法
public async Task ConfigureAuthenticatedProxy()
{
    var requestContext = new RequestContext();
    
    // 设置代理
    await requestContext.SetProxyAsync("http", "your-proxy-ip.com", 8080);
    
    // 创建浏览器
    var browser = new ChromiumWebBrowser("https://www.baidu.com")
    {
        RequestContext = requestContext,
        RequestHandler = new CommercialProxyRequestHandler("your_username", "your_password")
    };
}
```

**方法三：处理 `user:pass@host:port` 格式的代理**

许多代理服务（如思叶天）提供形如 `username:password@host:port` 的代理地址。**重要提示**：Chromium **不支持**在命令行参数中直接使用 `username:password@host:port` 格式，必须分成两步处理：

1. **第一步**：设置代理服务器地址 (`host:port`)
2. **第二步**：通过 `RequestHandler` 处理身份验证 (`username:password`)

以下是完整的解决方案：

```csharp
using System;
using CefSharp;
using CefSharp.Handler;
using CefSharp.WinForms;

/// <summary>
/// 代理认证辅助类
/// 用于解析和管理 user:pass@host:port 格式的代理
/// </summary>
public class ProxyAuthHelper
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    /// <summary>
    /// 解析 user:pass@host:port 格式的代理字符串
    /// 例如: "22VWD-1:531246@sk1.siyetian.com:6153"
    /// </summary>
    public static ProxyAuthHelper Parse(string proxyString)
    {
        if (string.IsNullOrWhiteSpace(proxyString))
            throw new ArgumentException("代理字符串不能为空", nameof(proxyString));
        
        // 按 @ 分割成认证部分和服务器部分
        var parts = proxyString.Split('@');
        if (parts.Length != 2)
            throw new ArgumentException("代理格式错误，应为 user:pass@host:port", nameof(proxyString));
        
        var authPart = parts[0];    // user:pass
        var serverPart = parts[1];  // host:port
        
        // 解析认证信息
        var authParts = authPart.Split(':');
        if (authParts.Length < 2)
            throw new ArgumentException("认证格式错误，应为 username:password", nameof(proxyString));
        
        // 处理密码中包含冒号的情况
        var username = authParts[0];
        var password = string.Join(":", authParts, 1, authParts.Length - 1);
        
        // 解析服务器地址
        var serverParts = serverPart.Split(':');
        if (serverParts.Length != 2)
            throw new ArgumentException("服务器格式错误，应为 host:port", nameof(proxyString));
        
        return new ProxyAuthHelper
        {
            Username = authParts[0],
            Password = authParts[1],
            Host = serverParts[0],
            Port = int.Parse(serverParts[1])
        };
    }
    
    /// <summary>
    /// 获取服务器地址（不含认证信息）
    /// 格式: host:port
    /// </summary>
    public string GetServerAddress()
    {
        return $"{Host}:{Port}";
    }
}

/// <summary>
/// 代理认证请求处理器
/// </summary>
public class ProxyAuthRequestHandler : RequestHandler
{
    private readonly string _username;
    private readonly string _password;
    
    public ProxyAuthRequestHandler(string username, string password)
    {
        _username = username;
        _password = password;
    }
    
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
        // 关键点：检查 isProxy 为 true，说明是代理服务器在要求认证
        if (isProxy)
        {
            // 自动填入账号密码
            callback.Continue(_username, _password);
            return true; // 告诉 CefSharp 我们已经处理了认证
        }
        
        // 如果是网站本身的弹窗验证，走默认逻辑
        return base.GetAuthCredentials(chromiumWebBrowser, browser, originUrl, 
            isProxy, host, port, realm, scheme, callback);
    }
}

// ============== 使用示例 ==============

/// <summary>
/// 示例 1：全局代理配置（程序启动时）
/// 适用于固定代理的场景
/// </summary>
public class GlobalProxyExample
{
    public void ConfigureGlobalProxy()
    {
        // 1. 解析代理字符串（思叶天格式示例）
        string proxyString = "22VWD-1:531246@sk1.siyetian.com:6153";
        var proxyInfo = ProxyAuthHelper.Parse(proxyString);
        
        Console.WriteLine($"代理服务器: {proxyInfo.Host}:{proxyInfo.Port}");
        Console.WriteLine($"用户名: {proxyInfo.Username}");
        
        // 2. 设置 Cef 全局代理（只设置服务器地址，不包含用户名密码）
        var settings = new CefSettings();
        settings.CefCommandLineArgs.Add("proxy-server", proxyInfo.GetServerAddress());
        
        // 3. 初始化 CefSharp
        Cef.Initialize(settings);
        
        // 4. 创建浏览器并设置认证处理器
        var browser = new ChromiumWebBrowser("https://www.baidu.com");
        // 【关键】将用户名密码传给 RequestHandler
        browser.RequestHandler = new ProxyAuthRequestHandler(
            proxyInfo.Username, 
            proxyInfo.Password
        );
        
        // 添加到窗体
        // this.Controls.Add(browser);
    }
}

/// <summary>
/// 示例 2：动态代理配置（运行时切换）
/// 适用于需要切换代理的场景
/// </summary>
public class DynamicProxyExample
{
    private ChromiumWebBrowser _browser;
    private IRequestContext _requestContext;
    
    public async Task ConfigureDynamicProxyAsync()
    {
        // 1. 解析代理字符串
        string proxyString = "22VWD-1:531246@sk1.siyetian.com:6153";
        var proxyInfo = ProxyAuthHelper.Parse(proxyString);
        
        // 2. 创建请求上下文
        _requestContext = new RequestContext();
        
        // 3. 设置代理（只设置服务器地址）
        var result = await _requestContext.SetProxyAsync(
            "http",              // 协议
            proxyInfo.Host,      // 主机
            proxyInfo.Port       // 端口
        );
        
        if (!result.Success)
        {
            Console.WriteLine($"代理设置失败: {result.ErrorMessage}");
            return;
        }
        
        Console.WriteLine("代理设置成功");
        
        // 4. 创建浏览器
        _browser = new ChromiumWebBrowser("https://www.baidu.com")
        {
            RequestContext = _requestContext,
            // 【关键】设置认证处理器
            RequestHandler = new ProxyAuthRequestHandler(
                proxyInfo.Username, 
                proxyInfo.Password
            )
        };
        
        // 添加到窗体
        // this.Controls.Add(_browser);
    }
    
    /// <summary>
    /// 切换到新的代理
    /// </summary>
    public async Task SwitchProxyAsync(string newProxyString)
    {
        var proxyInfo = ProxyAuthHelper.Parse(newProxyString);
        
        // 更新代理设置
        await _requestContext.SetProxyAsync(
            "http", 
            proxyInfo.Host, 
            proxyInfo.Port
        );
        
        // 更新认证处理器
        _browser.RequestHandler = new ProxyAuthRequestHandler(
            proxyInfo.Username, 
            proxyInfo.Password
        );
        
        // 重新加载当前页面以使用新代理
        _browser.Reload();
    }
}

/// <summary>
/// 示例 3：完整的 WinForms 应用程序
/// </summary>
public class AuthProxyBrowserForm : Form
{
    private ChromiumWebBrowser _browser;
    private IRequestContext _requestContext;
    private TextBox _proxyTextBox;
    private Button _setProxyButton;
    private TextBox _urlTextBox;
    private Button _goButton;
    private Label _statusLabel;
    
    public AuthProxyBrowserForm()
    {
        InitializeUI();
        InitializeBrowser();
    }
    
    private void InitializeUI()
    {
        this.Width = 1200;
        this.Height = 800;
        this.Text = "CefSharp - 带认证的代理示例";
        
        var toolPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 100
        };
        
        // 代理输入
        var proxyLabel = new Label
        {
            Text = "代理地址 (user:pass@host:port):",
            Location = new System.Drawing.Point(10, 10),
            Width = 200
        };
        
        _proxyTextBox = new TextBox
        {
            Location = new System.Drawing.Point(220, 8),
            Width = 400,
            Text = "22VWD-1:531246@sk1.siyetian.com:6153"
        };
        
        _setProxyButton = new Button
        {
            Text = "设置代理",
            Location = new System.Drawing.Point(630, 6),
            Width = 80
        };
        _setProxyButton.Click += async (s, e) => await SetProxyAsync();
        
        // URL 输入
        var urlLabel = new Label
        {
            Text = "访问地址:",
            Location = new System.Drawing.Point(10, 45),
            Width = 80
        };
        
        _urlTextBox = new TextBox
        {
            Location = new System.Drawing.Point(100, 43),
            Width = 520,
            Text = "https://www.baidu.com"
        };
        
        _goButton = new Button
        {
            Text = "访问",
            Location = new System.Drawing.Point(630, 41),
            Width = 80
        };
        _goButton.Click += (s, e) => _browser?.Load(_urlTextBox.Text);
        
        // 状态标签
        _statusLabel = new Label
        {
            Location = new System.Drawing.Point(10, 75),
            Width = 700,
            Text = "请输入代理地址并点击'设置代理'"
        };
        
        toolPanel.Controls.AddRange(new Control[] {
            proxyLabel, _proxyTextBox, _setProxyButton,
            urlLabel, _urlTextBox, _goButton,
            _statusLabel
        });
        
        this.Controls.Add(toolPanel);
    }
    
    private void InitializeBrowser()
    {
        _requestContext = new RequestContext();
        
        _browser = new ChromiumWebBrowser("")
        {
            Dock = DockStyle.Fill,
            RequestContext = _requestContext
        };
        
        _browser.LoadingStateChanged += OnLoadingStateChanged;
        
        this.Controls.Add(_browser);
    }
    
    private async Task SetProxyAsync()
    {
        try
        {
            _setProxyButton.Enabled = false;
            _statusLabel.Text = "正在设置代理...";
            
            // 解析代理字符串
            var proxyInfo = ProxyAuthHelper.Parse(_proxyTextBox.Text);
            
            // 设置代理服务器
            var result = await _requestContext.SetProxyAsync(
                "http", 
                proxyInfo.Host, 
                proxyInfo.Port
            );
            
            if (!result.Success)
            {
                _statusLabel.Text = $"代理设置失败: {result.ErrorMessage}";
                MessageBox.Show($"代理设置失败: {result.ErrorMessage}", "错误");
                return;
            }
            
            // 设置认证处理器
            _browser.RequestHandler = new ProxyAuthRequestHandler(
                proxyInfo.Username, 
                proxyInfo.Password
            );
            
            _statusLabel.Text = $"代理设置成功: {proxyInfo.GetServerAddress()} (用户: {proxyInfo.Username})";
            
            // 如果已经有页面，重新加载
            if (!string.IsNullOrEmpty(_browser.Address))
            {
                _browser.Reload();
            }
        }
        catch (Exception ex)
        {
            _statusLabel.Text = $"错误: {ex.Message}";
            MessageBox.Show($"设置代理失败: {ex.Message}", "错误");
        }
        finally
        {
            _setProxyButton.Enabled = true;
        }
    }
    
    private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
    {
        if (!e.IsLoading)
        {
            this.Invoke(new Action(() =>
            {
                _statusLabel.Text = $"页面加载完成: {_browser.Address}";
            }));
        }
    }
    
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _browser?.Dispose();
        _requestContext?.Dispose();
        base.OnFormClosing(e);
    }
}

// ============== 使用主程序 ==============
public class Program
{
    [STAThread]
    static void Main()
    {
        // 如果使用全局代理配置
        // var example = new GlobalProxyExample();
        // example.ConfigureGlobalProxy();
        
        // 如果使用动态代理配置（推荐）
        var settings = new CefSettings();
        Cef.Initialize(settings);
        
        Application.EnableVisualStyles();
        Application.Run(new AuthProxyBrowserForm());
        
        Cef.Shutdown();
    }
}
```

**重要说明**：

1. **为什么不能直接使用 `user:pass@host:port`？**
   - Chromium 的命令行参数 `--proxy-server` 不支持包含用户名和密码
   - 这是 Chromium 的安全设计，避免密码暴露在命令行中
   
2. **两步配置的原理**：
   - 第一步：通过命令行参数或 API 告诉 Chromium 代理服务器地址
   - 第二步：当代理服务器返回 407 状态码（需要认证）时，`GetAuthCredentials` 会被调用
   - 在 `GetAuthCredentials` 中检查 `isProxy` 参数为 `true`，然后提供用户名密码

3. **全局 vs 动态配置**：
   - **全局配置**：适用于程序启动时就确定代理，所有浏览器实例共享
   - **动态配置**：适用于运行时切换代理，每个浏览器可以有不同的代理

4. **思叶天代理格式**：
   - 思叶天通常提供 `username:password@host:port` 格式
   - 使用 `ProxyAuthHelper.Parse()` 方法可以轻松解析
   - 示例：`22VWD-1:531246@sk1.siyetian.com:6153`

5. **常见问题**：
   - **Q**：为什么页面加载很慢？
   - **A**：检查代理服务器是否正常，尝试在浏览器中手动配置相同代理测试
   
   - **Q**：认证失败怎么办？
   - **A**：确认用户名密码正确，检查代理服务是否启用了 IP 白名单
   
   - **Q**：如何判断是否在使用代理？
   - **A**：访问 `https://api.ipify.org` 或 `https://www.whatismyip.com` 查看当前 IP



#### 2. 从 API 获取代理地址

大多数商业代理服务提供 API 来获取代理地址。以下是完整的集成示例：

```csharp
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using CefSharp;
using CefSharp.WinForms;

/// <summary>
/// 商业代理服务客户端（以思叶天为例）
/// </summary>
public class SiyetianProxyClient : IDisposable
{
    private readonly string _apiUrl;
    private readonly HttpClient _httpClient;
    
    public SiyetianProxyClient(string apiUrl)
    {
        _apiUrl = apiUrl;
        _httpClient = new HttpClient();
    }
    
    /// <summary>
    /// 从 API 获取代理地址
    /// </summary>
    public async Task<ProxyAddress> GetProxyAsync()
    {
        try
        {
            // 调用代理服务API获取代理地址
            // 实际 API 格式需要参考思叶天的API文档
            var response = await _httpClient.GetStringAsync(_apiUrl);
            
            // 解析响应 - 格式示例: {"ip":"123.45.67.89","port":8080}
            // 实际格式可能不同，需要根据API文档调整
            var proxyData = JsonSerializer.Deserialize<ProxyApiResponse>(response);
            
            return new ProxyAddress
            {
                Host = proxyData.Ip,
                Port = proxyData.Port,
                Scheme = "http"
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"获取代理失败: {ex.Message}");
            return null;
        }
    }
    
    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}

// API 响应模型（根据实际API调整）
public class ProxyApiResponse
{
    public string Ip { get; set; }
    public int Port { get; set; }
    public string Expire { get; set; }  // 过期时间
}

// 代理地址模型
public class ProxyAddress
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Scheme { get; set; }
}

// 使用示例
public class SiyetianProxyExample
{
    public async Task ConfigureProxyFromApi()
    {
        // 1. 创建代理客户端（使用您的API地址，需替换 YOUR_KEY 为实际密钥）
        using (var proxyClient = new SiyetianProxyClient("https://api.siyetian.com/get?type=http&num=1&key=YOUR_KEY"))
        {
            // 2. 获取代理地址
            var proxy = await proxyClient.GetProxyAsync();
        
        if (proxy == null)
        {
            Console.WriteLine("无法获取代理地址");
            return;
        }
        
        Console.WriteLine($"获取到代理: {proxy.Host}:{proxy.Port}");
        
        // 3. 配置 CefSharp 使用代理
        var requestContext = new RequestContext();
        var result = await requestContext.SetProxyAsync(proxy.Scheme, proxy.Host, proxy.Port);
        
        if (result.Success)
        {
            // 4. 创建浏览器
            var browser = new ChromiumWebBrowser("https://www.baidu.com")
            {
                RequestContext = requestContext
            };
            
            Console.WriteLine("代理配置成功，浏览器已创建");
        }
        else
        {
            Console.WriteLine($"代理配置失败: {result.ErrorMessage}");
        }
    }
}

#### 3. 自动轮换商业代理

如果您购买了多个代理或有代理池，可以实现自动轮换：

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

/// <summary>
/// 商业代理轮换管理器
/// </summary>
public class CommercialProxyRotator
{
    private List<ProxyAddress> _proxies = new List<ProxyAddress>();
    private int _currentIndex = 0;
    private readonly SiyetianProxyClient _apiClient;
    
    public CommercialProxyRotator(string apiUrl)
    {
        _apiClient = new SiyetianProxyClient(apiUrl);
    }
    
    /// <summary>
    /// 从API获取指定数量的代理
    /// </summary>
    public async Task<bool> LoadProxiesAsync(int count = 5)
    {
        _proxies.Clear();
        
        for (int i = 0; i < count; i++)
        {
            var proxy = await _apiClient.GetProxyAsync();
            if (proxy != null)
            {
                _proxies.Add(proxy);
                Console.WriteLine($"已加载代理 {i + 1}/{count}: {proxy.Host}:{proxy.Port}");
            }
            
            // 避免请求过快，稍作延迟
            await Task.Delay(1000);
        }
        
        return _proxies.Any();
    }
    
    /// <summary>
    /// 获取下一个代理
    /// </summary>
    public ProxyAddress GetNextProxy()
    {
        if (!_proxies.Any())
            return null;
        
        var proxy = _proxies[_currentIndex];
        _currentIndex = (_currentIndex + 1) % _proxies.Count;
        
        return proxy;
    }
    
    /// <summary>
    /// 刷新单个失效的代理
    /// </summary>
    public async Task<bool> RefreshProxyAsync(ProxyAddress failedProxy)
    {
        var newProxy = await _apiClient.GetProxyAsync();
        if (newProxy != null)
        {
            var index = _proxies.FindIndex(p => 
                p.Host == failedProxy.Host && p.Port == failedProxy.Port);
            
            if (index >= 0)
            {
                _proxies[index] = newProxy;
                Console.WriteLine($"已刷新代理: {newProxy.Host}:{newProxy.Port}");
                return true;
            }
        }
        
        return false;
    }
}

// 使用示例
public class CommercialProxyRotationExample
{
    private CommercialProxyRotator _rotator;
    private ChromiumWebBrowser _browser;
    private IRequestContext _requestContext;
    
    public async Task InitializeAsync()
    {
        // 1. 创建轮换器（需替换 YOUR_KEY 为实际密钥）
        _rotator = new CommercialProxyRotator("https://api.siyetian.com/get?type=http&num=1&key=YOUR_KEY");
        
        // 2. 加载代理
        bool loaded = await _rotator.LoadProxiesAsync(5);
        
        if (!loaded)
        {
            Console.WriteLine("无法加载代理");
            return;
        }
        
        // 3. 使用第一个代理
        await RotateProxyAsync();
        
        // 4. 监听加载错误，自动切换代理
        _browser.LoadError += OnLoadError;
    }
    
    private async Task RotateProxyAsync()
    {
        var proxy = _rotator.GetNextProxy();
        
        if (proxy == null)
        {
            Console.WriteLine("没有可用的代理");
            return;
        }
        
        // 创建新的请求上下文（如果不存在）
        if (_requestContext == null)
        {
            _requestContext = new RequestContext();
        }
        
        // 设置代理
        var result = await _requestContext.SetProxyAsync(proxy.Scheme, proxy.Host, proxy.Port);
        
        if (result.Success)
        {
            Console.WriteLine($"已切换到代理: {proxy.Host}:{proxy.Port}");
            
            // 创建或更新浏览器
            if (_browser == null)
            {
                _browser = new ChromiumWebBrowser("https://www.baidu.com")
                {
                    RequestContext = _requestContext
                };
            }
            else
            {
                _browser.Reload();
            }
        }
    }
    
    private async void OnLoadError(object sender, LoadErrorEventArgs e)
    {
        if (e.ErrorCode != CefErrorCode.Aborted)
        {
            Console.WriteLine($"加载错误 ({e.ErrorCode})，尝试切换代理...");
            await RotateProxyAsync();
        }
    }
}
```

#### 4. 完整的 WinForms 示例

以下是一个完整的 Windows Forms 应用程序示例，展示如何使用思叶天等商业代理服务：

```csharp
using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using CefSharp;
using CefSharp.WinForms;

public class CommercialProxyBrowserForm : Form
{
    private ChromiumWebBrowser _browser;
    private IRequestContext _requestContext;
    private CommercialProxyRotator _proxyRotator;
    
    private TextBox _apiUrlTextBox;
    private TextBox _proxyTextBox;
    private Button _loadProxiesButton;
    private Button _rotateButton;
    private Button _goButton;
    private TextBox _urlTextBox;
    private Label _statusLabel;
    
    public CommercialProxyBrowserForm()
    {
        InitializeUI();
        InitializeCefSharp();
    }
    
    private void InitializeUI()
    {
        this.Width = 1200;
        this.Height = 800;
        this.Text = "CefSharp - 商业代理服务示例（思叶天等）";
        
        var toolPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 120
        };
        
        // API URL 输入
        var apiLabel = new Label
        {
            Text = "代理API地址:",
            Location = new System.Drawing.Point(10, 10),
            Width = 100
        };
        
        _apiUrlTextBox = new TextBox
        {
            Location = new System.Drawing.Point(120, 8),
            Width = 600,
            Text = "https://api.siyetian.com/get?type=http&num=1&key=YOUR_KEY"
        };
        
        _loadProxiesButton = new Button
        {
            Text = "加载代理",
            Location = new System.Drawing.Point(730, 6),
            Width = 80
        };
        _loadProxiesButton.Click += async (s, e) => await LoadProxiesAsync();
        
        // 当前代理显示
        var proxyLabel = new Label
        {
            Text = "当前代理:",
            Location = new System.Drawing.Point(10, 40),
            Width = 100
        };
        
        _proxyTextBox = new TextBox
        {
            Location = new System.Drawing.Point(120, 38),
            Width = 500,
            ReadOnly = true
        };
        
        _rotateButton = new Button
        {
            Text = "切换代理",
            Location = new System.Drawing.Point(630, 36),
            Width = 80
        };
        _rotateButton.Click += async (s, e) => await RotateProxyAsync();
        
        // URL 输入
        var urlLabel = new Label
        {
            Text = "访问地址:",
            Location = new System.Drawing.Point(10, 70),
            Width = 100
        };
        
        _urlTextBox = new TextBox
        {
            Location = new System.Drawing.Point(120, 68),
            Width = 500,
            Text = "https://www.baidu.com"
        };
        
        _goButton = new Button
        {
            Text = "访问",
            Location = new System.Drawing.Point(630, 66),
            Width = 80
        };
        _goButton.Click += (s, e) => _browser?.Load(_urlTextBox.Text);
        
        // 状态标签
        _statusLabel = new Label
        {
            Location = new System.Drawing.Point(120, 95),
            Width = 800,
            Text = "请先加载代理..."
        };
        
        toolPanel.Controls.AddRange(new Control[] {
            apiLabel, _apiUrlTextBox, _loadProxiesButton,
            proxyLabel, _proxyTextBox, _rotateButton,
            urlLabel, _urlTextBox, _goButton,
            _statusLabel
        });
        
        this.Controls.Add(toolPanel);
    }
    
    private void InitializeCefSharp()
    {
        _requestContext = new RequestContext();
        
        _browser = new ChromiumWebBrowser("")
        {
            Dock = DockStyle.Fill,
            RequestContext = _requestContext
        };
        
        _browser.LoadingStateChanged += OnLoadingStateChanged;
        _browser.LoadError += OnLoadError;
        
        this.Controls.Add(_browser);
    }
    
    private async Task LoadProxiesAsync()
    {
        try
        {
            _loadProxiesButton.Enabled = false;
            _statusLabel.Text = "正在加载代理...";
            
            _proxyRotator = new CommercialProxyRotator(_apiUrlTextBox.Text);
            bool success = await _proxyRotator.LoadProxiesAsync(3);
            
            if (success)
            {
                _statusLabel.Text = "代理加载成功，点击'切换代理'使用";
                await RotateProxyAsync();
            }
            else
            {
                _statusLabel.Text = "代理加载失败";
                MessageBox.Show("无法加载代理，请检查API地址", "错误");
            }
        }
        catch (Exception ex)
        {
            _statusLabel.Text = $"加载失败: {ex.Message}";
            MessageBox.Show($"加载代理失败: {ex.Message}", "错误");
        }
        finally
        {
            _loadProxiesButton.Enabled = true;
        }
    }
    
    private async Task RotateProxyAsync()
    {
        if (_proxyRotator == null)
        {
            MessageBox.Show("请先加载代理", "提示");
            return;
        }
        
        try
        {
            var proxy = _proxyRotator.GetNextProxy();
            if (proxy == null)
            {
                MessageBox.Show("没有可用的代理", "错误");
                return;
            }
            
            var result = await _requestContext.SetProxyAsync(
                proxy.Scheme, 
                proxy.Host, 
                proxy.Port
            );
            
            if (result.Success)
            {
                _proxyTextBox.Text = $"{proxy.Host}:{proxy.Port}";
                _statusLabel.Text = $"已切换到代理: {proxy.Host}:{proxy.Port}";
                
                if (!string.IsNullOrEmpty(_browser.Address))
                {
                    _browser.Reload();
                }
            }
            else
            {
                _statusLabel.Text = $"代理设置失败: {result.ErrorMessage}";
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"切换代理失败: {ex.Message}", "错误");
        }
    }
    
    private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
    {
        if (!e.IsLoading)
        {
            this.Invoke(new Action(() =>
            {
                _statusLabel.Text = "页面加载完成";
            }));
        }
    }
    
    private void OnLoadError(object sender, LoadErrorEventArgs e)
    {
        if (e.ErrorCode != CefErrorCode.Aborted && e.Frame.IsMain)
        {
            this.Invoke(new Action(async () =>
            {
                _statusLabel.Text = $"加载错误 ({e.ErrorCode})，尝试切换代理...";
                await Task.Delay(1000);
                await RotateProxyAsync();
            }));
        }
    }
    
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _browser?.Dispose();
        _requestContext?.Dispose();
        base.OnFormClosing(e);
    }
}
```

#### 5. 思叶天代理配置要点

**获取 API 地址：**
1. 登录思叶天网站 (https://www.siyetian.com/)
2. 进入用户中心
3. 获取您的 API 提取链接
4. API 链接格式通常为: `http://api.siyetian.com/get?type=http&num=1&key=YOUR_KEY`

**常见参数说明：**
- `type`: 代理类型 (http, https, socks5)
- `num`: 提取数量
- `key`: 您的密钥
- `time`: 代理有效时长（可选）
- `format`: 返回格式（json, txt等）

**注意事项：**
1. **IP白名单**: 某些代理服务需要您在后台添加服务器IP到白名单
2. **并发限制**: 注意API调用频率限制，避免过快请求
3. **代理有效期**: 商业代理通常有时效性，需要定期刷新
4. **失败重试**: 代理可能失效，建议实现自动切换机制
5. **流量计费**: 注意代理流量使用情况，避免超出套餐

**示例代码使用说明：**
```csharp
// 方式1: 直接使用固定代理
var result = await requestContext.SetProxyAsync("http", "123.45.67.89", 8080);

// 方式2: 从API获取代理
var client = new SiyetianProxyClient("YOUR_API_URL");
var proxy = await client.GetProxyAsync();
await requestContext.SetProxyAsync(proxy.Scheme, proxy.Host, proxy.Port);

// 方式3: 使用代理轮换器
var rotator = new CommercialProxyRotator("YOUR_API_URL");
await rotator.LoadProxiesAsync(5);
var proxy = rotator.GetNextProxy();
```

### IP 代理池集成

在实际应用中，您可能需要使用代理池来避免单一代理的限制。以下是完整的 IP 代理池实现方案：

#### 1. 代理池管理器

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 代理服务器信息
/// </summary>
public class ProxyInfo
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Scheme { get; set; } = "http"; // http, socks, socks4, socks5
    public string Username { get; set; }
    public string Password { get; set; }
    public int FailureCount { get; set; }
    public DateTime LastUsed { get; set; }
    public DateTime LastChecked { get; set; }
    public bool IsHealthy { get; set; } = true;
    
    public string GetProxyUrl()
    {
        return $"{Scheme}://{Host}:{Port}";
    }
    
    public override string ToString()
    {
        return $"{Scheme}://{Host}:{Port}";
    }
}

/// <summary>
/// 代理池管理器
/// </summary>
public class ProxyPool
{
    private List<ProxyInfo> _proxies = new List<ProxyInfo>();
    private int _currentIndex = 0;
    private readonly object _lock = new object();
    private readonly Random _random = new Random();
    private Timer _healthCheckTimer;
    
    /// <summary>
    /// 代理轮换模式
    /// </summary>
    public enum RotationMode
    {
        Sequential,  // 顺序轮换
        Random,      // 随机选择
        LeastUsed,   // 最少使用
        HealthBased  // 基于健康状态
    }
    
    public RotationMode Mode { get; set; } = RotationMode.Sequential;
    
    /// <summary>
    /// 最大失败次数，超过后代理将被标记为不健康
    /// </summary>
    public int MaxFailureCount { get; set; } = 3;
    
    /// <summary>
    /// 是否启用健康检查
    /// </summary>
    public bool EnableHealthCheck { get; set; } = true;
    
    /// <summary>
    /// 健康检查间隔（毫秒）
    /// </summary>
    public int HealthCheckInterval { get; set; } = 60000; // 1分钟
    
    public ProxyPool()
    {
        // 启动健康检查定时器
        _healthCheckTimer = new Timer(async _ => await PerformHealthCheckAsync(), 
            null, HealthCheckInterval, HealthCheckInterval);
    }
    
    /// <summary>
    /// 添加代理到池中
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
            
            _proxies.Add(proxy);
        }
    }
    
    /// <summary>
    /// 批量添加代理
    /// </summary>
    public void AddProxies(IEnumerable<ProxyInfo> proxies)
    {
        lock (_lock)
        {
            _proxies.AddRange(proxies);
        }
    }
    
    /// <summary>
    /// 从文件加载代理列表
    /// 格式：host:port 或 scheme://host:port 或 scheme://username:password@host:port
    /// </summary>
    public void LoadFromFile(string filePath)
    {
        var lines = System.IO.File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;
                
            try
            {
                var proxy = ParseProxyString(line.Trim());
                if (proxy != null)
                {
                    AddProxies(new[] { proxy });
                }
            }
            catch
            {
                // 忽略无效行
            }
        }
    }
    
    /// <summary>
    /// 解析代理字符串
    /// </summary>
    private ProxyInfo ParseProxyString(string proxyString)
    {
        var proxy = new ProxyInfo();
        
        // 处理 scheme://username:password@host:port 格式
        if (proxyString.Contains("://"))
        {
            var parts = proxyString.Split(new[] { "://" }, StringSplitOptions.None);
            proxy.Scheme = parts[0];
            proxyString = parts[1];
        }
        
        // 处理认证信息
        if (proxyString.Contains("@"))
        {
            var authParts = proxyString.Split('@');
            var credentials = authParts[0].Split(':');
            proxy.Username = credentials[0];
            proxy.Password = credentials.Length > 1 ? credentials[1] : "";
            proxyString = authParts[1];
        }
        
        // 处理 host:port
        var hostPort = proxyString.Split(':');
        proxy.Host = hostPort[0];
        proxy.Port = int.Parse(hostPort[1]);
        
        return proxy;
    }
    
    /// <summary>
    /// 获取下一个可用的代理
    /// </summary>
    public ProxyInfo GetNextProxy()
    {
        lock (_lock)
        {
            if (_proxies.Count == 0)
                return null;
            
            // 过滤健康的代理
            var healthyProxies = _proxies.Where(p => p.IsHealthy).ToList();
            if (healthyProxies.Count == 0)
            {
                // 如果没有健康的代理，重置所有代理的健康状态
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
                    selectedProxy = healthyProxies.OrderBy(p => p.LastUsed).First();
                    break;
                    
                case RotationMode.HealthBased:
                    selectedProxy = healthyProxies.OrderBy(p => p.FailureCount).First();
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
    /// 报告代理失败
    /// </summary>
    public void ReportFailure(ProxyInfo proxy)
    {
        lock (_lock)
        {
            var targetProxy = _proxies.FirstOrDefault(p => 
                p.Host == proxy.Host && p.Port == proxy.Port);
            
            if (targetProxy != null)
            {
                targetProxy.FailureCount++;
                
                if (targetProxy.FailureCount >= MaxFailureCount)
                {
                    targetProxy.IsHealthy = false;
                    Console.WriteLine($"代理 {targetProxy} 已被标记为不健康");
                }
            }
        }
    }
    
    /// <summary>
    /// 报告代理成功
    /// </summary>
    public void ReportSuccess(ProxyInfo proxy)
    {
        lock (_lock)
        {
            var targetProxy = _proxies.FirstOrDefault(p => 
                p.Host == proxy.Host && p.Port == proxy.Port);
            
            if (targetProxy != null)
            {
                targetProxy.FailureCount = 0;
                targetProxy.IsHealthy = true;
            }
        }
    }
    
    /// <summary>
    /// 执行健康检查
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
    /// 检查单个代理的健康状态
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
                client.Timeout = TimeSpan.FromSeconds(10);
                var response = await client.GetAsync("http://www.gstatic.com/generate_204");
                
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
    /// 清除所有代理
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
    /// 获取代理池统计信息
    /// </summary>
    public ProxyPoolStatistics GetStatistics()
    {
        lock (_lock)
        {
            return new ProxyPoolStatistics
            {
                TotalProxies = _proxies.Count,
                HealthyProxies = _proxies.Count(p => p.IsHealthy),
                UnhealthyProxies = _proxies.Count - _proxies.Count(p => p.IsHealthy),
                AverageFailureCount = _proxies.Any() ? _proxies.Average(p => p.FailureCount) : 0
            };
        }
    }
    
    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        _healthCheckTimer?.Dispose();
    }
}

/// <summary>
/// 代理池统计信息
/// </summary>
public class ProxyPoolStatistics
{
    public int TotalProxies { get; set; }
    public int HealthyProxies { get; set; }
    public int UnhealthyProxies { get; set; }
    public double AverageFailureCount { get; set; }
    
    public override string ToString()
    {
        return $"总代理数: {TotalProxies}, 健康: {HealthyProxies}, 不健康: {UnhealthyProxies}, 平均失败次数: {AverageFailureCount:F2}";
    }
}
```

#### 2. CefSharp 代理池集成

```csharp
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

/// <summary>
/// CefSharp 代理池浏览器包装器
/// </summary>
public class ProxyPoolBrowser
{
    private ChromiumWebBrowser _browser;
    private ProxyPool _proxyPool;
    private IRequestContext _requestContext;
    private ProxyInfo _currentProxy;
    
    public ChromiumWebBrowser Browser => _browser;
    
    public ProxyPoolBrowser(string initialUrl, ProxyPool proxyPool)
    {
        _proxyPool = proxyPool;
        
        // 创建独立的请求上下文
        var requestContextSettings = new RequestContextSettings
        {
            CachePath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CefSharp", "ProxyCache", Guid.NewGuid().ToString()
            )
        };
        
        _requestContext = new RequestContext(requestContextSettings);
        
        // 创建浏览器
        _browser = new ChromiumWebBrowser(initialUrl)
        {
            RequestContext = _requestContext
        };
        
        // 监听导航事件
        _browser.FrameLoadStart += OnFrameLoadStart;
        _browser.LoadError += OnLoadError;
        _browser.LoadingStateChanged += OnLoadingStateChanged;
        
        // 设置初始代理
        RotateProxyAsync().Wait();
    }
    
    /// <summary>
    /// 轮换到下一个代理
    /// </summary>
    public async Task<bool> RotateProxyAsync()
    {
        var newProxy = _proxyPool.GetNextProxy();
        if (newProxy == null)
        {
            MessageBox.Show("代理池中没有可用的代理！", "错误");
            return false;
        }
        
        _currentProxy = newProxy;
        
        // 设置代理
        var result = await _requestContext.SetProxyAsync(
            newProxy.Scheme, 
            newProxy.Host, 
            newProxy.Port
        );
        
        if (result.Success)
        {
            Console.WriteLine($"已切换到代理: {newProxy}");
            return true;
        }
        else
        {
            Console.WriteLine($"设置代理失败: {result.ErrorMessage}");
            _proxyPool.ReportFailure(newProxy);
            return false;
        }
    }
    
    /// <summary>
    /// 手动轮换代理并重新加载当前页面
    /// </summary>
    public async Task RotateAndReloadAsync()
    {
        var success = await RotateProxyAsync();
        if (success)
        {
            _browser.Reload();
        }
    }
    
    private void OnFrameLoadStart(object sender, FrameLoadStartEventArgs e)
    {
        if (e.Frame.IsMain)
        {
            Console.WriteLine($"使用代理 {_currentProxy} 开始加载: {e.Url}");
        }
    }
    
    private void OnLoadError(object sender, LoadErrorEventArgs e)
    {
        // 报告代理失败
        if (_currentProxy != null && e.ErrorCode != CefErrorCode.Aborted)
        {
            Console.WriteLine($"代理 {_currentProxy} 加载失败: {e.ErrorCode}");
            _proxyPool.ReportFailure(_currentProxy);
            
            // 自动切换到下一个代理并重试
            if (e.Frame.IsMain)
            {
                Task.Run(async () =>
                {
                    await Task.Delay(1000); // 延迟1秒
                    await RotateAndReloadAsync();
                });
            }
        }
    }
    
    private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
    {
        if (!e.IsLoading && _browser.CanExecuteJavascriptInMainFrame)
        {
            // 页面加载成功，报告代理成功
            if (_currentProxy != null)
            {
                _proxyPool.ReportSuccess(_currentProxy);
            }
        }
    }
    
    /// <summary>
    /// 获取当前使用的代理信息
    /// </summary>
    public string GetCurrentProxyInfo()
    {
        return _currentProxy?.ToString() ?? "无代理";
    }
    
    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        _browser?.Dispose();
        _requestContext?.Dispose();
    }
}
```

#### 3. 使用示例

**示例 1: 基本代理池使用**

```csharp
using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

public class ProxyPoolExampleForm : Form
{
    private ProxyPool _proxyPool;
    private ProxyPoolBrowser _proxyBrowser;
    private Label _statusLabel;
    private Button _rotateButton;
    private TextBox _urlTextBox;
    private Button _goButton;
    
    public ProxyPoolExampleForm()
    {
        InitializeUI();
        InitializeProxyPool();
    }
    
    private void InitializeUI()
    {
        this.Width = 1200;
        this.Height = 800;
        this.Text = "CefSharp 代理池示例";
        
        // 工具栏
        var toolbar = new Panel
        {
            Dock = DockStyle.Top,
            Height = 80
        };
        
        _statusLabel = new Label
        {
            Location = new System.Drawing.Point(10, 10),
            Width = 600,
            Text = "正在初始化..."
        };
        
        _urlTextBox = new TextBox
        {
            Location = new System.Drawing.Point(10, 40),
            Width = 400
        };
        _urlTextBox.Text = "https://api.ipify.org/?format=json"; // IP查询接口
        
        _goButton = new Button
        {
            Location = new System.Drawing.Point(420, 38),
            Width = 80,
            Text = "访问"
        };
        _goButton.Click += GoButton_Click;
        
        _rotateButton = new Button
        {
            Location = new System.Drawing.Point(510, 38),
            Width = 100,
            Text = "切换代理"
        };
        _rotateButton.Click += RotateButton_Click;
        
        var statsButton = new Button
        {
            Location = new System.Drawing.Point(620, 38),
            Width = 100,
            Text = "代理统计"
        };
        statsButton.Click += (s, e) => 
        {
            MessageBox.Show(_proxyPool.GetStatistics().ToString(), "代理池统计");
        };
        
        toolbar.Controls.AddRange(new Control[] { 
            _statusLabel, _urlTextBox, _goButton, _rotateButton, statsButton 
        });
        
        this.Controls.Add(toolbar);
    }
    
    private void InitializeProxyPool()
    {
        // 初始化代理池
        _proxyPool = new ProxyPool
        {
            Mode = ProxyPool.RotationMode.Sequential,
            MaxFailureCount = 3,
            EnableHealthCheck = true
        };
        
        // 添加代理（示例数据）
        // 在实际使用中，您应该从文件或API加载真实的代理列表
        _proxyPool.AddProxy("proxy1.example.com", 8080, "http");
        _proxyPool.AddProxy("proxy2.example.com", 8080, "http");
        _proxyPool.AddProxy("proxy3.example.com", 1080, "socks5");
        
        // 或从文件加载
        // _proxyPool.LoadFromFile("proxies.txt");
        
        // 创建代理浏览器
        _proxyBrowser = new ProxyPoolBrowser(_urlTextBox.Text, _proxyPool);
        _proxyBrowser.Browser.Dock = DockStyle.Fill;
        
        this.Controls.Add(_proxyBrowser.Browser);
        
        UpdateStatus();
    }
    
    private async void RotateButton_Click(object sender, EventArgs e)
    {
        _rotateButton.Enabled = false;
        await _proxyBrowser.RotateAndReloadAsync();
        UpdateStatus();
        _rotateButton.Enabled = true;
    }
    
    private void GoButton_Click(object sender, EventArgs e)
    {
        _proxyBrowser.Browser.Load(_urlTextBox.Text);
    }
    
    private void UpdateStatus()
    {
        _statusLabel.Text = $"当前代理: {_proxyBrowser.GetCurrentProxyInfo()} | {_proxyPool.GetStatistics()}";
    }
    
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        _proxyBrowser?.Dispose();
        _proxyPool?.Dispose();
        base.OnFormClosing(e);
    }
}

// 主程序入口
public class Program
{
    [STAThread]
    static void Main()
    {
        var settings = new CefSettings();
        Cef.Initialize(settings);
        
        Application.EnableVisualStyles();
        Application.Run(new ProxyPoolExampleForm());
        
        Cef.Shutdown();
    }
}
```

**示例 2: 带认证的代理池**

```csharp
public class AuthProxyPoolExample
{
    public void SetupAuthenticatedProxyPool()
    {
        var proxyPool = new ProxyPool
        {
            Mode = ProxyPool.RotationMode.Random
        };
        
        // 添加需要认证的代理
        proxyPool.AddProxy(
            host: "premium-proxy1.com",
            port: 8080,
            scheme: "http",
            username: "myuser",
            password: "mypassword"
        );
        
        proxyPool.AddProxy(
            host: "premium-proxy2.com",
            port: 8080,
            scheme: "http",
            username: "myuser",
            password: "mypassword"
        );
        
        // 使用自定义请求处理器处理认证
        var browser = new ChromiumWebBrowser("https://www.example.com");
        browser.RequestHandler = new AuthProxyRequestHandler(proxyPool);
    }
}

/// <summary>
/// 处理代理认证的请求处理器
/// </summary>
public class AuthProxyRequestHandler : RequestHandler
{
    private ProxyPool _proxyPool;
    private ProxyInfo _currentProxy;
    
    public AuthProxyRequestHandler(ProxyPool proxyPool)
    {
        _proxyPool = proxyPool;
        _currentProxy = proxyPool.GetNextProxy();
    }
    
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
        if (isProxy && _currentProxy != null)
        {
            if (!string.IsNullOrEmpty(_currentProxy.Username))
            {
                callback.Continue(_currentProxy.Username, _currentProxy.Password);
                return true;
            }
        }
        
        return false;
    }
}
```

**示例 3: 从文件加载代理列表**

创建代理列表文件 `proxies.txt`:
```
# HTTP 代理
http://proxy1.example.com:8080
http://proxy2.example.com:8080

# SOCKS5 代理
socks5://proxy3.example.com:1080

# 带认证的代理
http://username:password@proxy4.example.com:8080
socks5://user:pass@proxy5.example.com:1080

# 这是注释，会被忽略
```

使用代码：
```csharp
var proxyPool = new ProxyPool
{
    Mode = ProxyPool.RotationMode.HealthBased
};

// 从文件加载
proxyPool.LoadFromFile("proxies.txt");

Console.WriteLine(proxyPool.GetStatistics());
```

**示例 4: 高级代理池配置**

```csharp
public class AdvancedProxyPoolSetup
{
    public static ProxyPool CreateAdvancedProxyPool()
    {
        var proxyPool = new ProxyPool
        {
            // 使用健康状态优先的轮换模式
            Mode = ProxyPool.RotationMode.HealthBased,
            
            // 失败3次后标记为不健康
            MaxFailureCount = 3,
            
            // 启用自动健康检查
            EnableHealthCheck = true,
            
            // 每30秒检查一次
            HealthCheckInterval = 30000
        };
        
        // 从多个来源加载代理
        LoadProxiesFromApi(proxyPool);
        LoadProxiesFromFile(proxyPool);
        
        return proxyPool;
    }
    
    private static void LoadProxiesFromApi(ProxyPool pool)
    {
        // 从代理API服务获取代理列表
        // 这里是示例，实际应该调用您的代理API
        var apiProxies = new[]
        {
            new ProxyInfo { Host = "api-proxy1.com", Port = 8080, Scheme = "http" },
            new ProxyInfo { Host = "api-proxy2.com", Port = 8080, Scheme = "http" }
        };
        
        pool.AddProxies(apiProxies);
    }
    
    private static void LoadProxiesFromFile(ProxyPool pool)
    {
        if (System.IO.File.Exists("proxies.txt"))
        {
            pool.LoadFromFile("proxies.txt");
        }
    }
}
```

#### 4. 最佳实践

**性能优化：**

1. **使用独立的请求上下文**：每个代理浏览器使用独立的缓存路径，避免缓存冲突
2. **异步操作**：所有代理切换操作都使用异步方法，避免阻塞UI
3. **健康检查**：定期检查代理健康状态，自动剔除失效代理

**错误处理：**

```csharp
public class ProxyPoolBrowser
{
    private ChromiumWebBrowser _browser;
    private ProxyPool _proxyPool;
    private ProxyInfo _currentProxy;
    
    // ... 其他成员 ...
    
    public async Task<bool> SafeLoadWithProxy(string url, int maxRetries = 3)
    {
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                await RotateProxyAsync();
                _browser.Load(url);
                
                // 等待加载完成
                await Task.Delay(5000);
                
                if (_browser.IsLoading == false)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"重试 {i + 1}/{maxRetries}: {ex.Message}");
                
                if (_currentProxy != null)
                {
                    _proxyPool.ReportFailure(_currentProxy);
                }
            }
        }
        
        return false;
    }
}
```

**监控和日志：**

```csharp
public class ProxyPoolMonitor
{
    private ProxyPool _pool;
    
    public ProxyPoolMonitor(ProxyPool pool)
    {
        _pool = pool;
        
        // 定期记录统计信息
        var timer = new Timer(_ => LogStatistics(), null, 0, 60000);
    }
    
    private void LogStatistics()
    {
        var stats = _pool.GetStatistics();
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {stats}");
        
        // 可以将统计信息写入日志文件或发送到监控系统
        System.IO.File.AppendAllText(
            "proxy_pool_stats.log",
            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {stats}\n"
        );
    }
}
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
