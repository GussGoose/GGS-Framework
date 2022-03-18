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