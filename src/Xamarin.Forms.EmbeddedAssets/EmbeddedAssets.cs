using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Internals;

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class ModuleInitializerAttribute : Attribute
    {
    }
}

namespace Xamarin.Forms.EmbeddedAssets
{
    [Preserve]
    internal static class EmbeddedAssets
    {
        [Preserve]
        [ModuleInitializer]
        internal static void Init()
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
