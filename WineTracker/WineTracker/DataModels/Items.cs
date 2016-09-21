using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineTracker.DataModels
{
    public class Items
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
    }
}
