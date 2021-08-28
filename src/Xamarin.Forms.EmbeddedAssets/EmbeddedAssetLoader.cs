using System;
using System.Diagnostics;
using System.IO;

namespace Xamarin.Forms.EmbeddedAssets
{
    public class EmbeddedAssetLoader
    {
        public (bool success, string filePath) LoadFont(EmbeddedAsset asset)
        {
            var tmpdir = Path.GetTempPath();
            var filePath = Path.Combine(tmpdir, asset.FontName);

            if (File.Exists(filePath))
                return (true, filePath);

            try
            {
                using (var fileStream = File.Create(filePath))
                {
                    asset.ResourceStream.CopyTo(fileStream);
                }

                return (true, filePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                File.Delete(filePath);
            }

            return (false, null);
        }
    }
}
