using System;
using System.Reflection;

namespace Xamarin.Forms.EmbeddedAssets
{
    internal static class ReflectionExtensions
    {
        internal static object[] GetCustomAttributesSafe(this Assembly assembly, Type attrType)
        {
            try
            {
#if NETSTANDARD2_0
                return assembly.GetCustomAttributes(attrType, true);
#else
				return assembly.GetCustomAttributes(attrType).ToArray();
#endif
            }
            catch (System.IO.FileNotFoundException)
            {
                // Sometimes the previewer doesn't actually have everything required for these loads to work
                Console.WriteLine("[WARNING] Could not load assembly: {0} for Attribute {1} | Some renderers may not be loaded", assembly.FullName, attrType.FullName);
            }

            return null;
        }
    }
}
