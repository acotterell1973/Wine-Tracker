using System;
using WineTracker.EventHandler;
using Xamarin.Forms;

namespace WineTracker
{
    public static class BotMessageHandler 
    {
        private static IBotMessageHandler _botMessageHelper;

        private static void Init()
        {
            if (_botMessageHelper == null)
            {
                _botMessageHelper = DependencyService.Get<IBotMessageHandler>();
            }
        }

        public static event EventHandler<BotMessageEventEventArgs> BotMessageEventReceived
        {
            add
            {
                Init();
                _botMessageHelper.BotMessageEvent += value;
            }
            remove
            {
                Init();
                _botMessageHelper.BotMessageEvent -= value;
            }
        }

        public static void PublishMessage(string messageType, string message)
        {
            _botMessageHelper.UpdateUiWithResponse(messageType, message);
        }
    }
}