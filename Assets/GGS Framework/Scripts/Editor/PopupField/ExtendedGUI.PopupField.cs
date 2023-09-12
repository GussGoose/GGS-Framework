// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUI
    {
        #region Class Implementation
        public static bool PopupField (Rect rect, string label, string selected)
        {
            return PopupField (rect, new GUIContent (label), selected, out Rect buttonRect);
        }

        public static bool PopupField (Rect rect, GUIContent label, string selected)
        {
            return PopupField (rect, label, selected, out Rect buttonRect);
        }

        public static bool PopupField (Rect rect, string label, string selected, out Rect buttonRect)
        {
            return PopupField (rect, new GUIContent (label), selected, out buttonRect);
        }

        public static bool PopupField (Rect rect, GUIContent label, string selected, out Rect buttonRect)
        {
            rect.height = EditorGUIUtility.singleLineHeight;
            buttonRect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), label);

            if (GUI.Button (buttonRect, selected, EditorStyles.popup))
                return true;

            return false;
        }
        #endregion
    }
}