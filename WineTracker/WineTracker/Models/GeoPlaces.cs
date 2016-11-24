using System.Collections.Generic;

namespace WineTracker.Models
{
    
    public class Photo
    {
        public int height { get; set; }
        public List<string> html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }

    public class OpeningHours
    {
        public bool open_now { get; set; }
        public List<object> weekday_text { get; set; }
    }

    public partial class Result
    {
      //  public Geometry geometry { get; set; }
        public string icon { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    //    public string place_id { get; set; }
        public string reference { get; set; }
        public string scope { get; set; }
     //   public List<string> types { get; set; }
        public string vicinity { get; set; }
        public List<Photo> photos { get; set; }
        public OpeningHours opening_hours { get; set; }
        public double? rating { get; set; }
        public int? price_level { get; set; }
        public bool? permanently_closed { get; set; }
    }

    public class GeoPlaces
    {
        public List<object> html_attributions { get; set; }
        public List<Result> results { get; set; }
        public string status { get; set; }
    }


}
