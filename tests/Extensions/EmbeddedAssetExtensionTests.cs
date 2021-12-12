extern alias Source;

using FluentAssertions;
using Xunit;
using Source::Xamarin.Forms.EmbeddedAssets;
using System.IO;

namespace Xamarin.Forms.EmbeddedAssets.Tests.Extensions
{
    public sealed class EmbeddedAssetExtensionTests : BaseTests
    {
        private readonly EmbeddedAssetExtension _extension;

        public EmbeddedAssetExtensionTests()  :  base()
        {
            _extension  = new EmbeddedAssetExtension();
        }

        [Theory]
        [InlineData("MyPage.html", true)]
        [InlineData("fakeResource.jpg", false)]
        public void Extension_ShouldSearchEmbeddedResourceOnLoader(string asset, bool fileExists)
        {
            _extension.Source = asset;
            var result = _extension.ProvideValue(null);

            var resultStr = result.Should().BeOfType<string>().Subject;

            resultStr.EndsWith(resultStr).Should().BeTrue();

            File.Exists(resultStr).Should().Be(fileExists);
        }
    }
}
