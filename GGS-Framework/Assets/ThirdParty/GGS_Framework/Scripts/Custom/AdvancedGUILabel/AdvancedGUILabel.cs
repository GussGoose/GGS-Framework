#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
    public static class AdvancedGUILabel
    {
        #region Class Implementation
        public static void Draw (AdvancedGUILabelConfig config)
        {
            DoDraw (config);
        }

        public static void Draw (Rect rect, AdvancedGUILabelConfig config)
        {
            DoDraw (rect, config);
        }

        private static void DoDraw (AdvancedGUILabelConfig config)
        {
            Rect rect = GUILayoutUtility.GetRect (new GUIContent (config.Content), GetConfiguredStyle (config), GUILayout.ExpandWidth (true));
            DoDraw (rect, config);
        }

        private static void DoDraw (Rect rect, AdvancedGUILabelConfig config)
        {
            config.BackgroundStyle.fixedHeight = rect.height;
            GUI.Box (rect, "", config.BackgroundStyle);

            GUIStyle labelStyle = GetConfiguredStyle (config);
            GUI.Label (rect, config.Content, labelStyle);
        }

        public static float GetRequieredHeight (AdvancedGUILabelConfig config, float width)
        {
            GUIStyle style = GetConfiguredStyle (config);
            return style.CalcHeight (new GUIContent (config.Content), width);
        }

        public static GUIStyle GetConfiguredStyle (AdvancedGUILabelConfig config)
        {
            GUIStyle style = new GUIStyle (GUI.skin.GetStyle ("Label"));

            style.normal.textColor = config.Color;
            style.fontStyle = config.FontStyle;
            style.fontSize = config.FontSize;

            style.alignment = config.Alignment;
            style.clipping = config.Clipping;
            style.wordWrap = config.WordWrap;

            return style;
        }
        #endregion
    }
}
#endif