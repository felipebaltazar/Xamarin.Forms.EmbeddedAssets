using System.IO;

namespace Xamarin.Forms.EmbeddedAssets
{
    public struct EmbeddedAsset
    {
        public string FontName
        {
            get; set;
        }

        public Stream ResourceStream
        {
            get; set;
        }
    }
}
