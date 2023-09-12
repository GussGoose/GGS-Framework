// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class AdvancedRect
    {
        public abstract class Group : Element
        {
            #region Class Accesors
            public Orientation Orientation
            {
                get;
                private set;
            }

            public Element[] Elements
            {
                get;
                private set;
            }
            #endregion

            #region Class Implementation
            public Group (Type type, string key, Orientation orientation, float size, RectPadding padding, Element[] elements, bool use) : base (type, key, size, padding, use)
            {
                if (padding != null)
                {
                    if (orientation == Orientation.Horizontal)
                        Size -= base.padding.offset.horizontal;
                    else
                        Size -= base.padding.offset.vertical;
                }

                Orientation = orientation;
                Elements = elements;
            }

            public void ComputeElements ()
            {
                Rect rect = Rect;

                if (padding != null)
                    rect = padding.Apply (rect);

                ComputeElementsRect (rect, Orientation, Elements);
            }
            #endregion
        }
    }
}