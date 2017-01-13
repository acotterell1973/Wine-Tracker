using System.Threading.Tasks;
using WineTracker.Models.DirectLineClient;

namespace WineTracker.Interface
{
    public interface IDirectLineApiClient
    {
        void Initialize(string directLineSecret);
        Task RefreshTokenAsync();

        /// <summary>
        /// Opens a direct conversation with a specific BOT
        /// </summary>
        /// <returns></returns>
        Task<StartConversationResponse> StartConversationAsync();

        Task SendMessageAsync(string conversationId, string from, string text);
        Task EndMessageAsync(string conversationId, string from);

        /// <summary>
        /// Reconnects to a conversation if the connection is lost
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="watermark"></param>
        /// <returns></returns>
        Task<ReconnectMessageResponse> ReconnectAsync(string conversationId, string watermark);

        Task<ConversationMessages> GetMessagesAsync(string conversationId, string watermark);
    }

}