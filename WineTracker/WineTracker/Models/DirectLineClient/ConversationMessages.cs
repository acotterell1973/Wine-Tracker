using System.Collections.Generic;
using Newtonsoft.Json;

namespace WineTracker.Models.DirectLineClient
{
    public class ConversationMessages
    {
        [JsonProperty("activities")]
        public IList<Activity> Messages { get; set; }

        [JsonProperty("watermark")]
        public string Watermark { get; set; }
    }
}
