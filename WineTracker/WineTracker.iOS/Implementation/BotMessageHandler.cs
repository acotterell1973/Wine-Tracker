using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using WineTracker.EventHandler;
using WineTracker.iOS.Implementation;

[assembly: Xamarin.Forms.Dependency(typeof(BotMessageHandler))]
namespace WineTracker.iOS.Implementation
{
    public class BotMessageHandler : IBotMessageHandler
    {
        public BotMessageHandler()
        {
            //Listen to when the user types a message to the bot
            NSNotificationCenter.DefaultCenter.AddObserver((NSString)"OnMessegeSendNotification", OnMessegeSendNotification);
        }

        private void OnMessegeSendNotification(NSNotification notification)
        {
            var messageData = notification.Object;
            var x = notification.UserInfo;
            //Send the event to the PCL Library with the Bot Message
            BotMessageEvent?.Invoke(this, new BotMessageEventEventArgs("", ""));
        }

        public void UpdateUiWithResponse(string messageType, string message)
        {
            var messageAttrs = new Dictionary<string, string>
                {
                    {"SENDERID", messageType},
                    {"MESSAGE", message},
                };
            var messageDict = NSDictionary.FromObjectsAndKeys(messageAttrs.Values.ToArray(), messageAttrs.Keys.ToArray());
            NSNotificationCenter.DefaultCenter.PostNotificationName((NSString)"OnMessegeReceviedNotification", null, messageDict);
        }
        public event EventHandler<BotMessageEventEventArgs> BotMessageEvent;
    }
}
