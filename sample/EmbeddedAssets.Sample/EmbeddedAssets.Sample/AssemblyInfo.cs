using Xamarin.Forms.EmbeddedAssets;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

[assembly:PreserveEmbeddedAssets]

[assembly: ExportAsset("MyPage.html", loadAssociatedResourcesInFolder: true)]