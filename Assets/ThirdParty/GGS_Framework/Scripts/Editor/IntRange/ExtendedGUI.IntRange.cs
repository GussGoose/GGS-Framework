using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	public static partial class ExtendedGUI
    {
        #region Class Implementation
        public static IntRange IntRange (Rect rect, GUIContent label, IntRange value)
        {
            rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), label);

            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.ExpandedItem ("Start"),
                new AdvancedRect.FixedSpace (5),
                new AdvancedRect.ExpandedItem ("End")
            );

            float lastLabelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = 35;
            value.start = EditorGUI.IntField (rects["Start"], "Start", value.start);
            EditorGUIUtility.labelWidth = 30;
            value.end = EditorGUI.IntField (rects["End"], "End", value.end);
            EditorGUIUtility.labelWidth = lastLabelWidth;

            EditorGUI.indentLevel = indentLevel;

            return value;
        }

        public static IntRange IntRange (Rect rect, string label, IntRange value)
        {
            return IntRange (rect, new GUIContent (label), value);
        }

        public static void IntRange (Rect rect, GUIContent label, SerializedProperty property)
        {
            EditorGUI.BeginProperty (rect, label, property);

            rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), label);

            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.ExpandedItem ("Start"),
                new AdvancedRect.FixedSpace (5),
                new AdvancedRect.ExpandedItem ("End")
            );

            EditorGUIUtility.labelWidth = 35;
            EditorGUI.PropertyField (rects["Start"], property.FindPropertyRelative ("start"));
            EditorGUIUtility.labelWidth = 30;
            EditorGUI.PropertyField (rects["End"], property.FindPropertyRelative ("end"));
            EditorGUIUtility.labelWidth = 0;

            EditorGUI.indentLevel = indentLevel;

            EditorGUI.EndProperty ();
        }

        public static void IntRange (Rect rect, string label, SerializedProperty property)
        {
            IntRange (rect, new GUIContent (label), property);
        }
        #endregion
    }
}