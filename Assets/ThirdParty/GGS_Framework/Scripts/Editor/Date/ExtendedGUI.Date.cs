using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUI
    {
        #region Members
        private const string DateTimeFormat = "dd/MM/yyyy";

        private static readonly string[] Months =
        {
            "January",
            "February",
            "March",
            "April",
            "May",
            "June",
            "July",
            "August",
            "September",
            "Octuber",
            "November",
            "December",
        };
        #endregion

        #region Implementation
        public static void DateField (Rect rect, GUIContent label, SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.HelpBox (rect, "The type must be a string.", MessageType.Error);
                return;
            }

            EditorGUI.BeginProperty (rect, label, property);

            Rect contentRect = EditorGUI.PrefixLabel (rect, label);

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (contentRect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.FixedItem ("Day", 30),
                new AdvancedRect.FixedSpace (2),
                new AdvancedRect.ExpandedItem ("Month"),
                new AdvancedRect.FixedSpace (2),
                new AdvancedRect.FixedItem ("Year", 40)
            );

            if (string.IsNullOrEmpty (property.stringValue))
                property.stringValue = DateTime.Now.ToString (DateTimeFormat);

            DateTime currentDate = DateTime.ParseExact (property.stringValue, DateTimeFormat, null);

            EditorGUI.BeginChangeCheck ();
            int year = EditorGUI.IntField (rects["Year"], GUIContent.none, currentDate.Year);
            int month = EditorGUI.Popup (rects["Month"], currentDate.Month - 1, Months) + 1;
            int day = EditorGUI.IntField (rects["Day"], GUIContent.none, currentDate.Day);
            if (EditorGUI.EndChangeCheck ())
                ApplyChangesToString (property, day, month, year);

            EditorGUI.EndProperty ();
        }

        public static void DateField (Rect rect, string label, SerializedProperty property)
        {
            DateField (rect, new GUIContent (label), property);
        }

        private static void ApplyChangesToString (SerializedProperty serializedProperty, int day, int month, int year)
        {
            year = Mathf.Clamp (year, DateTime.MinValue.Year, DateTime.MaxValue.Year);
            month = Mathf.Clamp (month, 0, 11);
            day = Mathf.Clamp (day, 1, DateTime.DaysInMonth (year, month));

            serializedProperty.stringValue = new DateTime (year, month, day).ToString (DateTimeFormat);
            serializedProperty.serializedObject.ApplyModifiedProperties ();
        }
        #endregion
    }
}