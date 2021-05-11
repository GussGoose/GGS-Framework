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