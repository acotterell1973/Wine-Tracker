using Newtonsoft.Json;

namespace WineTracker.Models.DirectLineClient
{
    public class ReconnectMessageResponse
    {
        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }


        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty("streamUrl")]
        public string StreamUrl { get; set; }
    }
}