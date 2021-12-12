using System;
using System.ComponentModel;

namespace Xamarin.Forms.EmbeddedAssets
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    [AttributeUsage(AttributeTargets.Assembly)]
    public class PreserveEmbeddedAssetsAttribute : Attribute
    {
    }
}
