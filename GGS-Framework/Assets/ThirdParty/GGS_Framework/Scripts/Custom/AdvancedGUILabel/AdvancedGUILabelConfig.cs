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
            public const int FontSize = 12;
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
            SetDefaults ();
        }

        public AdvancedGUILabelConfig (string content, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
        {
            SetDefaults ();

            Content = content;
            FontStyle = fontStyle;
            FontSize = fontSize;
            Alignment = alignment;
            Clipping = clipping;
            WordWrap = wordWrap;
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
        {
            SetDefaults ();

            Content = content;
            BackgroundStyle = backgroundStyle;
            FontStyle = fontStyle;
            FontSize = fontSize;
            Alignment = alignment;
            Clipping = clipping;
            WordWrap = wordWrap;
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
        {
            SetDefaults ();

            Content = content;
            BackgroundStyle = backgroundStyle;
            Color = color;
            FontStyle = fontStyle;
            FontSize = fontSize;
            Alignment = alignment;
            Clipping = clipping;
            WordWrap = wordWrap;
        }

        public AdvancedGUILabelConfig (string content, Color color, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
        {
            SetDefaults ();

            Content = content;
            Color = color;
            FontStyle = fontStyle;
            FontSize = fontSize;
            Alignment = alignment;
            Clipping = clipping;
            WordWrap = wordWrap;
        }
        #endregion

        private void SetDefaults ()
        {
            BackgroundStyle = Defaults.BackgroundStyle;
            Content = Defaults.Content;
            Color = Defaults.Color;
            FontStyle = Defaults.Style;
            Alignment = Defaults.Alignment;
            Clipping = Defaults.Clipping;
            WordWrap = Defaults.WordWrap;
            FontSize = Defaults.FontSize;
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