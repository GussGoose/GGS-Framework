using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUILayout
    {
        #region Implementation
        public static Enumeration Enumeration (GUIContent label, Enumeration enumeration)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            return ExtendedGUI.Enumeration (rect, label, enumeration);
        }

        public static Enumeration Enumeration (string label, Enumeration enumeration)
        {
            return Enumeration (new GUIContent (label), enumeration);
        }

        public static void Enumeration (GUIContent label, SerializedProperty property)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            ExtendedGUI.Enumeration (rect, label, property);
        }

        public static void Enumeration (string label, SerializedProperty property)
        {
            Enumeration (new GUIContent (label), property);
        }
        #endregion
    }
}