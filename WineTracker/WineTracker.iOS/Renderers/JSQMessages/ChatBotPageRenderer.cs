using CoreGraphics;
using Foundation;
using UIKit;
using WineTracker.iOS.Renderers.JSQMessages;
using WineTracker.iOS.Renderers.JSQMessages.ChatHelpers;
using WineTracker.Models.DirectLineClient;
using WineTracker.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Message = JSQMessagesViewController.Message;

[assembly: ExportRenderer(typeof(ChatBotPage), typeof(ChatBotPageRenderer))]
namespace WineTracker.iOS.Renderers.JSQMessages
{
    //https://github.com/rlingineni/Xamarin.Forms-ChatMessenger/blob/master/iOS/ChatPageRenderer.cs
    public class ChatBotPageRenderer : PageRenderer
	{

		public UINavigationController navigationController;

		public static UIWindow window;

		public JSQmessages viewController;

		public static System.EventHandler finished;

		public static UINavigationController navigation;

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
            
			navigation = NavigationController;

			window = new UIWindow(UIScreen.MainScreen.Bounds);
            //This is the class which actually implements the component a couple of elements to make it work
            //sender is a public field of JSQmessages and it is populated using the public fields we defined in the orginal forms ChatPage.
            viewController = new JSQmessages
			{
				sender = new BotUser() {Id = ChatBotPage.SenderId, DisplayName = ChatBotPage.SenderName},
				View = {Frame = View.Frame}
			};
            //viewController.messages.Add(new Message(ChatBotPage.SenderId, ChatBotPage.SenderName, NSDate.DistantPast, "Hi There"));
            navigationController = new UINavigationController();
			navigationController.PushViewController(viewController, false);

			AddChildViewController(viewController);

		    //Position the chat in the existing layout
            JsqMessageLayoutPosition messageLayoutPosition = new JsqMessageLayoutPosition(View.Frame.X + 12f, View.Frame.Y + 190f, View.Bounds.Width - 23f, View.Bounds.Height - 274f);
            viewController.View.Frame = new CGRect(messageLayoutPosition.X,messageLayoutPosition.Y,messageLayoutPosition.Width,messageLayoutPosition.Height);

			View.AddSubview(viewController.View);
			this.DidMoveToParentViewController(viewController);
		}
        
    }
	
}
