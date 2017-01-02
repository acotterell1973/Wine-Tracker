using Newtonsoft.Json;

namespace WineTracker.Models.DirectLineClient
{
    public class AuthenticationResponse
    {
        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
