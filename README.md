# Xamarin.Forms.EmbeddedAssets
Free yourself from platform specifc assets

## Getting started

- Include your assets as embeddded resource on your NetStandard project

- Declare all assets that you need to use

```csharp

using Xamarin.Forms.EmbeddedAssets;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly: ExportAsset("MyPage.html", loadAssociatedResourcesInFolder: true)] //here you can declare your assets files and if some associated files (in the same folder) are needed

```

- Now you can use the default markup extension

```xml
<WebView Source="{EmbeddedAsset MyPage.html}"
                 VerticalOptions="FillAndExpand"
                 HorizontalOptions="FillAndExpand"/>
```


![image](https://user-images.githubusercontent.com/19656249/145725238-b290df11-3535-4def-b34e-2826257599a7.png)
