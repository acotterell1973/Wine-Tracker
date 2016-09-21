using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WineTracker.Extensions
{
    public class ExtendedFrame : Frame
    {
       // public Update updateCornerRadius;

        private float cornerRadius;
        public float CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                cornerRadius = value;

                //if (updateCornerRadius != null)
                //{
                //  //  updateCornerRadius();
                //}
            }
        }

        public ExtendedFrame() : base()
        {
            //Setting thickness and background color allows
            //Rounded Corners
        //    Padding = new Thickness(1.0);
       //     BackgroundColor = Color.Black;
        //    CornerRadius = 0.0f;
        }
    }
}
