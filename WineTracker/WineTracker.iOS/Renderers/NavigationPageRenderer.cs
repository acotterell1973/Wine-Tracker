using UIKit;
using WineTracker.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationRenderer), typeof(NavigationPageRenderer))]
namespace WineTracker.iOS.Renderers
{
    public class NavigationPageRenderer : NavigationRenderer
    {
        public NavigationPageRenderer()
        {
            
        }
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.NavigationBar.TintColor = UIColor.White;
            this.NavigationBar.BarTintColor = UIColor.Blue;
            this.NavigationBar.BarStyle = UIBarStyle.Black;
        }
    }
}
