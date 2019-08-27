#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Development.Test
{
	[Flags]
	public enum ReorderableListOptions
	{
		Default = 0
	}

	public class ReorderableListConfig
	{
		#region Class members
		public float headerHeight;
		public FontStyle headerTittleFontStyle;
		public string headerTittle;

		public float elementHeight;
		public float elementSpacing;

		public float footerHeight;
		#endregion

		#region Class implementation
		public ReorderableListConfig ()
		{
			headerHeight = 18;
			headerTittleFontStyle = FontStyle.Normal;
			headerTittle = "List";

			elementHeight = 16;
			elementSpacing = 0;

			footerHeight = 16;
		}
		#endregion
	}

	public class ReorderableListDefaults
	{
		#region Class members
		public const int DragHandleWidth = 20;
		public const int Padding = 6;

		public readonly GUIStyle headerBackgroundStyle;

		public readonly GUIStyle listBackgroundStyle = EditorStyles.helpBox;
		//public readonly GUIStyle elementBackgroundStyle = new GUIStyle ((GUIStyle) "RL Element");
		public readonly GUIStyle elementDragHandleStyle;
		public readonly GUIStyle elementBackgroundStyle = new GUIStyle ((GUIStyle) "PR Label");
		//public readonly GUIStyle dragIndicatorMarketStyle = new GUIStyle ((GUIStyle) "PR Insertion");
		public readonly GUIStyle dragIndicatorMarketStyle = new GUIStyle ((GUIStyle) "PR Insertion Above");

		public readonly GUIStyle footerBackgroundStyle;

		public GUIContent iconToolbarPlus;
		public GUIContent iconToolbarPlusMore;
		public GUIContent iconToolbarMinus;
		#endregion

		#region Class implementation
		public ReorderableListDefaults ()
		{
			//headerBackgroundStyle = EditorStyles.helpBox;

			//draggingHandleStyle = EditorStyles.helpBox;
			//listBackgroundStyle = GUI.skin.GetStyle ("PR Label
			//elementBackgroundStyle = ;

			//footerBackgroundStyle = EditorStyles.helpBox;
		}
		#endregion
	}

	public class ReorderableList
	{
		#region Class members
		private IList list;
		private Type elementType;

		private int controlId = -1;
		private bool dragging;
		private int insertIndex;

		private List<int> selectedElements = new List<int> ();
		#endregion

		#region Class accesors
		public int Count { get { return list.Count; } }

		public bool HasSelection { get { return selectedElements.Count > 0; } }

		public ReorderableListDefaults Defaults { get; private set; }

		public ReorderableListConfig Config { get; set; }
		#endregion

		#region Class overrides
		#endregion

		#region Class implementation
		public ReorderableList (IList list, Type elementType, ReorderableListOptions options)
		{
			this.list = list;
			this.elementType = elementType;

			controlId = GUIUtility.GetControlID (FocusType.Keyboard);

			Config = new ReorderableListConfig ();
		}

		private void InitDefaults ()
		{
			if (Defaults != null)
				return;

			Defaults = new ReorderableListDefaults ();
		}

		public void LayoutDraw ()
		{
		}

		public void Draw (Rect rect)
		{
			InitDefaults ();

			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Vertical,
				new AdvancedRect.FixedItem ("Header", Config.headerHeight),
				new AdvancedRect.FixedItem ("Elements", Config.elementHeight * Count),
				new AdvancedRect.FixedItem ("Footer", Config.footerHeight)
			);

			DrawHeader (rects["Header"]);
			DrawElements (rects["Elements"]);
			DrawFooter (rects["Footer"]);
		}

		private void DrawHeader (Rect rect)
		{
			AdvancedGUILabel.Draw (rect, new AdvancedGUILabelConfig (Config.headerTittle, Defaults.headerBackgroundStyle, Config.headerTittleFontStyle));
		}

		private void DrawElements (Rect rect)
		{
			Rect[] rects = CalculateElementRects (rect);

			Event currentEvent = Event.current;

			switch (currentEvent.GetTypeForControl (controlId))
			{
				case EventType.MouseDown:
					if (rect.Contains (currentEvent.mousePosition) && Event.current.button == 0)
					{
						for (int i = 0; i < Count; i++)
						{
							if (rects[i].Contains (currentEvent.mousePosition) && !ElementSelected (i))
							{
								if (currentEvent.shift)
								{
									if (selectedElements.Count >= 1)
									{
										selectedElements = GetIndexesForRange (selectedElements[0], i);
										break;
									}
								}

								if (!currentEvent.control)
									selectedElements.Clear ();

								selectedElements.Add (i);
							}
						}

						GUIUtility.hotControl = controlId;
						GrabKeyboardFocus ();
						currentEvent.Use ();
					}
					break;

				case EventType.MouseUp:
					if (GUIUtility.hotControl == controlId)
					{
						if (HasSelection)
						{
							if (dragging)
							{
								MoveSelection (insertIndex);
								dragging = false;
							}
							else
							{
								if (!currentEvent.control && !currentEvent.shift)
								{
									for (int i = 0; i < Count; i++)
									{
										if (rects[i].Contains (currentEvent.mousePosition))
										{
											selectedElements.Clear ();
											selectedElements.Add (i);
										}
									}
								}
							}

							currentEvent.Use ();
						}

						GUIUtility.hotControl = 0;
					}
					break;

				case EventType.MouseDrag:
					if (HasSelection && GUIUtility.hotControl == controlId)
					{
						float mouseY = currentEvent.mousePosition.y;
						for (int i = 0; i < Count; i++)
						{
							Rect currentRect = rects[i];

							float middleY = currentRect.y + currentRect.height / 2f;

							if (mouseY > currentRect.yMin && mouseY < middleY)
								insertIndex = i;
							else if (mouseY > middleY && mouseY < currentRect.yMax)
								insertIndex = i + 1;
							else
							{
								if (i == 0 && mouseY < middleY)
									insertIndex = 0;
								else if (i == Count - 1 && mouseY > middleY)
									insertIndex = Count;
							}
						}

						dragging = true;
						currentEvent.Use ();
					}
					break;

				case EventType.Repaint:
					for (int i = 0; i < Count; i++)
						DrawElement (rects[i], i, selectedElements.Contains (i));

					if (dragging)
					{
						Rect elementRect = rects[(insertIndex < Count) ? insertIndex : insertIndex - 1];
						bool mouseAbove = (currentEvent.mousePosition.y < elementRect.y + elementRect.height / 2);
						Rect dragMarketRect = new Rect (rect.x + 4, (mouseAbove) ? elementRect.yMin : elementRect.yMax, rect.width, 2);
						Defaults.dragIndicatorMarketStyle.Draw (dragMarketRect, false, false, true, false);
					}
					break;
			}
		}

		private bool ElementSelected (int i)
		{
			return selectedElements.Contains (i);
		}

		private List<int> GetIndexesForRange (int a, int b)
		{
			List<int> indexes = new List<int> ();

			if (b > a)
			{
				for (int i = a; i <= b; i++)
					indexes.Add (i);
			}
			else
			{
				for (int i = a; i >= b; i--)
					indexes.Add (i);
			}

			return indexes;
		}

		private void MoveSelection (int insertIndex)
		{
			List<object> selection = new List<object> ();

			for (int i = 0; i < selectedElements.Count; i++)
				selection.Add (list[selectedElements[i]]);

			foreach (object item in selection)
				list.Remove (item);

			int itemsAboveInsertIndex = 0;
			foreach (int selectedElement in selectedElements)
			{
				if (selectedElement < insertIndex)
					itemsAboveInsertIndex++;
			}

			insertIndex -= itemsAboveInsertIndex;

			selection.Reverse ();
			foreach (object item in selection)
				list.Insert (insertIndex, item);
		}

		public void GrabKeyboardFocus ()
		{
			GUIUtility.keyboardControl = controlId;
		}

		public void ReleaseKeyboardFocus ()
		{
			if (GUIUtility.keyboardControl != controlId)
				return;

			GUIUtility.keyboardControl = 0;
		}

		public bool HasKeyboardControl ()
		{
			return GUIUtility.keyboardControl == controlId;
		}

		private Rect[] CalculateElementRects (Rect rect)
		{
			Rect[] rects = new Rect[Count];

			for (int i = 0; i < Count; i++)
			{
				float yPosition = rect.y + Config.elementHeight * i + Config.elementSpacing * i;
				rects[i] = new Rect (rect.x, yPosition, rect.width, Config.elementHeight);
			}

			return rects;
		}

		private void DrawElement (Rect rect, int index, bool selected)
		{
			Defaults.elementBackgroundStyle.Draw (rect, selected, false, true, false);
			GUI.Label (rect, (string) list[index]);
			//EditorGUI.DrawRect (rect.Expand (1), (selected) ? Color.white : Color.gray);
		}

		private void DrawFooter (Rect rect)
		{
		}
		#endregion
	}
}
#endif