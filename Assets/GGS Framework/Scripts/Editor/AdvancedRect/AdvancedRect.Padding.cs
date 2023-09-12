// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public partial class AdvancedRect
    {
        [Obsolete("The use of AdvancedRect.Padding has been replaced with RectPadding and it would be removed soon. You should change all the references to this class.")]
        public partial class Padding
        {
            #region Class Accesors
            public RectOffset offset
            {
                get;
                private set;
            }
            #endregion

            #region Class Implementation
            public Padding (int offset, Type type)
            {
                int left = ((type & Type.Left) != 0) ? offset : 0;
                int right = ((type & Type.Right) != 0) ? offset : 0;
                int top = ((type & Type.Top) != 0) ? offset : 0;
                int bottom = ((type & Type.Bottom) != 0) ? offset : 0;

                this.offset = new RectOffset (left, right, top, bottom);
            }

            public Padding (RectOffset offset)
            {
                this.offset = offset;
            }

            public Rect Apply (Rect rect)
            {
                return offset.Add (rect);
            }

            public static implicit operator RectPadding (Padding padding)
            {
                if (padding == null)
                    return null;
                
                return new RectPadding (padding.offset);
            }
            
            public static explicit operator Padding (RectPadding padding)
            {
                if (padding == null)
                    return null;
                
                return new Padding (padding.offset);
            }
            #endregion
        }
    }
}