# Xamarin.Forms.EmbeddedAssets
Free yourself from platform specifc assets


## Getting started

- Include your assets as embeddded resource on your NetStandard project

- Declare all assets that you need to use

```csharp

using Xamarin.Forms.EmbeddedAssets;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportAsset("MyPage.html")] //here you can declare your assets files

```

- You should call `Init();` method on App.xaml.cs

```csharp

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        XFEmbeddedAssets.Init(); //this will process your files

        MainPage = new MainPage();
    }
    ...
}
```

- Declare the Xaml namespace for Xamarin.Forms.EmbeddedAssets Sdk

```xml
xmlns:embedded="clr-namespace:Xamarin.Forms.EmbeddedAssets;assembly=Xamarin.Forms.EmbeddedAssets"
```

- Now you can use the default markup extension

```xml
<WebView Source="{embedded:EmbeddedAsset MyPage.html}"
                 VerticalOptions="FillAndExpand"
                 HorizontalOptions="FillAndExpand"/>
```