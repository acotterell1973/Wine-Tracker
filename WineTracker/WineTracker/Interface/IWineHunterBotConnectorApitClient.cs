using System.Threading.Tasks;

namespace WineTracker.Services.Components
{
    public interface IWineHunterBotConnectorApitClient
    {
        Task SendMessageAsync(string from, string text);
    }
}