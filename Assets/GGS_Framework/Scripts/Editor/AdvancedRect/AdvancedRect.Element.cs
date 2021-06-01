using System;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class AdvancedRect
    {
        public abstract partial class Element
        {
            #region Class Members
            protected readonly RectPadding padding;
            #endregion

            #region Class Accesors
            public Type ElementType
            {
                get;
                private set;
            }

            public string Key
            {
                get;
                internal set;
            }

            public float Size
            {
                get;
                protected set;
            }

            public Rect Rect
            {
                get;
                internal set;
            }

            public bool Use
            {
                get;
                protected set;
            }
            #endregion

            #region Class Implementation
            [Obsolete("The use of AdvancedRect.Padding has been replaced with RectPadding and it would be removed soon. You should change all the references to this overload.")]
            protected Element (Type type, string key, float size, Padding padding, bool use) : this (type, key, size, (RectPadding) padding, use)
            {
            }

            protected Element (Type type, string key, float size, RectPadding padding, bool use)
            {
                ElementType = type;

                Key = key;
                Size = size;

                this.padding = padding;

                Use = use;
            }

            public void ApplyPadding ()
            {
                if (padding != null)
                    Rect = padding.Apply (Rect);
            }
            #endregion
        }
    }
}