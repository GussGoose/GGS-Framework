// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUILayout
    {
        #region Class Implementation
        public static FloatRange FloatRange (GUIContent label, FloatRange value)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            return ExtendedGUI.FloatRange (rect, label, value);
        }

        public static FloatRange FloatRange (string label, FloatRange value)
        {
            return FloatRange (new GUIContent (label), value);
        }

        public static void FloatRange (GUIContent label, SerializedProperty property)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            ExtendedGUI.FloatRange (rect, label, property);
        }

        public static void FloatRange (string label, SerializedProperty property)
        {
            FloatRange (new GUIContent (label), property);
        }
        #endregion
    }
}