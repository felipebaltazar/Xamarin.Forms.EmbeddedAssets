using Xamarin.Forms;
using XFEmbeddedAssets = Xamarin.Forms.EmbeddedAssets.EmbeddedAssets;

namespace EmbeddedAssets.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            XFEmbeddedAssets.Init();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
