#if UNITY_EDITOR
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
            Rect rect = GUILayoutUtility.GetRect (new GUIContent (config.content), GetConfiguredStyle (config), GUILayout.ExpandWidth (true));
            DoDraw (rect, config);
        }

        private static void DoDraw (Rect rect, AdvancedGUILabelConfig config)
        {
            config.backgroundStyle.fixedHeight = rect.height;
            GUI.Box (rect, "", config.backgroundStyle);

            GUIStyle labelStyle = GetConfiguredStyle (config);
            GUI.Label (rect, config.content, labelStyle);
        }

        public static float GetRequieredHeight (AdvancedGUILabelConfig config, float width)
        {
            GUIStyle style = GetConfiguredStyle (config);
            return style.CalcHeight (new GUIContent (config.content), width);
        }

        public static GUIStyle GetConfiguredStyle (AdvancedGUILabelConfig config)
        {
            GUIStyle style = new GUIStyle (GUI.skin.GetStyle ("Label"));

            style.normal.textColor = config.color;
            style.fontStyle = config.fontStyle;
            style.fontSize = config.fontSize;

            style.alignment = config.alignment;
            style.clipping = config.clipping;
            style.wordWrap = config.wordWrap;

            return style;
        }
        #endregion
    }
}
#endif