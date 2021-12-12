extern alias Source;

using FluentAssertions;
using System.IO;
using System.Linq;
using Xunit;
using Source::Xamarin.Forms.EmbeddedAssets;

namespace Xamarin.Forms.EmbeddedAssets.Tests
{
    public sealed class AssetLoaderTests : BaseTests
    {
        public AssetLoaderTests() : base()
        {
        }

        [Fact(DisplayName = "Asset loader deve processar asset como recurso inserido")]
        public void AssetLoader_ShouldProcessAssociatedEmbeddedResource()
        {
            const string EMBEDDDED_ASSET_NAME = "MyPage.html";
            var (hasAsset, assetPath) = AssetRegistrar.HasAsset(EMBEDDDED_ASSET_NAME);

            hasAsset.Should().BeTrue();
            File.Exists(assetPath).Should().BeTrue();
        }

        [Fact(DisplayName = "Asset loader deve processar arquivos associados")]
        public void AssetLoader_ShouldProcessAssociatedFiles()
        {
            const string EMBEDDDED_ASSET_NAME = "MyPage.html";
            var (hasAsset, assetPath) = AssetRegistrar.HasAsset(EMBEDDDED_ASSET_NAME);

            var directory = Path.GetDirectoryName(assetPath);
            var associatedFiles = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories)
                                           .Where(f => !f.EndsWith(EMBEDDDED_ASSET_NAME))
                                           .ToArray();

            associatedFiles.Should().HaveCount(2);
        }
    }
}
