using Foundation;
using WineTracker.iOS.Implementation;
using WineTracker.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(ClearMobileWebViewCache))]
namespace WineTracker.iOS.Implementation
{
    public class ClearMobileWebViewCache : IClearWebViewCache
    {
        public void ClearWebViewCache()
        {
            NSUrlCache.SharedCache.RemoveAllCachedResponses();
        }
    }
}
