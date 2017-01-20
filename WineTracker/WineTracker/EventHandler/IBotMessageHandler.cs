using System;

namespace WineTracker.EventHandler
{
    public interface IBotMessageHandler
    {
        event EventHandler<BotMessageEventEventArgs> BotMessageEvent;
        void UpdateUiWithResponse(string messageType, string message);
    }

    public class BotMessageEventEventArgs : EventArgs
    {
        public readonly string MessageType;
        public readonly string Message;

        public BotMessageEventEventArgs(string messageType, string message)
        {
            MessageType = messageType;
            Message = message;
        }
    }
}
