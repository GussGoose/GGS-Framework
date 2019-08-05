#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public static partial class ExtendedGUI
	{
		#region Class members
		private static float requieredLabelWidth;
		#endregion

		#region Class accesors
		public static Color DefaultLabelColor
		{
			get { return (EditorGUIUtility.isProSkin) ? new Color (0.705f, 0.705f, 0.705f, 1) : Color.black; }
		}

		private static float RequieredLabelWidth
		{
			get
			{
				float currentRectWidth = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (0)).width;

				if (Event.current.type == EventType.Repaint)
					requieredLabelWidth = currentRectWidth;

				return requieredLabelWidth;
			}
		}
		#endregion

		#region Class implementation
		#region Automatic Layout
		public static void DrawLabel (string content)
		{
			DoDrawLabel (content, null, DefaultLabelColor, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawLabel (string content, Color color)
		{
			DoDrawLabel (content, null, color, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawLabel (string content, Color color, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawLabel (content, null, color, fontStyle, aligment);
		}

		public static void DrawLabel (string content, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawLabel (content, null, DefaultLabelColor, fontStyle, aligment);
		}

		public static void DrawLabel (string content, GUIStyle backgroundStyle)
		{
			DoDrawLabel (content, backgroundStyle, DefaultLabelColor, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawLabel (string content, GUIStyle backgroundStyle, Color color)
		{
			DoDrawLabel (content, backgroundStyle, color, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawLabel (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawLabel (content, backgroundStyle, color, fontStyle, aligment);
		}

		public static void DrawLabel (string content, GUIStyle backgroundStyle, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawLabel (content, backgroundStyle, DefaultLabelColor, fontStyle, aligment);
		}
		#endregion

		#region Without Automatic Layout
		public static void DrawLabel (Rect rect, string content)
		{
			DoDrawLabel (rect, content, null, DefaultLabelColor, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawLabel (Rect rect, string content, Color color)
		{
			DoDrawLabel (rect, content, null, color, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawLabel (Rect rect, string content, Color color, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawLabel (rect, content, null, color, fontStyle, aligment);
		}

		public static void DrawLabel (Rect rect, string content, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawLabel (rect, content, null, DefaultLabelColor, fontStyle, aligment);
		}

		public static void DrawLabel (Rect rect, string content, GUIStyle backgroundStyle)
		{
			DoDrawLabel (rect, content, backgroundStyle, DefaultLabelColor, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawLabel (Rect rect, string content, GUIStyle backgroundStyle, Color color)
		{
			DoDrawLabel (rect, content, backgroundStyle, color, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawLabel (Rect rect, string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawLabel (rect, content, backgroundStyle, color, fontStyle, aligment);
		}

		public static void DrawLabel (Rect rect, string content, GUIStyle backgroundStyle, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawLabel (rect, content, backgroundStyle, DefaultLabelColor, fontStyle, aligment);
		}
		#endregion

		public static float CalculateLabelHeight (string content, FontStyle fontStyle, TextAnchor textAligment)
		{
			GUIStyle labelStyle = new GUIStyle (GUI.skin.GetStyle ("Label"));
			labelStyle.fontStyle = fontStyle;
			labelStyle.alignment = textAligment;
			labelStyle.wordWrap = true;
			labelStyle.clipping = TextClipping.Clip;

			return labelStyle.CalcHeight (new GUIContent (content), RequieredLabelWidth);
		}

		private static void DoDrawLabel (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle, TextAnchor textAligment)
		{
			float height = CalculateLabelHeight (content, fontStyle, textAligment);
			Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (height));

			DoDrawLabel (rect, content, backgroundStyle, color, fontStyle, textAligment);
		}

		private static void DoDrawLabel (Rect rect, string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle, TextAnchor textAligment)
		{
			if (backgroundStyle == null)
				backgroundStyle = new GUIStyle (GUI.skin.GetStyle ("Label"));
			else
				backgroundStyle = new GUIStyle (backgroundStyle);

			backgroundStyle.fixedHeight = rect.height;

			GUI.Box (rect, "", backgroundStyle);

			GUIStyle labelStyle = new GUIStyle (GUI.skin.GetStyle ("Label"));
			labelStyle.normal.textColor = color;
			labelStyle.fontStyle = fontStyle;
			labelStyle.alignment = textAligment;
			labelStyle.wordWrap = true;
			labelStyle.clipping = TextClipping.Clip;
			GUI.Label (rect, content, labelStyle);
		}
		#endregion
	}
}
#endif