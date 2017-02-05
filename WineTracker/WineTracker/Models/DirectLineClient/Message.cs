using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WineTracker.Models.DirectLineClient
{
    public class From
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
    public class Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }


        [JsonProperty("from")]
        public From From { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("channelData")]
        public JObject ChannelData { get; set; }

        [JsonProperty("images")]
        public IList<string> Images { get; set; }

        //[JsonProperty("attachments")]
        //public IList<Attachement> Attachments { get; set; }

        [JsonProperty("eTag")]
        public string ETag { get; set; }
    }
}
