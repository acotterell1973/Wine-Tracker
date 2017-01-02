using System;
using Foundation;
using MonoTouch.Dialog;
using UIKit;


namespace WineTracker.iOS.Renderers
{
    public class ChatBubble : Element, IElementSizing
    {
    
        readonly bool _isLeft;

        public ChatBubble(bool isLeft, string text): base(text)
        {
            _isLeft = isLeft;
        }
        public override UITableViewCell GetCell(UITableView tv)
        {
            var cell = tv.DequeueReusableCell(_isLeft ? BubbleCell.KeyLeft : BubbleCell.KeyRight) as BubbleCell ??
                       new BubbleCell(_isLeft);
            cell.Update(Caption);
            return cell;
        }

        public nfloat GetHeight(UITableView tableView, NSIndexPath indexPath)
        {
            return BubbleCell.GetSizeForText(tableView, Caption).Height + BubbleCell.BubblePadding.Height;
        }


    }
}
