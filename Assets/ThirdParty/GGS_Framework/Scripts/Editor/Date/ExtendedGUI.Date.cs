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
        public static string DateField (Rect rect, GUIContent label, string date)
        {
            Rect contentRect = EditorGUI.PrefixLabel (rect, label);

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (contentRect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.FixedItem ("Day", 30),
                new AdvancedRect.FixedSpace (2),
                new AdvancedRect.ExpandedItem ("Month"),
                new AdvancedRect.FixedSpace (2),
                new AdvancedRect.FixedItem ("Year", 40)
            );

            DateTime dateTime = DateTime.ParseExact (date, DateTimeFormat, null);

            EditorGUI.BeginChangeCheck ();
            int year = EditorGUI.IntField (rects["Year"], GUIContent.none, dateTime.Year);
            int month = EditorGUI.Popup (rects["Month"], dateTime.Month - 1, Months) + 1;
            int day = EditorGUI.IntField (rects["Day"], GUIContent.none, dateTime.Day);
            if (EditorGUI.EndChangeCheck ())
                dateTime = ApplyToDate (day, month, year);

            return dateTime.ToString (DateTimeFormat);
        }

        public static string DateField (Rect rect, string label, string date)
        {
            return DateField (rect, new GUIContent (label), date);
        }

        public static void DateField (Rect rect, GUIContent label, SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.HelpBox (rect, "The type must be a string.", MessageType.Error);
                return;
            }

            EditorGUI.BeginProperty (rect, label, property);

            if (string.IsNullOrEmpty (property.stringValue))
                property.stringValue = DateTime.Now.ToString (DateTimeFormat);

            EditorGUI.BeginChangeCheck ();
            string dateValue = DateField (rect, label, property.stringValue);
            if (EditorGUI.EndChangeCheck ())
            {
                property.stringValue = dateValue;
                property.serializedObject.ApplyModifiedProperties ();
            }

            EditorGUI.EndProperty ();
        }

        public static void DateField (Rect rect, string label, SerializedProperty property)
        {
            DateField (rect, new GUIContent (label), property);
        }

        private static DateTime ApplyToDate (int day, int month, int year)
        {
            year = Mathf.Clamp (year, DateTime.MinValue.Year, DateTime.MaxValue.Year);
            month = Mathf.Clamp (month, 0, 11);
            day = Mathf.Clamp (day, 1, DateTime.DaysInMonth (year, month));

            return new DateTime (year, month, day);
        }
        #endregion
    }
}