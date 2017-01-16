using System;

namespace WineTracker.iOS.Renderers.JSQMessages
{
    public struct JsqMessageLayoutPosition
    {
        public nfloat X, Y, Width, Height;

        public JsqMessageLayoutPosition(nfloat layoutX, nfloat layoutY, nfloat widthOffset, nfloat heightOffSet)
        {
            X = layoutX;
            Y = layoutY;
            Width = widthOffset;
            Height = heightOffSet;

        }
    }
}