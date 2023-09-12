// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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