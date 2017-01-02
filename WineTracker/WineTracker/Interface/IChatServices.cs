using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WineTracker.Models;
using WineTracker.Models.Messages;

namespace WineTracker.Interface
{
    public interface IChatServices
    {
        ObservableCollection<Event> Messages { get; }

        Task SendImage(byte[] image);
        Task SendMessage(string message);
    }
}