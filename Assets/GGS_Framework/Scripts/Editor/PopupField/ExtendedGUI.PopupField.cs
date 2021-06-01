using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUI
    {
        #region Class Implementation
        public static bool PopupField (Rect rect, string label, string selected)
        {
            return PopupField (rect, new GUIContent (label), selected);
        }

        public static bool PopupField (Rect rect, GUIContent label, string selected)
        {
            rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), label);

            if (GUI.Button (rect, selected, EditorStyles.popup))
                return true;

            return false;
        }
        #endregion
    }
}