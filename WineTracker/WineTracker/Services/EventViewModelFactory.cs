using WineTracker.Models;
using WineTracker.Models.Messages;
using WineTracker.ViewModels;

namespace WineTracker.Services
{
    public class EventViewModelFactory
    {
        public EventViewModel Get(Event @event, string currentUserName)
        {
            if (@event is TextMessage)
                return new TextMessageViewModel(@event as TextMessage, currentUserName);

            //TODO: create VM for other event types 

            return new EventViewModel(@event);
        }
    }
}
