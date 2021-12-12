using System.Collections.Generic;
using System.IO;

namespace Xamarin.Forms.EmbeddedAssets
{
    internal struct EmbeddedAsset
    {
        public string AssetName
        {
            get; set;
        }

        public Stream ResourceStream
        {
            get; set;
        }

        public IEnumerable<EmbeddedAsset> AssociatedResources
        {
            get; set;
        }

        public bool LoadAssociatedResources
        {
            get;
            set;
        }

        public string FolderName
        {
            get;
            set;
        }
    }
}
