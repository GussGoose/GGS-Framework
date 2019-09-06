#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
    public class AdvancedGUILabelConfig
    {
        #region Nested Classes
        public static class Defaults
        {
            public static string Content = string.Empty;
            public static GUIStyle BackgroundStyle = GUIStyle.none;
            public static Color Color = (EditorGUIUtility.isProSkin) ? new Color (0.705f, 0.705f, 0.705f, 1) : Color.black;
            public const FontStyle Style = FontStyle.Normal;
            public const TextAnchor Alignment = TextAnchor.MiddleCenter;
            public const TextClipping Clipping = TextClipping.Clip;
            public const bool WordWrap = true;
            public const int FontSize = 11;
        }
        #endregion

        #region Class Accesors
        public string content;
        public GUIStyle backgroundStyle;

        public Color color;
        public FontStyle fontStyle;
        public TextAnchor alignment;

        public TextClipping clipping;
        public bool wordWrap;
        public int fontSize;
        #endregion

        #region Class Implementation
        #region Constructors
        public AdvancedGUILabelConfig ()
        {
            Initialize (Defaults.Content, Defaults.BackgroundStyle, Defaults.Color, Defaults.Style, Defaults.Alignment, Defaults.Clipping, Defaults.WordWrap, Defaults.FontSize);
        }

        public AdvancedGUILabelConfig (string content, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
        {
            Initialize (content, Defaults.BackgroundStyle, Defaults.Color, fontStyle, alignment, clipping, wordWrap, fontSize);
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
        {
            Initialize (content, backgroundStyle, Defaults.Color, fontStyle, alignment, clipping, wordWrap, fontSize);
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
        {
            Initialize (content, backgroundStyle, color, fontStyle, alignment, clipping, wordWrap, fontSize);
        }

        public AdvancedGUILabelConfig (string content, Color color, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
        {
            Initialize (content, Defaults.BackgroundStyle, color, fontStyle, alignment, clipping, wordWrap, fontSize);
        }
        #endregion

        private void Initialize (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
        {
            this.content = content;
            this.backgroundStyle = new GUIStyle (backgroundStyle);
            this.color = color;
            this.fontStyle = fontStyle;
            this.fontSize = fontSize;
            this.alignment = alignment;
            this.clipping = clipping;
            this.wordWrap = wordWrap;
        }

        public float GetRequieredHeight (float width)
        {
            GUIStyle style = AdvancedGUILabel.GetConfiguredStyle (this);
            return style.CalcHeight (new GUIContent (content), width);
        }

        public Rect GetRectWithRequieredHeight (Rect rect)
        {
            float height = GetRequieredHeight (rect.width);
            return new Rect (rect.x, rect.y, rect.width, height);
        }
        #endregion
    }
}
#endif