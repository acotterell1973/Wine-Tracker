using WineTracker.Models;
using WineTracker.Models.Messages;

//https://github.com/EgorBo/CrossChat-Xamarin.Forms/tree/master/Client/Crosschat.Client/Model/Entities/Messages
namespace WineTracker.ViewModels
{
    public class EventViewModel 
    {
        private readonly Event _eventPoco;

        public EventViewModel(Event eventPoco)
        {
            _eventPoco = eventPoco;
        }
    }

   
}
