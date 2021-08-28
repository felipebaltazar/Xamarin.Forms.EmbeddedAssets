using System;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.EmbeddedAssets
{
    [ContentProperty(nameof(Source))]
    public class EmbeddedAssetExtension : IMarkupExtension
    {
        public string Source
        {
            get; set;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source is null) return null;

            var (hasAsset, filePath) = AssetRegistrar.HasAsset(Source);

            var htmlSourceStr = Source;
            if (hasAsset)
                htmlSourceStr = filePath;

            return (Device.RuntimePlatform == Device.Android ? "file:///" : string.Empty ) +htmlSourceStr;
        }
    }
}
