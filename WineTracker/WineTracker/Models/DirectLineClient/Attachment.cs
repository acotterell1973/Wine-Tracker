using Newtonsoft.Json;

namespace WineTracker.Models.DirectLineClient
{
    public class Attachement
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }
    }
}
