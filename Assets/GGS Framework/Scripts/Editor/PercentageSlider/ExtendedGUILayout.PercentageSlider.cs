// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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