using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
    public partial class AdvancedRect
    {
        public partial class Padding
        {
            #region Class Members
            private RectOffset offset;
            #endregion

            #region Class Implementation
            public Padding (int size, Type type)
            {
                int left = ((type & Type.Left) != 0) ? size : 0;
                int right = ((type & Type.Right) != 0) ? size : 0;
                int top = ((type & Type.Top) != 0) ? size : 0;
                int bottom = ((type & Type.Bottom) != 0) ? size : 0;

                offset = new RectOffset (left, right, top, bottom);
            }

            public Rect Apply (Rect rect)
            {
                return offset.Add (rect);
            }
            #endregion
        }
    }
}
