using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUI
    {
        #region Implementation
        public static Enumeration Enumeration (Rect rect, GUIContent label, Enumeration enumeration)
        {
            if (!EnumerationsValidator.AreAllEnumerationsValid)
            {
                Rect fieldLabel = EditorGUI.PrefixLabel (rect, label);

                if (GUI.Button (fieldLabel, "There's a duplicated member definition. Click here.", EnumerationGUIUtility.LabelButtonStyle))
                    EnumerationsValidator.LogValidationErrors ();

                return enumeration;
            }

            Enumeration[] enumerations = GGS_Framework.Enumeration.GetMerbersOf (enumeration.GetType ());
            Enumeration selected = enumerations.FirstOrDefault (query => query.ID == enumeration.ID);
            int selectedIndex = Array.IndexOf (enumerations, selected);

            EditorGUI.BeginChangeCheck ();
            selectedIndex = EditorGUI.Popup (rect, label, selectedIndex, enumerations.Select (enumQuery => new GUIContent (enumQuery.Name)).ToArray ());
            if (EditorGUI.EndChangeCheck ())
                enumeration = enumerations[selectedIndex];

            return enumeration;
        }

        public static Enumeration Enumeration (Rect rect, string label, Enumeration enumeration)
        {
            return Enumeration (rect, new GUIContent (label), enumeration);
        }

        public static void Enumeration (Rect rect, GUIContent label, SerializedProperty property)
        {
            if (!EnumerationsValidator.AreAllEnumerationsValid)
            {
                Rect fieldLabel = EditorGUI.PrefixLabel (rect, label);

                if (GUI.Button (fieldLabel, "There's a duplicated member definition. Click here.", EnumerationGUIUtility.LabelButtonStyle))
                    EnumerationsValidator.LogValidationErrors ();

                return;
            }

            SerializedProperty idProperty = property.FindPropertyRelative ("id");

            Type enumerationType = EnumerationsValidator.CatchedEnumerationTypes.FirstOrDefault (enumQuery => enumQuery.Name == property.type);
            Enumeration[] enumerations = GGS_Framework.Enumeration.GetMerbersOf (enumerationType);
            int selectedIndex = Array.IndexOf (enumerations, enumerations.FirstOrDefault (query => query.ID == idProperty.intValue));

            EditorGUI.BeginProperty (rect, label, property);

            EditorGUI.BeginChangeCheck ();
            selectedIndex = EditorGUI.Popup (rect, label, selectedIndex, enumerations.Select (enumQuery => new GUIContent (enumQuery.Name)).ToArray ());
            if (EditorGUI.EndChangeCheck ())
                idProperty.intValue = enumerations[selectedIndex].ID;

            EditorGUI.EndProperty ();
        }

        public static void Enumeration (Rect rect, string label, SerializedProperty property)
        {
            Enumeration (rect, new GUIContent (label), property);
        }
        #endregion
    }
}