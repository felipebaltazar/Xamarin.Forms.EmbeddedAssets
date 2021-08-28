using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Xamarin.Forms.EmbeddedAssets
{
    public static class AssetRegistrar
    {
        internal static readonly Dictionary<string, (ExportAssetAttribute attribute, Assembly assembly)> EmbeddedFonts =
            new Dictionary<string, (ExportAssetAttribute attribute, Assembly assembly)>();

        private static Dictionary<string, (bool, string)> assetLookupCache = new Dictionary<string, (bool, string)>();

        private static readonly EmbeddedAssetLoader embeddedAssetLoader = new EmbeddedAssetLoader();

        public static void Register(ExportAssetAttribute fontAttribute, Assembly assembly)
        {
            EmbeddedFonts[fontAttribute.AssetFileName] = (fontAttribute, assembly);
        }

        public static (bool hasAsset, string fontPath) HasAsset(string asset)
        {
            try
            {
                if (!EmbeddedFonts.TryGetValue(asset, out var foundFont))
                    return (false, null);

                if (assetLookupCache.TryGetValue(asset, out var foundResult))
                    return foundResult;


                var fontStream = GetEmbeddedResourceStream(foundFont.assembly, foundFont.attribute.AssetFileName);

                var result = embeddedAssetLoader.LoadFont(new EmbeddedAsset { FontName = foundFont.attribute.AssetFileName, ResourceStream = fontStream });
                return assetLookupCache[asset] = result;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return assetLookupCache[asset] = (false, null);
        }

        private static Stream GetEmbeddedResourceStream(Assembly assembly, string resourceFileName)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            var resourcePaths = resourceNames
                .Where(x => x.EndsWith(resourceFileName, StringComparison.CurrentCultureIgnoreCase))
                .ToArray();

            if (!resourcePaths.Any())
                throw new Exception(string.Format("Resource ending with {0} not found.", resourceFileName));

            if (resourcePaths.Length > 1)
                resourcePaths = resourcePaths.Where(x => IsFile(x, resourceFileName)).ToArray();

            return assembly.GetManifestResourceStream(resourcePaths.FirstOrDefault());
        }

        private static bool IsFile(string path, string file)
        {
            if (!path.EndsWith(file, StringComparison.Ordinal))
                return false;

            return path.Replace(file, "").EndsWith(".", StringComparison.Ordinal);
        }
    }
}
