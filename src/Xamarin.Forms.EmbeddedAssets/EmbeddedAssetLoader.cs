using System;
using System.Diagnostics;
using System.IO;

namespace Xamarin.Forms.EmbeddedAssets
{
    internal class EmbeddedAssetLoader
    {
        internal (bool success, string filePath) LoadAsset(EmbeddedAsset asset)
        {
            var tmpdir = Path.GetTempPath();

            var directoryPath = Path.Combine(tmpdir, asset.FolderName);
            var filePath = Path.Combine(directoryPath, asset.AssetName);

            if (File.Exists(filePath))
                return (true, filePath);

            if(!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            try
            {
                ProcessResourceStream(asset.ResourceStream, filePath);

                if (asset.LoadAssociatedResources)
                {
                    foreach (var resource in asset.AssociatedResources)
                    {
                        LoadAsset(resource);
                    }
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

        private static void ProcessResourceStream(Stream resourceStream, string filePath)
        {
            using (var fileStream = File.Create(filePath))
            {
                resourceStream.CopyTo(fileStream);
            }
        }
    }
}
