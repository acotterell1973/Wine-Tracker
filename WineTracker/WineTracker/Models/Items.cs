namespace WineTracker.Models
{
    public class Items
    {
        public string Ean { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Upc { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Dimension { get; set; }
        public string Weight { get; set; }
        public string Currency { get; set; }
        public double LowestRecordedPrice { get; set; }
    }
}
