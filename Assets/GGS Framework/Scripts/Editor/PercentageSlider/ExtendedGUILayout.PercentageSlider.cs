using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static partial class ExtendedGUILayout
    {
        #region Implementation
        public static void PercentageSlider (GUIContent label, ref float[] percentages, string[] labels, PercentageSliderState state)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight * 2));
            ExtendedGUI.PercentageSlider (rect, label, ref percentages, labels, state);
        }

        public static void PercentageSlider (string label, ref float[] percentages, string[] labels, PercentageSliderState state)
        {
            PercentageSlider (new GUIContent (label), ref percentages, labels, state);
        }

        public static void PercentageSlider (GUIContent label, SerializedProperty[] percentagesProperties, string[] labels, PercentageSliderState state)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight * 2));
            ExtendedGUI.PercentageSlider (rect, label, percentagesProperties, labels, state);
        }

        public static void PercentageSlider (string label, SerializedProperty[] percentagesProperties, string[] labels, PercentageSliderState state)
        {
            PercentageSlider (new GUIContent (label), percentagesProperties, labels, state);
        }
        #endregion
    }
}