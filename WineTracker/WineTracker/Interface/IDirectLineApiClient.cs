using System.Threading.Tasks;
using WineTracker.Models.DirectLineClient;

namespace WineTracker.Interface
{
    public interface IDirectLineApiClient
    {
        Task<ConversationMessages> GetMessagesAsync(string conversationId, string watermark);
        Task SendMessageAsync(string conversationId, string from, string text);
        void SetClientSecret(string botSecret);
        Task<StartConversationResponse> StartConversationAsync();
    }
}