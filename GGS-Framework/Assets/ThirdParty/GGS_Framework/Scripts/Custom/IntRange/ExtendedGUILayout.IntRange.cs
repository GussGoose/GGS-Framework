#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public static partial class ExtendedGUILayout
    {
        #region Class Implementation
        public static IntRange IntRange (GUIContent label, IntRange value)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            return ExtendedGUI.IntRange (rect, label, value);
        }

        public static IntRange IntRange (string label, IntRange value)
        {
            return IntRange (new GUIContent (label), value);
        }

        public static void IntRange (GUIContent label, SerializedProperty property)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            ExtendedGUI.IntRange (rect, label, property);
        }

        public static void IntRange (string label, SerializedProperty property)
        {
            IntRange (new GUIContent (label), property);
        }
        #endregion
    }
}
#endif