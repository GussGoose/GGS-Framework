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