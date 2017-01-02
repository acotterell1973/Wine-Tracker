using Newtonsoft.Json;

namespace WineTracker.Models.DirectLineClient
{
    public class StartConversationResponse
    {
        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }


        [JsonProperty("ExpiresIn")]
        public string ExpiresIn { get; set; }

        [JsonProperty("streamUrl")]
        public string StreamUrl { get; set; }
    }
}
