using System.Collections.Generic;
using UIKit;
using WineTracker.iOS.Renderers;
using WineTracker.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EditPage), typeof(WineContentPageRenderer))]
namespace WineTracker.iOS.Renderers
{

    public class WineContentPageRenderer : PageRenderer
    {
        public new EditPage Element => (EditPage)base.Element;

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var leftNavList = new List<UIBarButtonItem>();
            var rightNavList = new List<UIBarButtonItem>();

            var navigationItem = NavigationController.TopViewController.NavigationItem;

            for (var i = 0; i < Element.ToolbarItems.Count; i++)
            {

                var reorder = (Element.ToolbarItems.Count - 1);
                var itemPriority = Element.ToolbarItems[reorder - i].Priority;

                if (itemPriority == 1)
                {
                    UIBarButtonItem leftNavItems = navigationItem.RightBarButtonItems[i];
                    leftNavList.Add(leftNavItems);
                }
                else if (itemPriority == 0)
                {
                    UIBarButtonItem rightNavItems = navigationItem.RightBarButtonItems[i];
                    rightNavList.Add(rightNavItems);
                }
            }

            navigationItem.SetLeftBarButtonItems(leftNavList.ToArray(), false);
            navigationItem.SetRightBarButtonItems(rightNavList.ToArray(), false);

        }
    }
}