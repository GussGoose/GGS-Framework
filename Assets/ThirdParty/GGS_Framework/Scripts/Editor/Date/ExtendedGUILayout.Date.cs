using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUILayout
    {
        #region Implementation
        public static string DateField (GUIContent label, string date)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            return ExtendedGUI.DateField (rect, label, date);
        }

        public static string DateField (string label, string date)
        {
            return DateField (new GUIContent (label), date);
        }

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