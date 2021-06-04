using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework.Editor
{
	public class EasingsEditorWindow : EditorWindow
	{
		#region Class Members
		private const string PrefsKey = "EasingsEditorWindow_";

		private const float EasingResolution = 100;
		private static readonly Vector2 EasingSize = new Vector2 (175, 175);
		private static readonly Vector2 EasingSpacing = new Vector2 (30, 10);

		private static SearchField searchField;
		#endregion

		#region Class Accesors
		private Rect WindowRect
		{
			get { return new Rect (Vector2.zero, position.size); }
		}

		private float ScrollPosition
		{
			get { return EditorPrefs.GetFloat (string.Concat (PrefsKey, "ScrollPosition")); }
			set { EditorPrefs.SetFloat (string.Concat (PrefsKey, "ScrollPosition"), value); }
		}

		// private int FilteredEasingsModes
		// {
		// 	get { return EditorPrefs.GetInt (string.Concat (PrefsKey, "FilteredEasingsModes")); }
		// 	set { EditorPrefs.SetInt (string.Concat (PrefsKey, "FilteredEasingsModes"), value); }
		// }

		private int FilteredEasings
		{
			get { return EditorPrefs.GetInt (string.Concat (PrefsKey, "FilteredEasings")); }
			set { EditorPrefs.SetInt (string.Concat (PrefsKey, "FilteredEasings"), value); }
		}

		// private string FilteredEasingsSearch
		// {
		// 	get { return EditorPrefs.GetString (string.Concat (PrefsKey, "FilteredEasingsSearch")); }
		// 	set { EditorPrefs.SetString (string.Concat (PrefsKey, "FilteredEasingsSearch"), value); }
		// }

		private float PreviewTime
		{
			get { return EditorPrefs.GetFloat (string.Concat (PrefsKey, "PreviewTime"), 1); }
			set { EditorPrefs.SetFloat (string.Concat (PrefsKey, "PreviewTime"), value); }
		}
		#endregion

		#region EditorWindow Callbacks
		private void OnGUI ()
		{
			Draw ();
		}
		#endregion

		#region Class Implementation
		[MenuItem ("Window/GGS Framework/Easings Viewer")]
		private static void Open ()
		{
			EasingsEditorWindow window = (EasingsEditorWindow) GetWindow (typeof (EasingsEditorWindow));
			window.titleContent = new GUIContent ("Easings Viewer");
			window.Show ();
		}

		private void Draw ()
		{
			Dictionary<string, Rect> rects = AdvancedRect.GetRects (WindowRect, AdvancedRect.Orientation.Vertical,
				new AdvancedRect.FixedItem ("Header", 24),
				new AdvancedRect.FixedSpace (2),
				new AdvancedRect.FixedGroup ("Settings", AdvancedRect.Orientation.Horizontal, 18, new RectPadding (-2, RectPaddingType.All),
					new AdvancedRect.FixedItem ("PreviewTime", 150),
					new AdvancedRect.FixedSpace (6),
					new AdvancedRect.FixedItem ("FilteredEasingsModes", 200),
					new AdvancedRect.FixedSpace (3),
					new AdvancedRect.ExpandedItem ("FilteredEasingsType")
				),
				new AdvancedRect.FixedSpace (5),
				new AdvancedRect.ExpandedItem ("Easings")
			);

			GUI.Label (rects["Header"], "Easings Viewer", Styles.Header);

			Styles.Background.Draw (rects["Settings"]);

			EditorGUIUtility.labelWidth = 80;
			PreviewTime = EditorGUI.FloatField (rects["PreviewTime"], "Preview Time", PreviewTime);

			EditorGUIUtility.labelWidth = 90;
			EditorGUI.BeginChangeCheck ();
			EditorGUI.BeginChangeCheck ();
			int filteredMode = (int) (Easings.Mode) EditorGUI.EnumFlagsField (rects["FilteredEasingsModes"], "Easings Modes", (Easings.Mode) FilteredEasings);

			if (filteredMode == -1)
				filteredMode = (int) Easings.Mode.Everything;

			if (EditorGUI.EndChangeCheck ())
			{
			}

			EditorGUI.BeginChangeCheck ();
			int filteredTypes = (int) (Easings.Type) EditorGUI.EnumFlagsField (rects["FilteredEasingsType"], "Easings Types", (Easings.Type) FilteredEasings);

			if (filteredTypes == -1)
				filteredTypes = (int) Easings.Type.Everything;

			if (EditorGUI.EndChangeCheck ())
			{
			}

			if (EditorGUI.EndChangeCheck ())
			{
			}

			if (searchField == null)
				searchField = new SearchField ();

			DrawEasings (rects["Easings"]);
		}

		private void UpdateFilteredEasings ()
		{
			Array allValues = Enum.GetValues (typeof (Easings.All));

			for (int i = 0; i < allValues.Length; i++)
			{
				int current = (int) allValues.GetValue (i);

				if (Enum.IsDefined (typeof (Easings.All), (FilteredEasings & current)))
					Debug.Log ((Easings.All) current);
			}
		}

		private void DrawEasings (Rect rect)
		{
			Styles.Background.Draw (rect);
			Rect paddedRect = new RectOffset (Styles.Padding, Styles.Padding, Styles.Padding, Styles.Padding).Add (rect);

			int easeCount = Easings.Functions.Count;

			List<Rect> easingsRects = ComputeEasingsRects (paddedRect.size, easeCount, out Vector2 totalSize);

			float viewHeight = paddedRect.height + Styles.Padding;
			float viewOffset = totalSize.y - viewHeight;
			float handleSize = (viewHeight / totalSize.y) * totalSize.y;

			bool needsScrollBar = viewOffset > 0;

			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
				new AdvancedRect.ExpandedItem ("Easings", new RectPadding (Styles.Padding, RectPaddingType.Vertical)),
				new AdvancedRect.FixedItem ("Scrollbar", 16, needsScrollBar)
			);

			Rect easingsRect = rects["Easings"];

			if (needsScrollBar)
			{
				ScrollPosition = GUI.VerticalScrollbar (rects["Scrollbar"], ScrollPosition, handleSize, 0, viewOffset + handleSize);

				if (easingsRect.Contains (Event.current.mousePosition) && Event.current.type == EventType.ScrollWheel)
				{
					ScrollPosition += Event.current.delta.y * 20f;
					Event.current.Use ();
				}
			}
			else
				ScrollPosition = 0;

			Vector2 easingsOffset = new Vector2 ((easingsRect.width - totalSize.x) / 2f, 0);
			easingsOffset.y -= ScrollPosition;

			GUI.BeginGroup (easingsRect);
			for (int i = 0; i < easingsRects.Count; i++)
			{
				if (!easingsRect.Overlaps (rect))
					continue;

				Rect easeRect = new Rect (easingsRects[i].position + easingsOffset, easingsRects[i].size);
				DrawEase (easeRect, 0);
			}

			GUI.EndGroup ();
		}

		private List<Rect> ComputeEasingsRects (Vector2 containerSize, int easingCount, out Vector2 totalSize)
		{
			int columnCount = GetColumnCount (easingCount, containerSize.x);
			int rowCount = Mathf.CeilToInt ((float) easingCount / (float) columnCount);

			if (rowCount * EasingSize.y + (rowCount - 1) * EasingSpacing.y > containerSize.y)
			{
				containerSize.x -= 16;
				columnCount = GetColumnCount (easingCount, containerSize.x);
			}

			List<Rect> rects = new List<Rect> ();

			for (int y = 0; y < rowCount; y++)
			{
				for (int x = 0; x < columnCount; x++)
				{
					if (rects.Count >= easingCount)
						continue;

					Vector2 easingPosition = new Vector2 (EasingSize.x * x + EasingSpacing.x * x, EasingSize.y * y + EasingSpacing.y * y);
					rects.Add (new Rect (easingPosition, EasingSize));
				}
			}

			totalSize = new Vector2 (columnCount * EasingSize.x + (columnCount - 1) * EasingSpacing.x, rowCount * EasingSize.y + (rowCount - 1) * EasingSpacing.y);
			return rects;
		}

		private int GetColumnCount (int easingCount, float containerWidth)
		{
			int columnCount = 0;
			float computedWidth = 0;
			while (true)
			{
				computedWidth += EasingSize.x + ((columnCount == 0) ? 0 : EasingSpacing.x);

				if (computedWidth <= containerWidth)
					columnCount++;
				else
					break;
			}

			return columnCount;
		}

		private void DrawEase (Rect rect, int easeType)
		{
			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Vertical,
				new AdvancedRect.FixedItem ("Header", 20),
				new AdvancedRect.FixedSpace (1),
				new AdvancedRect.ExpandedItem ("Ease")
			);

			Rect originalEaseRect = rects["Ease"];
			Rect easeRect = originalEaseRect;

			// GUI.Label (rects["Header"], ease.ToString ().ToTitleCase (), EasingCurvesStyles.Header);
			Styles.Ease.Draw (easeRect);

			GUI.BeginGroup (easeRect);
			easeRect.position = Vector2.zero;
			easeRect = new RectOffset (-2, -2, 0, 0).Add (easeRect);

			float verticalOffset = easeRect.height / 4f;
			Vector2 offset = new Vector2 (easeRect.xMin, easeRect.height - verticalOffset);

			Vector2 bottomLeft = offset;
			Vector2 bottomRight = offset + Vector2.right * easeRect.width;

			Vector2 topLeft = new Vector2 (easeRect.xMin, verticalOffset);
			Vector2 topRight = new Vector2 (easeRect.xMax, verticalOffset);

			Handles.color = Color.red;
			Handles.DrawLine (bottomLeft, bottomRight);
			Handles.DrawLine (topLeft, topRight);
			Handles.color = Color.white;

			GUI.EndGroup ();
		}
		#endregion

		#region Nested Classes
		private static class Styles
		{
			public static readonly GUISkin DarkSkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("EasingCurves/EasingCurves_DarkGUISkin.guiskin") as GUISkin;
			public static readonly GUISkin Lightkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("EasingCurves/EasingCurves_LightGUISkin.guiskin") as GUISkin;

			public static readonly GUISkin Skin = (EditorGUIUtility.isProSkin) ? DarkSkin : Lightkin;

			public static readonly int Padding = -4;

			public static readonly GUIStyle Background = Skin.GetStyle ("Background");
			public static readonly GUIStyle Ease = Skin.GetStyle ("Ease");
			public static readonly GUIStyle Header = Skin.GetStyle ("Header");
			public static readonly GUIStyle Circle = Skin.GetStyle ("Circle");
			public static readonly GUIStyle Arrow = Skin.GetStyle ("Arrow");
		}
		#endregion
	}
}