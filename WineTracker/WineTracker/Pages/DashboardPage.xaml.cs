using WineTracker.Styles;

namespace WineTracker.Pages
{
    public partial class DashboardPage : BasePage
    {
        public DashboardPage()
        {
            InitializeComponent();
            WhiteLabelCount.Style = AppStyle.LabelStyle.LargeLabel;
        }
    }
}
