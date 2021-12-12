using System;
using System.Threading.Tasks;

namespace Xamarin.Forms.EmbeddedAssets
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class ExportAssetAttribute : Attribute
    {
        public ExportAssetAttribute(string assetFileName, bool loadAssociatedResourcesInFolder = false)
        {
            AssetFileName = assetFileName;
            LoadAssociatedInFolder = loadAssociatedResourcesInFolder;
        }

        public bool LoadAssociatedInFolder
        {
            get;
        }

        public string AssetFileName
        {
            get;
        }
    }
}
