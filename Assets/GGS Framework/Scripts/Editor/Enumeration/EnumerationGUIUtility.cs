// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;

namespace GGS_Framework.Editor
{
    public static class EnumerationGUIUtility
    {
        #region Members
        public static readonly GUIStyle LabelButtonStyle = GetLabelButtonStyle ();
        #endregion

        #region Implementation
        private static GUIStyle GetLabelButtonStyle ()
        {
            GUIStyle style = new GUIStyle ();
            RectOffset border = style.border;
            style.normal.textColor = Color.red;
            border.left = 0;
            border.top = 0;
            border.right = 0;
            border.bottom = 0;

            return style;
        }
        #endregion
    }
}