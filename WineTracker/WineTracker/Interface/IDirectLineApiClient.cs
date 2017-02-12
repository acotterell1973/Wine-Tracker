using System;
using System.Threading.Tasks;
using WineTracker.EventHandler;

namespace WineTracker.Interface
{
    public interface IDirectLineApiClient
    {
        event EventHandler<OnMessageEventArgs> OnBotMessage;
        string Initialize(string directLineSecret);
        Task SendMessageAsync(string from, string text);
        Task EndMessageAsync(string from);
    }

}