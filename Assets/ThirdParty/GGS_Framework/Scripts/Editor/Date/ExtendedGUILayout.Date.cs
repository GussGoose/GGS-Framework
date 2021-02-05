using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUILayout
    {
        #region Implementation
        public static void DateField (GUIContent label, SerializedProperty property)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            ExtendedGUI.DateField (rect, label, property);
        }

        public static void DateField (string label, SerializedProperty property)
        {
            DateField (new GUIContent (label), property);
        }
        #endregion
    }
}