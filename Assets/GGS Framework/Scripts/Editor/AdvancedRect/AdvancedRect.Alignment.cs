// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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