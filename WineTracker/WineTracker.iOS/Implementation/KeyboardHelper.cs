using System;
using Foundation;
using UIKit;
using WineTracker.Interface;
using KeyboardHelper = WineTracker.iOS.Implementation.KeyboardHelper;

// Raises keyboard changed events containing the keyboard height and
// whether the keyboard is becoming visible or not
[assembly: Xamarin.Forms.Dependency(typeof(KeyboardHelper))]
namespace WineTracker.iOS.Implementation
{
	public class KeyboardHelper : IKeyboardHelper
	{
		public KeyboardHelper()
		{
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillHideNotification, OnKeyboardNotification);
			NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.WillShowNotification, OnKeyboardNotification);
		}

		public event EventHandler<KeyboardHelperEventArgs> KeyboardChanged;

		private void OnKeyboardNotification(NSNotification notification)
		{
			var visible = notification.Name == UIKeyboard.WillShowNotification;
			var keyboardFrame = visible
				? UIKeyboard.FrameEndFromNotification(notification)
				: UIKeyboard.FrameBeginFromNotification(notification);

			KeyboardChanged?.Invoke(this, new KeyboardHelperEventArgs(visible, (float)keyboardFrame.Height));
		}
	}
}