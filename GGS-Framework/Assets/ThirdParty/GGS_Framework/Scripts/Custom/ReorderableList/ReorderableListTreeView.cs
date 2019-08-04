using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework
{
	public partial class ReorderableList
	{
		public class TreeView : UnityEditor.IMGUI.Controls.TreeView
		{
			#region Class members
			private const string GenericDragId = "GenericDragColumnDragging";

			private ReorderableList rl;

			private SearchField searchField;
			#endregion

			#region Class accesors
			#endregion

			#region Class overrides
			protected override TreeViewItem BuildRoot ()
			{
				TreeViewItem root = new TreeViewItem (-1, -1, "Root");

				for (int i = 0; i < rl.list.Count; i++)
				{
					string displayName = GetDisplayNameOfElement (i, rl.searchVariableName);
					root.AddChild (new TreeViewItem (i, -1, displayName));
				}

				return root;
			}

			protected override void RowGUI (RowGUIArgs args)
			{
				Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (args.rowRect,
					new RectLayoutElement ("DragZone", Styles.dragZoneWidth),
					new RectLayoutElement ("Element")
				);

				Styles.dragZone.Draw (rects["DragZone"]);

				if (rl.onDrawElement != null)
					rl.onDrawElement (rects["Element"], args.item.id);
			}

			protected override void ContextClickedItem (int id)
			{
				base.ContextClickedItem (id);
				DoElementOptionsMenu ();
			}

			#region Drag
			protected override bool CanStartDrag (CanStartDragArgs args)
			{
				return true;
			}

			protected override void SetupDragAndDrop (SetupDragAndDropArgs args)
			{
				DragAndDrop.PrepareStartDrag ();
				List<int> draggedItems = args.draggedItemIDs as List<int>;

				DragAndDrop.SetGenericData (GenericDragId, draggedItems);
				DragAndDrop.objectReferences = new UnityEngine.Object[] { };

				DragAndDrop.StartDrag ("Drag");
			}

			protected override DragAndDropVisualMode HandleDragAndDrop (DragAndDropArgs args)
			{
				List<int> draggedItems = DragAndDrop.GetGenericData (GenericDragId) as List<int>;
				if (draggedItems == null)
					return DragAndDropVisualMode.None;

				switch (args.dragAndDropPosition)
				{
					case DragAndDropPosition.BetweenItems:
						{
							if (args.performDrop)
								MoveSelection (args.insertAtIndex, draggedItems);

							return DragAndDropVisualMode.Move;
						}
					default:
						return DragAndDropVisualMode.None;
				}
			}
			#endregion
			#endregion

			#region Class implementation
			public TreeView (ReorderableList reorderableList) : base (new TreeViewState ())
			{
				rl = reorderableList;

				searchField = new SearchField ();
				searchField.downOrUpArrowKeyPressed += SetFocusAndEnsureSelectedItem;

				Reload ();
			}

			public void Draw (Rect rect)
			{
				Dictionary<string, Rect> rects = ExtendedRect.VerticalRects (rect,
					new RectLayoutElement ("Header", Styles.headerHeight),
					new RectLayoutElement (Styles.defaultSpacing),
					new RectLayoutElement ("List")
				);

				DrawHeader (rects["Header"].HorizontalExpand (Styles.defaultPadding));
				OnGUI (rects["List"]);
			}

			private void DrawHeader (Rect rect)
			{
				Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (rect,
					new RectLayoutElement ("Title"),
					new RectLayoutElement ("AddButton", Styles.addButtonWidth)
				);

				Styles.headerBackground.Draw (rect);

				DrawTitle (rects["Title"]);
				DrawAddButton (rects["AddButton"]);
			}

			private void DrawTitle (Rect rect)
			{
				Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (rect,
					new RectLayoutElement (Styles.addButtonWidth),
					new RectLayoutElement ("Title")
				);

				DrawSerchBar (rect);

				if (!searchField.HasFocus () && string.IsNullOrEmpty (searchString))
					GUI.Label (rects["Title"], rl.title, Styles.header);
			}

			private void DrawSerchBar (Rect rect)
			{
				searchString = searchField.OnGUI (rect, searchString, Styles.searchField, GUIStyle.none, GUIStyle.none);
			}

			private void DrawAddButton (Rect rect)
			{
				if (GUI.Button (rect, string.Empty, Styles.addButton))
				{
				}
			}

			private void DoElementOptionsMenu ()
			{
				Repaint ();

				AdvancedGenericMenu.Item[] items =
				{
					new AdvancedGenericMenu.Item ("Duplicate", false),
					new AdvancedGenericMenu.Item ("Delete", false),
				};

				AdvancedGenericMenu.Draw<ElementOptions> (items, item =>
				{
					ElementOptions option = (ElementOptions) item;

					switch (option)
					{
						case ElementOptions.Duplicate:
							break;
						case ElementOptions.Delete:
							DeleteSelection ();
							break;
						case ElementOptions.Copy:
							break;
					}
				});
			}

			private void MoveSelection (int insertIndex, List<int> selectedIds)
			{
				if (insertIndex < 0)
					return;

				List<object> selection = new List<object> ();

				for (int i = 0; i < selectedIds.Count; i++)
					selection.Add (rl.list[selectedIds[i]]);

				foreach (object item in selection)
					rl.list.Remove (item);

				int itemsAboveInsertIndex = 0;
				foreach (int selectedElement in selectedIds)
				{
					if (selectedElement < insertIndex)
						itemsAboveInsertIndex++;
				}

				insertIndex -= itemsAboveInsertIndex;

				selection.Reverse ();
				foreach (object item in selection)
					rl.list.Insert (insertIndex, item);

				List<int> newSelection = new List<int> ();
				for (int i = insertIndex; i < insertIndex + selection.Count; i++)
					newSelection.Add (i);

				SetSelection (newSelection, TreeViewSelectionOptions.RevealAndFrame);
			}

			private string GetDisplayNameOfElement (int elementIndex, string variableName)
			{
				System.Type elementType = rl.elementType;

				if (string.IsNullOrEmpty (variableName))
					return rl.list[elementIndex] as string;

				if (elementType.GetField (variableName) != null)
					return elementType.GetField (variableName).GetValue (rl.list[elementIndex]) as string;

				if (elementType.GetProperty (variableName) != null)
					return elementType.GetProperty (variableName).GetValue (rl.list[elementIndex], null) as string;

				return "Unnamed";
			}

			private void AddElement ()
			{

			}

			private void DeleteSelection ()
			{
				List<int> selection = state.selectedIDs;

				if (selection.Count > 1)
					selection.Sort ((a, b) => -1 * a.CompareTo (b));

				foreach (int id in selection)
					rl.list.RemoveAt (id);

				Reload ();
				//SetSelection (null);
			}
			#endregion
		}
	}
}