using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WineTracker.EventHandler;
using WineTracker.Models.Messages;

namespace WineTracker.Interface
{
    public interface IChatServices
    {
        ObservableCollection<Event> Messages { get; }

        Task SendImage(byte[] image);
        Task SendMessage(string message);
        event EventHandler<OnMessageEventArgs> OnBotMessageReceived;
    }
}