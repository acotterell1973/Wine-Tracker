using Newtonsoft.Json;


namespace WineTracker.Models.DirectLineClient
{
    public class Conversation
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Activity
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("channelId")]
        public string ChannelId { get; set; }


        [JsonProperty("conversation")]
        public Conversation Conversation { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

    }
}