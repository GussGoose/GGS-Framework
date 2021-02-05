using System;

namespace GGS_Framework.Editor
{
    public partial class AdvancedRect
    {
        public partial class Padding
        {
            [Flags]
            [Obsolete ("The use of AdvancedRect.Padding.Type has been replaced with RectPadding.Type and it would be removed soon. You should change all the references to this enum.")]
            public enum Type
            {
                Left = 1 << 0,
                Right = 1 << 1,
                Top = 1 << 2,
                Bottom = 1 << 3,

                Horizontal = Left | Right,
                Vertical = Top | Bottom,

                TopLeft = Top | Left,
                TopRight = Top | Right,

                BottomLeft = Bottom | Left,
                BottomRight = Bottom | Right,

                All = Horizontal | Vertical
            }
        }
    }
}