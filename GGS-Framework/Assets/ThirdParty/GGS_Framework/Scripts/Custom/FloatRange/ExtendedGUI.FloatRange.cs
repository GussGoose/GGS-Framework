#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
    public static partial class ExtendedGUI
    {
        #region Class Implementation
        public static FloatRange FloatRange (Rect rect, GUIContent label, FloatRange value)
        {
            rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), label);

            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.ExpandedItem ("Start"),
                new AdvancedRect.Space (5),
                new AdvancedRect.ExpandedItem ("End")
            );

            float lastLabelWidth = EditorGUIUtility.labelWidth;

            EditorGUIUtility.labelWidth = 35;
            value.start = EditorGUI.FloatField (rects["Start"], "Start", value.start);
            EditorGUIUtility.labelWidth = 30;
            value.end = EditorGUI.FloatField (rects["End"], "End", value.end);
            EditorGUIUtility.labelWidth = lastLabelWidth;

            EditorGUI.indentLevel = indentLevel;

            return value;
        }

        public static FloatRange FloatRange (Rect rect, string label, FloatRange value)
        {
            return FloatRange (rect, new GUIContent (label), value);
        }

        public static void FloatRange (Rect rect, GUIContent label, SerializedProperty property)
        {
            EditorGUI.BeginProperty (rect, label, property);

            rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), label);

            int indentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.ExpandedItem ("Start"),
                new AdvancedRect.Space (5),
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

        public static void FloatRange (Rect rect, string label, SerializedProperty property)
        {
            FloatRange (rect, new GUIContent (label), property);
        }
        #endregion
    }
}
#endif