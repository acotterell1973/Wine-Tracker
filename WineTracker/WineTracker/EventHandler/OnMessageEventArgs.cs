using System;
using WineTracker.Models.DirectLineClient;

namespace WineTracker.EventHandler
{
    public class OnMessageEventArgs : EventArgs
    {
        public readonly ConversationMessages BotActivityMessage;
        

        public OnMessageEventArgs(ConversationMessages botActivityMessage)
        {
            BotActivityMessage = botActivityMessage;
        }
    }
}