// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;

namespace GGS_Framework.Editor
{
    public class RectPadding
    {
        #region Members
        public readonly RectOffset offset;
        #endregion

        #region Implementation
        public RectPadding (int padding, RectPaddingType type)
        {
            int left = ((type & RectPaddingType.Left) != 0) ? padding : 0;
            int right = ((type & RectPaddingType.Right) != 0) ? padding : 0;
            int top = ((type & RectPaddingType.Top) != 0) ? padding : 0;
            int bottom = ((type & RectPaddingType.Bottom) != 0) ? padding : 0;

            this.offset = new RectOffset (left, right, top, bottom);
        }

        public RectPadding (RectOffset offset)
        {
            this.offset = offset;
        }

        public Rect Apply (Rect rect)
        {
            return offset.Add (rect);
        }
        #endregion
    }
}