// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;

namespace GGS_Framework.Editor
{
    public static class RectExtensions
    {
        #region Implementation
        public static Rect Expand (this Rect rect, RectPadding padding)
        {
            return ExtendedRect.Expand (rect, padding);
        }
        #endregion
    }
}