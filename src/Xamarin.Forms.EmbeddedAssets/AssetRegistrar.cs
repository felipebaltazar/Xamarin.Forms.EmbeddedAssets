using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Xamarin.Forms.EmbeddedAssets
{
    internal static class AssetRegistrar
    {
        internal static readonly Dictionary<string, (ExportAssetAttribute attribute, Assembly assembly)> EmbeddedAssets =
            new Dictionary<string, (ExportAssetAttribute attribute, Assembly assembly)>();

        private static Dictionary<string, (bool, string)> assetLookupCache = new Dictionary<string, (bool, string)>();

        private static readonly EmbeddedAssetLoader embeddedAssetLoader = new EmbeddedAssetLoader();

        internal static void Register(ExportAssetAttribute assetAttribute, Assembly assembly)
        {
            EmbeddedAssets[assetAttribute.AssetFileName] = (assetAttribute, assembly);
        }

        internal static (bool hasAsset, string assetPath) HasAsset(string asset)
        {
            try
            {
                if (!EmbeddedAssets.TryGetValue(asset, out var foundAsset))
                    return (false, null);

                if (assetLookupCache.TryGetValue(asset, out var foundResult))
                    return foundResult;

                var assetInfo = GetEmbeddedResourceStream(foundAsset.assembly, foundAsset.attribute.AssetFileName);

                var embeededAsset = new EmbeddedAsset {
                    AssetName = foundAsset.attribute.AssetFileName,
                    ResourceStream = assetInfo.Stream,
                    FolderName = assetInfo.Folder,
                    LoadAssociatedResources = foundAsset.attribute.LoadAssociatedInFolder,
                    AssociatedResources = GetAssociatedEmbeddedResourcesStream(foundAsset.assembly, foundAsset.attribute.AssetFileName)
                };

                var result = embeddedAssetLoader.LoadAsset(embeededAsset);
                return assetLookupCache[asset] = result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return assetLookupCache[asset] = (false, null);
        }

        private static IEnumerable<EmbeddedAsset> GetAssociatedEmbeddedResourcesStream(Assembly assembly, string assetFileName)
        {
            var resourceNames = assembly.GetManifestResourceNames();
            var resourcePath = resourceNames
               .FirstOrDefault(x => x.EndsWith(assetFileName, StringComparison.CurrentCultureIgnoreCase));

            if (string.IsNullOrWhiteSpace(resourcePath))
                yield break;

            var prefix = resourcePath.Replace(assetFileName, string.Empty);
            foreach (var resource in resourceNames.Where(r => r.StartsWith(prefix) && !r.EndsWith(assetFileName)))
            {
                var assemblyName = assembly.GetName().Name;
                var extension = Path.GetExtension(resource);

                var resourceWithoutExtension = resource.Replace(extension, string.Empty);
                var resourceDirectory = resourceWithoutExtension.Substring(0, resourceWithoutExtension.LastIndexOf("."));
                var resourceWithoutAssembly = resourceDirectory.Replace(assemblyName, string.Empty).Trim('.');
                var folder = resourceWithoutAssembly.Replace(".", "/");

                yield return new EmbeddedAsset {
                    AssetName = resource.Replace(resourceDirectory, string.Empty).Trim('.'),
                    FolderName = folder,
                    ResourceStream = assembly.GetManifestResourceStream(resource)
                };
            }
        }

        private static (string Folder, Stream Stream) GetEmbeddedResourceStream(Assembly assembly, string resourceFileName)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            var resourcePaths = resourceNames
                .Where(x => x.EndsWith(resourceFileName, StringComparison.CurrentCultureIgnoreCase))
                .ToArray();

            if (!resourcePaths.Any())
                throw new Exception(string.Format("Resource ending with {0} not found.", resourceFileName));

            if (resourcePaths.Length > 1)
                resourcePaths = resourcePaths.Where(x => IsFile(x, resourceFileName)).ToArray();

            var resourcePath = resourcePaths.First();
            var assemblyName = assembly.GetName().Name;
            var folder = resourcePath.Replace(resourceFileName, string.Empty)
                                     .Replace(assemblyName, string.Empty)
                                     .Trim('.')
                                     .Replace(".", "/");

            return (folder, assembly.GetManifestResourceStream(resourcePath));
        }

        private static bool IsFile(string path, string file)
        {
            if (!path.EndsWith(file, StringComparison.Ordinal))
                return false;

            return path.Replace(file, "").EndsWith(".", StringComparison.Ordinal);
        }

    }
}
