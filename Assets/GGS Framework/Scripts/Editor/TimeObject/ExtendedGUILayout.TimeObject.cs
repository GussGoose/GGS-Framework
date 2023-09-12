// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public partial class ExtendedGUILayout
    {
        #region Class Implementation
        public static TimeObject TimeObject (GUIContent label, TimeObject value)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            return ExtendedGUI.TimeObject (rect, label, value);
        }

        public static TimeObject TimeObject (string label, TimeObject value)
        {
            return TimeObject (new GUIContent (label), value);
        }

        public static void TimeObject (GUIContent label, SerializedProperty property)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            ExtendedGUI.TimeObject (rect, label, property);
        }

        public static void TimeObject (string label, SerializedProperty property)
        {
            TimeObject (new GUIContent (label), property);
        }
        #endregion
    }
}