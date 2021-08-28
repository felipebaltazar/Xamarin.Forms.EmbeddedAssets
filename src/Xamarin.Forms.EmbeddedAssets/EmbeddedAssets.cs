using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Xamarin.Forms.EmbeddedAssets
{
    public static class EmbeddedAssets
    {
        public static void Init()
        {
            var assemblies = Device.GetAssemblies();
            var defaultRendererAssembly = Device.PlatformServices.GetType().GetTypeInfo().Assembly;
            var indexOfExecuting = Array.IndexOf(assemblies, defaultRendererAssembly);

            if (indexOfExecuting > 0)
            {
                assemblies[indexOfExecuting] = assemblies[0];
                assemblies[0] = defaultRendererAssembly;
            }

            foreach (var assembly in assemblies)
            {
                var attrType = typeof(ExportAssetAttribute);
                var attributes = assembly.GetCustomAttributesSafe(attrType);

                if (attributes == null || attributes.Length == 0)
                    continue;

                var length = attributes.Length;
                for (var i = 0; i < length; i++)
                {
                    var a = attributes[i];
                    var attribute = a as HandlerAttribute;
                    if (attribute == null && (a is ExportAssetAttribute fa))
                    {
                        AssetRegistrar.Register(fa, assembly);
                    }
                }
            }
        }
    }
}
