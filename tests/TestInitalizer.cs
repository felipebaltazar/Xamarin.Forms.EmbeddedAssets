using System.Runtime.CompilerServices;
using Xamarin.Forms.EmbeddedAssets.Tests.Mocks;

namespace Xamarin.Forms.EmbeddedAssets.Tests
{
    public static class TestInitalizer
    {
        [ModuleInitializer]
        public static void Init()
        {
            Device.PlatformServices = new PlatformServices("Windows", OSAppTheme.Unspecified);
        }
    }
}
