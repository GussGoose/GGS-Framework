﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public static partial class AdvancedLabel
	{
		public class Config
		{
			#region Class Members
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
			public Config ()
			{
				Initialize (Defaults.Content, Defaults.BackgroundStyle, Defaults.Color, Defaults.Style, Defaults.Alignment, Defaults.Clipping, Defaults.WordWrap, Defaults.FontSize);
			}

			public Config (string content, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
			{
				Initialize (content, Defaults.BackgroundStyle, Defaults.Color, fontStyle, alignment, clipping, wordWrap, fontSize);
			}

			public Config (string content, GUIStyle backgroundStyle, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
			{
				Initialize (content, backgroundStyle, Defaults.Color, fontStyle, alignment, clipping, wordWrap, fontSize);
			}

			public Config (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
			{
				Initialize (content, backgroundStyle, color, fontStyle, alignment, clipping, wordWrap, fontSize);
			}

			public Config (string content, Color color, FontStyle fontStyle = Defaults.Style, TextAnchor alignment = Defaults.Alignment, TextClipping clipping = Defaults.Clipping, bool wordWrap = Defaults.WordWrap, int fontSize = Defaults.FontSize)
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

			public GUIStyle GetConfiguredStyle ()
			{
				GUIStyle style = new GUIStyle (GUI.skin.GetStyle ("Label"));

				style.normal.textColor = color;
				style.fontStyle = fontStyle;
				style.fontSize = fontSize;

				style.alignment = alignment;
				style.clipping = clipping;
				style.wordWrap = wordWrap;

				return style;
			}

			public Vector2 GetRequieredSize ()
			{
				GUIStyle style = GetConfiguredStyle ();
				return style.CalcSize (new GUIContent (content));
			}

			public float GetRequieredHeight (float width)
			{
				GUIStyle style = GetConfiguredStyle ();
				return style.CalcHeight (new GUIContent (content), width);
			}

			public Rect GetRectWithRequieredHeight (Rect rect)
			{
				float height = GetRequieredHeight (rect.width);
				return new Rect (rect.x, rect.y, rect.width, height);
			}
			#endregion

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
		}
	}
}
#endif