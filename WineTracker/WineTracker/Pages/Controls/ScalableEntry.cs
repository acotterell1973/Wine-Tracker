using Xamarin.Forms;

namespace WineTracker.Pages.Controls
{
    public class ScalableEntry : Editor
    {
        public ScalableEntry()
        {
            this.TextChanged += (sender, e) => { this.InvalidateMeasure(); };
        }
    }
}

