namespace UtilityFramework
{
#if UNITY_EDITOR
	using UnityEditor;
	using UnityEngine;

	public class ExtendedGUI
	{
		#region Class accesors
		public static Color DefaultTittleColor
		{
			get { return (EditorGUIUtility.isProSkin) ? new Color (0.705f, 0.705f, 0.705f, 1) : Color.black; }
		}
		#endregion

		#region Class implementation
		public static void BeginInspector ()
		{
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Space (-9);
			EditorGUILayout.BeginVertical ();
		}

		public static void EndInspector ()
		{
			EditorGUILayout.EndVertical ();
			EditorGUILayout.EndHorizontal ();
		}

		#region DrawTitle with automatic layout
		public static void DrawTitle (string content)
		{
			DoDrawTitle (content, null, DefaultTittleColor, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawTitle (string content, Color color)
		{
			DoDrawTitle (content, null, color, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawTitle (string content, Color color, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawTitle (content, null, color, fontStyle, aligment);
		}

		public static void DrawTitle (string content, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawTitle (content, null, DefaultTittleColor, fontStyle, aligment);
		}

		public static void DrawTitle (string content, GUIStyle backgroundStyle)
		{
			DoDrawTitle (content, backgroundStyle, DefaultTittleColor, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawTitle (string content, GUIStyle backgroundStyle, Color color)
		{
			DoDrawTitle (content, backgroundStyle, color, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawTitle (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawTitle (content, backgroundStyle, color, fontStyle, aligment);
		}

		public static void DrawTitle (string content, GUIStyle backgroundStyle, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawTitle (content, backgroundStyle, DefaultTittleColor, fontStyle, aligment);
		}
		#endregion

		#region DrawTitle without automatic layout
		public static void DrawTitle (Rect rect, string content)
		{
			DoDrawTitle (rect, content, null, DefaultTittleColor, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawTitle (Rect rect, string content, Color color)
		{
			DoDrawTitle (rect, content, null, color, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawTitle (Rect rect, string content, Color color, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawTitle (rect, content, null, color, fontStyle, aligment);
		}

		public static void DrawTitle (Rect rect, string content, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawTitle (rect, content, null, DefaultTittleColor, fontStyle, aligment);
		}

		public static void DrawTitle (Rect rect, string content, GUIStyle backgroundStyle)
		{
			DoDrawTitle (rect, content, backgroundStyle, DefaultTittleColor, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawTitle (Rect rect, string content, GUIStyle backgroundStyle, Color color)
		{
			DoDrawTitle (rect, content, backgroundStyle, color, FontStyle.Normal, TextAnchor.MiddleCenter);
		}

		public static void DrawTitle (Rect rect, string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawTitle (rect, content, backgroundStyle, color, fontStyle, aligment);
		}

		public static void DrawTitle (Rect rect, string content, GUIStyle backgroundStyle, FontStyle fontStyle = FontStyle.Normal, TextAnchor aligment = TextAnchor.MiddleCenter)
		{
			DoDrawTitle (rect, content, backgroundStyle, DefaultTittleColor, fontStyle, aligment);
		}
		#endregion

		public static void DoDrawTitle (string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle, TextAnchor textAligment)
		{
			Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (18));
			DoDrawTitle (rect, content, backgroundStyle, color, fontStyle, textAligment);
		}

		public static void DoDrawTitle (Rect rect, string content, GUIStyle backgroundStyle, Color color, FontStyle fontStyle, TextAnchor textAligment)
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
			GUI.Label (rect, content, labelStyle);
		}
		#endregion
	}
#endif 
}