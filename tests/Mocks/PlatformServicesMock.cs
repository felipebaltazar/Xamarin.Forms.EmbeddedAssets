using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.EmbeddedAssets.Tests.Mocks
{
    internal class PlatformServices : IPlatformServices
    {
        public PlatformServices(string runtimePlatform, OSAppTheme requestedTheme)
        {
            RuntimePlatform = runtimePlatform;
            RequestedTheme = requestedTheme;
        }

        public bool IsInvokeRequired
        {
            get
            {
                return false;
            }
        }

        public string RuntimePlatform
        {
            get;
            private set;
        }

        public OSAppTheme RequestedTheme
        {
            get;
        }

        public void BeginInvokeOnMainThread(Action action)
        {
            action();
        }

        public Ticker CreateTicker()
        {
            return null;
        }

        public Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public string GetMD5Hash(string input)
        {
            throw new NotImplementedException();
        }

        public string GetHash(string input)
        {
            var stringBuilder = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(input));
                foreach (byte b in result)
                    stringBuilder.Append(b.ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        public double GetNamedSize(NamedSize size, Type targetElementType, bool useOldSizes)
        {
            return 14;
        }

        public Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IIsolatedStorageFile GetUserStoreForApplication()
        {
            return null;
        }

        public void OpenUriAction(Uri uri)
        {
        }

        public void QuitApplication()
        {
        }

        public SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            return new SizeRequest();
        }

        public async void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            while (true)
            {
                await Task.Delay(interval);

                if (!callback())
                    return;
            }
        }

        public Color GetNamedColor(string name)
        {
            // Not supported on this platform
            return Color.Default;
        }
    }
}
