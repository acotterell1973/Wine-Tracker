namespace WineTracker.Pages
{
    public partial class ChatBotPage : BasePage
    {
        public ChatBotPage()
        {
            InitializeComponent();
        }

        #region Public Properties
        //set the public fields that describes the sender (specific for ios Renderer)
        public static string SenderName { get; set; } = "Ravi";

        public static string SenderId { get; set; } = "2";
        #endregion
    }
}
