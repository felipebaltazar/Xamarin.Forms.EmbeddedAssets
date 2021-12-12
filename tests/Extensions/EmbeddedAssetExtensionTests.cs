extern alias Source;

using FluentAssertions;
using Xunit;
using Source::Xamarin.Forms.EmbeddedAssets;

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
        [InlineData("MyPage.html", "FakeResources/HtmlPages\\MyPage.html")]
        [InlineData("fakeResource.jpg", "fakeResource.jpg")]
        public void Extension_ShouldSearchEmbeddedResourceOnLoader(string asset, string expectedResult)
        {
            _extension.Source = asset;
            var result = _extension.ProvideValue(null);

            var resultStr = result.Should().BeOfType<string>().Subject;

            resultStr.EndsWith(expectedResult).Should().BeTrue();
        }

    }
}
