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