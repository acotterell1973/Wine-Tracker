using Newtonsoft.Json;
// ReSharper disable InconsistentNaming

namespace WineTracker.Models
{
    public class ProductInfo
    {
        [JsonProperty(PropertyName = "Valid")]
        public string valid { get; set; }
        [JsonProperty(PropertyName = "Number")]
        public string number { get; set; }

        [JsonProperty(PropertyName = "ItemName")]
        public string itemname { get; set; }

        [JsonProperty(PropertyName = "Alias")]
        public string alias { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string description { get; set; }

        [JsonProperty(PropertyName = "AveragePrice")]
        public double avg_price { get; set; }

        [JsonProperty(PropertyName = "RateUp")]
        public int rate_up { get; set; }

        [JsonProperty(PropertyName = "RateDown")]
        public int rate_down { get; set; }
    }
}
