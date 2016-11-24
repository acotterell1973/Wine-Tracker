namespace WineTracker.Models
{
    public class BottleInfo
    {
        public BottleInfo()
        {
            this.Bottle = new Bottle();
            this.Location = new BottleLocation();
            this.Occasions = new BottleOccasions();
        }


        public Bottle Bottle { get; set; }
        public BottleLocation Location { get; set; }
        public BottleOccasions Occasions { get; set; }

    }
}
