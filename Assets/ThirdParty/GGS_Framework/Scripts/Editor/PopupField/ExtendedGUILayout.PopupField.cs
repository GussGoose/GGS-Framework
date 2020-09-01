using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public partial class ExtendedGUILayout
    {
        #region Class Implementation
        public static bool PopupField (string label, string selected)
        {
            return PopupField (label, selected);
        }

        public static bool PopupField (GUIContent label, string selected)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            return ExtendedGUI.PopupField (rect, label, selected);
        }
        #endregion
    }
}