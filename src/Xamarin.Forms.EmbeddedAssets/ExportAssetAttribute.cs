using System;

namespace Xamarin.Forms.EmbeddedAssets
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class ExportAssetAttribute : Attribute
    {
        public ExportAssetAttribute(string assetFileName)
        {
            AssetFileName = assetFileName;
        }

        public string AssetFileName
        {
            get;
        }
    }
}
