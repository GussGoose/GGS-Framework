using UnityEngine;

namespace GGS_Framework
{
    public static class GUIStyleExtensions
    {
        #region Implementation
        public static void Draw (this GUIStyle guiStyle, Rect rect)
        {
            if (Event.current.type == EventType.Repaint)
                guiStyle.Draw (rect, false, false, false, false);
        }
        #endregion
    }
}