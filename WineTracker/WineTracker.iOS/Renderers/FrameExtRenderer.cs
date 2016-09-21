using System.Drawing;
using CoreGraphics;
using UIKit;
using WineTracker.Extensions;
using WineTracker.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FrameRenderer), typeof(FrameExtRenderer
    ))]
namespace WineTracker.iOS.Renderers
{
    public class FrameExtRenderer : FrameRenderer
    {
        private ExtendedFrame _extendedFrame;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (!(e.NewElement is ExtendedFrame))
            {
                return;
            }

            _extendedFrame = (ExtendedFrame) e.NewElement;
            //     _extendedFrame.updateCornerRadius += SetCornerRadius;
            // Border
      //      this.Layer.CornerRadius = (float)_extendedFrame.CornerRadius;
         //   this.Layer.Bounds.Inset((int)_extendedFrame.BorderThickness, (int)_extendedFrame.BorderThickness);
        //    Layer.BorderColor = _extendedFrame.BorderColor.ToCGColor();
        //    Layer.BorderWidth = (float)_extendedFrame.BorderThickness;

            // Shadow
         //   this.Layer.ShadowColor = UIColor.DarkGray.CGColor;
         //   this.Layer.ShadowOpacity = 0.6f;
         //   this.Layer.ShadowRadius = 2.0f;
         //   this.Layer.ShadowOffset = new SizeF(0, 0);
            //this.Layer.MasksToBounds = true;

         //   SetCornerRadius();

        }

        private void SetCornerRadius()
        {
            if (this.NativeView != null && _extendedFrame != null)
            {
                this.NativeView.Layer.CornerRadius = _extendedFrame.CornerRadius;
                // Child Element Layer
                if (this.Layer.Sublayers.Length > 0)
                {
                    var subLayer = this.Layer.Sublayers[0];
                    subLayer.CornerRadius = (float)_extendedFrame.CornerRadius;
                    subLayer.MasksToBounds = true;
                }
            
            }
        }
    }
}
