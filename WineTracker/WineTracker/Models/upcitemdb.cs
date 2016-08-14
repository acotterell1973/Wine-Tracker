using System.Collections.Generic;

namespace WineTracker.Models
{

    public class Offer
    {
        public string merchant { get; set; }
        public string domain { get; set; }
        public string title { get; set; }
        public string currency { get; set; }
        public string list_price { get; set; }
        public double price { get; set; }
        public string shipping { get; set; }
        public string condition { get; set; }
        public string availability { get; set; }
        public string link { get; set; }
        public int updated_t { get; set; }
    }

    public class Item
    {
        public string ean { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string upc { get; set; }
        public string brand { get; set; }
        public string model { get; set; }
        public string dimension { get; set; }
        public string weight { get; set; }
        public string currency { get; set; }
        public double lowest_recorded_price { get; set; }
        public List<string> images { get; set; }
        public List<Offer> offers { get; set; }
    }

    public class UpcItemDb
    {
        public string code { get; set; }
        public int total { get; set; }
        public int offset { get; set; }
        public List<Item> items { get; set; }
    }

}
