using System;

namespace GGS_Framework.Editor
{
    public static partial class AdvancedRect
    {
        [Obsolete ("The use of AdvancedRect.Alignment has been replaced with RectAlignment and it would be removed soon. You should change all the references to this enum.")]
        public enum Alignment
        {
            TopLeft,
            TopCenter,
            TopRight,

            CenterLeft,
            Center,
            CenterRight,

            BottomLeft,
            BottomCenter,
            BottomRight
        }
    }
}