using Xamarin.Forms;

namespace WineTracker.Styles
{
    public static class BrandColor
    {
        //-------------------//
        // Color Definitions // -- these colors are utilized in the "Implementation" section below
        //-------------------//

        // Brand Color Palette -- these are explicitly defined in the style guide
        public static Color Strawberry = Color.FromHex("ec3c3e");
        public static Color SunflowerYellow = Color.FromHex("ffcc00");
        public static Color TurquoiseBlue = Color.FromHex("00b4bf");
        public static Color Black = Color.FromHex("333");
        public static Color PinkishGray = Color.FromHex("ddd");
        public static Color White = Color.White;

        // Supporting Colors -- these are not (yet) explicitly called out in the style guide
        public static Color Purple = Color.FromHex("94368d");
        public static Color WarmGray = Color.FromHex("999999");
        public static Color WarmGrey = WarmGray; // Alias
        public static Color Offwhite = Color.FromHex("f7f7f7");

        // Semantic Color Definitions
        public static Color Default = Black;
        public static Color Success = TurquoiseBlue;
        public static Color Warning = SunflowerYellow;
        public static Color Error = Strawberry;



        //-----------------------//
        // Color Implementations // -- these colors are defined in the "Definitions" section above
        //-----------------------//

        // Button Colors
        public static Color ButtonPrimary = TurquoiseBlue;
        public static Color ButtonSecondary = Black;
        public static Color ButtonDisabled = PinkishGray;

        // Entry Colors
        public static Color Entry = Black;
        public static Color EntryFocused = TurquoiseBlue;

        // Sidebar Colors
        public static Color Sidebar = Black;

        // Sla Colors
        public static Color SlaTimeLeft = Success;
        public static Color SlaTimeClose = Warning;
        public static Color SlaTimeOut = Error;
        public static Color NoSla = WarmGray;

        // Dashboard Colors
        public static Color DashboardBackground = Offwhite;
        public static Color BucketTypeText = WarmGray;
        public static Color BucketShadow = PinkishGray;
        public static Color MessageBar = TurquoiseBlue;
        public static Color MessageBarText = White;

        // Pick Ticket List Colors
        public static Color GetNextButton = WarmGray;

        // Pick Ticket Line Item Colors
        public static Color NotFullyPicked = White;
        public static Color Picked = PinkishGray;
        public static Color PickTicketLineItemBorder = PinkishGray;

        // Pick Ticket List View
        public static Color PickTicketHeaderBackgroundColor = Black;
    }
}
