using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	public static partial class ExtendedGUI
    {
        #region Class Implementation
        public static TimeObject TimeObject (Rect rect, GUIContent label, TimeObject value)
        {
            rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), new GUIContent (label));

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.ExpandedItem ("Value"),
                new AdvancedRect.FixedSpace (2),
                new AdvancedRect.FixedItem ("Type", 75)
            );

            value.value = EditorGUI.DoubleField (rects["Value"], value.value);
            value.type = (TimeObjectType) EditorGUI.EnumPopup (rects["Type"], value.type);

            return value;
        }

        public static TimeObject TimeObject (Rect rect, string label, TimeObject value)
        {
            return TimeObject (rect, new GUIContent (label), value);
        }

        public static void TimeObject (Rect rect, GUIContent label, SerializedProperty property)
        {
            EditorGUI.BeginProperty (rect, label, property);

            rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), label);

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.ExpandedItem ("Value"),
                new AdvancedRect.FixedSpace (2),
                new AdvancedRect.FixedItem ("Type", 75)
            );

            EditorGUI.PropertyField (rects["Value"], property.FindPropertyRelative ("value"), GUIContent.none);
            EditorGUI.PropertyField (rects["Type"], property.FindPropertyRelative ("type"), GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty ();
        }

        public static void TimeObject (Rect rect, string label, SerializedProperty property)
        {
            TimeObject (rect, new GUIContent (label), property);
        }
        #endregion
    }
}