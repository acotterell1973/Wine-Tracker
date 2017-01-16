using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

