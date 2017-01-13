using System.Drawing;
using Foundation;
using UIKit;

namespace WineTracker.iOS.Renderers
{
    public sealed class BubbleCell : UITableViewCell
    {
        public static NSString KeyLeft = new NSString("BubbleElementLeft");
        public static NSString KeyRight = new NSString("BubbleElementRight");
        public static UIImage Bleft, Bright, Left, Right;
        public static UIFont Font = UIFont.SystemFontOfSize(14);
        readonly UIView _view;
        readonly UIView _imageView;
        readonly UILabel _label;
        readonly bool _isLeft;

        static BubbleCell()
        {
            Bright = UIImage.FromFile("green.png");
            Bleft = UIImage.FromFile("grey.png");

            // buggy, see https://bugzilla.xamarin.com/show_bug.cgi?id=6177
            //Left = Bleft.CreateResizableImage (new UIEdgeInsets (10, 16, 18, 26));
            //Right = Bright.CreateResizableImage (new UIEdgeInsets (11, 11, 17, 18));
            Left = Bleft.StretchableImage(26, 16);
            Right = Bright.StretchableImage(11, 11);
        }

        public BubbleCell(bool isLeft)
            : base(UITableViewCellStyle.Default, isLeft ? KeyLeft : KeyRight)
        {
            var rect = new RectangleF(0, 0, 1, 1);
            _isLeft = isLeft;
            _view = new UIView(rect);
            _imageView = new UIImageView(isLeft ? Left : Right);
            _view.AddSubview(_imageView);
            _label = new UILabel(rect)
            {
                LineBreakMode = UILineBreakMode.WordWrap,
                Lines = 0,
                Font = Font,
                BackgroundColor = UIColor.Clear
            };
            _view.AddSubview(_label);
            ContentView.Add(_view);
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            var frame = ContentView.Frame;
            var size = GetSizeForText(this, _label.Text) + BubblePadding;
            _imageView.Frame = new RectangleF(new PointF((float) (_isLeft ? 10 : frame.Width - size.Width - 10), (float) frame.Y), size);
            _view.SetNeedsDisplay();
            frame = _imageView.Frame;
            _label.Frame = new RectangleF(new PointF((float) (frame.X + (_isLeft ? 12 : 8)), (float) (frame.Y + 6)), size - BubblePadding);
        }

        internal static SizeF BubblePadding = new SizeF(22, 16);

        internal static SizeF GetSizeForText(UIView tv, string text)
        {
            var nsText = new NSString(text);
            nsText.GetSizeUsingAttributes(new UIStringAttributes { Font = Font });
            var options = NSStringDrawingOptions.UsesFontLeading | NSStringDrawingOptions.UsesLineFragmentOrigin;
            var attributes = new UIStringAttributes
            {
                Font = Font
            };

            var boundSize = new SizeF((float) (tv.Bounds.Width*.7f - 10 - 22), 99999);
            var sizeF = nsText.GetBoundingRect(boundSize, options, attributes, null).Size;
            return (SizeF) sizeF;
        }

        public void Update(string text)
        {
            _label.Text = text;
            SetNeedsLayout();
        }

        public float GetHeight(UIView tv)
        {
            return GetSizeForText(tv, _label.Text).Height + BubblePadding.Height;
        }
    }
}
