#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
    public class AdvancedGUILabelConfig
    {
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

        public int FontSize
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
        #endregion

        #region Class Implementation
        #region Constructors
        public AdvancedGUILabelConfig ()
        {
            SetDefaults ();
        }

        public AdvancedGUILabelConfig (string content)
        {
            SetDefaults ();

            Content = content;
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle)
        {
            SetDefaults ();

            Content = content;
            BackgroundStyle = backgroundStyle;
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, Color color)
        {
            SetDefaults ();

            Content = content;
            BackgroundStyle = backgroundStyle;

            Color = color;
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle)
        {
            SetDefaults ();

            Content = content;
            BackgroundStyle = backgroundStyle;

            Color = color;
            FontStyle = fontStyle;
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle, int fontSize)
        {
            SetDefaults ();

            Content = content;
            BackgroundStyle = backgroundStyle;

            Color = color;
            FontStyle = fontStyle;
            FontSize = fontSize;
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle, int fontSize, TextAnchor alignment)
        {
            SetDefaults ();

            Content = content;
            BackgroundStyle = backgroundStyle;

            Color = color;
            FontStyle = fontStyle;
            FontSize = fontSize;

            Alignment = alignment;
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle, int fontSize, TextAnchor alignment, TextClipping clipping)
        {
            SetDefaults ();

            Content = content;
            BackgroundStyle = backgroundStyle;

            Color = color;
            FontStyle = fontStyle;
            FontSize = fontSize;

            Alignment = alignment;
            Clipping = clipping;
        }

        public AdvancedGUILabelConfig (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle, int fontSize, TextAnchor alignment, TextClipping clipping, bool wordWrap)
        {
            Content = content;
            BackgroundStyle = backgroundStyle;

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
            BackgroundStyle = new GUIStyle (EditorStyles.label);

            Color = (EditorGUIUtility.isProSkin) ? new Color (0.705f, 0.705f, 0.705f, 1) : Color.black;
            FontStyle = FontStyle.Normal;
            FontSize = EditorStyles.label.fontSize;

            Alignment = TextAnchor.MiddleCenter;
            Clipping = TextClipping.Clip;
            WordWrap = true;
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