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
        public string Content
        {
            get; internal set;
        }

        public GUIStyle BackgroundStyle
        {
            get; internal set;
        }

        public Color Color
        {
            get; internal set;
        }

        public FontStyle FontStyle
        {
            get; internal set;
        }

        public TextAnchor Alignment
        {
            get; internal set;
        }

        public TextClipping Clipping
        {
            get; internal set;
        }

        public bool WordWrap
        {
            get; internal set;
        }

        public int FontSize
        {
            get; internal set;
        }
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
            Content = content;
            BackgroundStyle = new GUIStyle (backgroundStyle);
            Color = color;
            FontStyle = fontStyle;
            FontSize = fontSize;
            Alignment = alignment;
            Clipping = clipping;
            WordWrap = wordWrap;
        }

        public float GetRequieredHeight (float width)
        {
            GUIStyle style = AdvancedGUILabel.GetConfiguredStyle (this);
            return style.CalcHeight (new GUIContent (Content), width);
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