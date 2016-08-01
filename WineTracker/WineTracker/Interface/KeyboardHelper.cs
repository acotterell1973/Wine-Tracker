using System;
using Xamarin.Forms;

namespace WineTracker.Interface
{
	// Provides static access to keyboard events
	public static class KeyboardHelper
	{
		private static IKeyboardHelper _keyboardHelper;

		public static void Init()
		{
			if (_keyboardHelper == null)
			{
				_keyboardHelper = DependencyService.Get<IKeyboardHelper>();
			}
		}

		public static event EventHandler<KeyboardHelperEventArgs> KeyboardChanged
		{
			add
			{
				Init();
				_keyboardHelper.KeyboardChanged += value;
			}
			remove
			{
				Init();
				_keyboardHelper.KeyboardChanged -= value;
			}
		}
	}

	public interface IKeyboardHelper
	{
		event EventHandler<KeyboardHelperEventArgs> KeyboardChanged;
	}

	public class KeyboardHelperEventArgs : EventArgs
	{
		public readonly bool Visible;
		public readonly float Height;

		public KeyboardHelperEventArgs(bool visible, float height)
		{
			Visible = visible;
			Height = height;
		}
	}
}
