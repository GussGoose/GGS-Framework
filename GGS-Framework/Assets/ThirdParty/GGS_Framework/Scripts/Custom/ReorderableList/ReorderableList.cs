#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework
{
	public abstract partial class ReorderableList<ElementType>
	{
		#region Class Members
		protected List<ElementType> list;

		private TreeView treeView;
		private TreeViewState treeViewState;
		private SearchField searchBar;

		#region Events
		public delegate void SelectionChangedDelegate (List<int> selection);
		public SelectionChangedDelegate onSelectionChanged;

		public delegate void RightClickElementDelegate (int index);
		public RightClickElementDelegate onRightClickElement;
		#endregion
		#endregion

		#region Class Accesors
		public int Count
		{
			get { return list.Count; }
		}

		public string Title
		{
			get;
			private set;
		}

		public bool HasSearch
		{
			get { return treeView.hasSearch; }
		}

		public string SearchString
		{
			get { return treeView.searchString; }
			private set { treeView.searchString = value; }
		}

		public List<int> Selection
		{
			get { return treeViewState.selectedIDs; }
		}

		public int FirstOfSelection
		{
			get
			{
				if (Selection.Count == 0)
					return -1;

				return Selection[0];
			}
		}

		public int LatestOfSelection
		{
			get
			{
				if (Selection.Count == 0)
					return -1;

				return Selection[Selection.Count - 1];
			}
		}

		public int CopiedElementHashCode
		{
			get;
			private set;
		}
		#endregion

		#region Class Implementation
		protected ReorderableList (List<ElementType> list, string title = "Reorderable List")
		{
			this.list = list;

			treeViewState = new TreeViewState ();
			treeView = new TreeView (this, treeViewState);

			Title = title;

			searchBar = new SearchField ();
			searchBar.downOrUpArrowKeyPressed += treeView.SetFocusAndEnsureSelectedItem;
		}

		#region Draw
		public virtual void Draw (Rect rect)
		{
			if (treeView.ItemCount != Count)
			{
				ReloadTree ();
				return;
			}

			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Vertical,
				new AdvancedRect.FixedSpace (Styles.DefaultSpacing),
				new AdvancedRect.FixedItem ("Header", Styles.HeaderHeight, new AdvancedRect.Padding (Styles.DefaultPadding, AdvancedRect.Padding.Type.Horizontal)),
				new AdvancedRect.FixedSpace (Styles.DefaultSpacing),
				new AdvancedRect.ExpandedItem ("TreeView")
			);

			DrawHeader (rects["Header"]);

			if (CanDrawBackground ())
			{
				Styles.Background.Draw (rects["TreeView"]);
				rects["TreeView"] = AdvancedRect.ExpandRect (rects["TreeView"], new AdvancedRect.Padding (Styles.DefaultPadding, AdvancedRect.Padding.Type.All));
			}

			if (Count > 0)
				treeView.OnGUI (rects["TreeView"]);
			else
				AdvancedLabel.Draw (rects["TreeView"], new AdvancedLabel.Config ("Nothing in list.", FontStyle.Bold));
		}

		protected virtual void DrawHeader (Rect rect)
		{
			bool canSearch = CanSearch ();
			bool canAddElement = CanAddElement ();

			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
				new AdvancedRect.ExpandedGroup ("SearchBar", AdvancedRect.Orientation.Horizontal,
					new AdvancedRect.FixedSpace (12, canSearch),
					new AdvancedRect.ExpandedItem ("Title")
				),
				new AdvancedRect.FixedItem ("AddButton", Styles.AddButtonWidth, canAddElement)
			);

			Styles.HeaderBackground.Draw (rect);

			if (canSearch)
			{
				if (Count > 0)
					DrawSearchBar (rects["SearchBar"]);
			}

			if (!canSearch || (!searchBar.HasFocus () && string.IsNullOrEmpty (SearchString)))
				GUI.Label (rects["Title"], Title, Styles.Header);

			if (canAddElement)
			{
				if (GUI.Button (rects["AddButton"], string.Empty, Styles.AddButton))
					AddElement ();
			}
		}

		private void DrawSearchBar (Rect rect)
		{
			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
				new AdvancedRect.ExpandedItem ("Bar"),
				new AdvancedRect.FixedItem ("CancelButton", Styles.HeaderHeight, new AdvancedRect.Padding (Styles.SearchBarCancelButtonPadding, AdvancedRect.Padding.Type.All))
			);

			SearchString = searchBar.OnGUI (rects["Bar"], SearchString, Styles.SearchBar, GUIStyle.none, GUIStyle.none);

			if (!string.IsNullOrEmpty (SearchString))
			{
				if (GUI.Button (rects["CancelButton"], string.Empty, Styles.SearchBarCancelButton))
				{
					SearchString = string.Empty;
					GUI.FocusControl (null);
				}
			}
		}

		private void DrawElementBase (Rect rect, int index)
		{
			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
				new AdvancedRect.FixedItem ("ReorderIcon", Styles.DragIconWidth, CanReorder ()),
				new AdvancedRect.ExpandedItem ("Element")
			);

			if (CanReorder ())
				Styles.DragIcon.Draw (rects["ReorderIcon"]);

			DrawElement (rects["Element"], index);
		}

		protected abstract void DrawElement (Rect rect, int index);
		#endregion

		#region General
		protected virtual void SelectionChanged (List<int> selection)
		{
			onSelectionChanged?.Invoke (selection);
		}

		protected virtual void RightClickElement (int index)
		{
			RepaintTree ();

			bool canRemove = CanRemoveSelection ();
			bool canInsertElementAbove = CanInsertElementAbove ();
			bool canInsertElementBelow = CanInsertElementBelow ();
			bool canCopyElement = CanCopyElement ();
			bool canPasteElement = CanPasteElement ();

			AdvancedGenericDropdown.Option[] dropdownOptions =
			{
				new AdvancedGenericDropdown.Item ("Remove", false, canRemove),
				new AdvancedGenericDropdown.Separator (canCopyElement || canPasteElement),
				new AdvancedGenericDropdown.Item ("Copy", false, canCopyElement),
				new AdvancedGenericDropdown.Item ("Paste", false, canPasteElement),
				new AdvancedGenericDropdown.Separator (canInsertElementAbove || canInsertElementBelow),
				new AdvancedGenericDropdown.Item ("Insert Above", false, canInsertElementAbove),
				new AdvancedGenericDropdown.Item ("Insert Below", false, canInsertElementBelow)
			};

			AdvancedGenericDropdown.Show<ElementOptions> (dropdownOptions, option =>
				{
					ElementOptions elementOption = (ElementOptions) option;

					switch (elementOption)
					{
						case ElementOptions.Remove:
							RemoveElementSelection ();
							break;
						case ElementOptions.Copy:
							CopyElement ();
							break;
						case ElementOptions.Paste:
							PasteElement ();
							break;
						case ElementOptions.InsertAbove:
							InsertElementAbove ();
							break;
						case ElementOptions.InsertBelow:
							InsertElementBelow ();
							break;
					}
				}
			);
			
			onRightClickElement?.Invoke (index);
		}

		private void PerformDrop (int insertIndex, List<int> draggedElements)
		{
			MoveElementSelection (insertIndex, draggedElements);
		}

		protected abstract string GetNameOfElementForSearch (int index);

		protected string FindNameOfElementForSearch (int index, string variableName)
		{
			Type elementType = typeof (ElementType);
			string name = string.Empty;

			if (string.IsNullOrEmpty (variableName))
				name = list[index] as string;
			else if (elementType.GetField (variableName) != null)
				name = elementType.GetField (variableName).GetValue (list[index]) as string;
			else if (elementType.GetProperty (variableName) != null)
				name = elementType.GetProperty (variableName).GetValue (list[index], null) as string;

			return name;
		}
		#endregion

		#region Configuration
		protected virtual bool CanDrawBackground ()
		{
			return true;
		}

		protected virtual bool CanSearch ()
		{
			return true;
		}

		protected virtual bool CanReorder ()
		{
			return true;
		}

		protected virtual bool CanAdd ()
		{
			return true;
		}

		protected virtual bool CanInsert ()
		{
			return true;
		}

		protected virtual bool CanRemove ()
		{
			return true;
		}

		protected virtual bool CanCopyAndPaste ()
		{
			return true;
		}
		#endregion

		#region Element Management
		#region Add
		protected bool CanAddElement ()
		{
			return CanAdd () & !HasSearch;
		}

		protected virtual void AddElement ()
		{
			AddElementAtIndex (Count);
		}
		#endregion

		#region Insert
		protected bool CanInsertElement ()
		{
			return CanInsert () && CanAddElement ();
		}

		#region Above
		protected bool CanInsertElementAbove ()
		{
			if (!CanInsertElement ())
				return false;

			return (Selection.Count == 1);
		}

		protected virtual void InsertElementAbove ()
		{
			AddElementAtIndex (LatestOfSelection);
		}
		#endregion

		#region Below
		protected bool CanInsertElementBelow ()
		{
			if (!CanInsertElement ())
				return false;

			return (Selection.Count == 1);
		}

		protected virtual void InsertElementBelow ()
		{
			AddElementAtIndex (LatestOfSelection + 1);
		}
		#endregion
		#endregion

		#region Remove
		protected bool CanRemoveSelection ()
		{
			return CanRemove ();
		}

		protected virtual void RemoveElementSelection ()
		{
			DoRemoveElementSelection ();
		}

		protected void DoRemoveElementSelection ()
		{
			List<int> selection = Selection;

			// Sort elements by descending 
			if (selection.Count > 1)
				selection.Sort ((a, b) => -1 * a.CompareTo (b));

			foreach (int id in selection)
				list.RemoveAt (id);

			ReloadTree ();
			SetSelection (null);
		}
		#endregion

		#region Copy
		protected bool CanCopyElement ()
		{
			if (!CanCopyAndPaste ())
				return false;

			return (Selection.Count == 1);
		}

		protected virtual void CopyElement ()
		{
			CopiedElementHashCode = list[FirstOfSelection].GetHashCode ();
		}

		protected int GetCopiedElementIndex ()
		{
			int index = -1;

			if (CopiedElementHashCode == 0)
				return index;

			// Find for element with copied hash code
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GetHashCode () == CopiedElementHashCode)
					return i;
			}

			return index;
		}
		#endregion

		#region Paste
		protected bool CanPasteElement ()
		{
			if (!CanCopyAndPaste () || GetCopiedElementIndex () == -1)
				return false;

			return true;
		}

		protected virtual void PasteElement ()
		{
			int copiedElementIndex = GetCopiedElementIndex ();
		}
		#endregion

		#region General
		protected virtual void MoveElementSelection (int insertIndex, List<int> selectedIds)
		{
			DoMoveElementSelection (insertIndex, selectedIds);
		}

		protected void DoMoveElementSelection (int insertIndex, List<int> selectedIds)
		{
			if (insertIndex < 0)
				return;

			List<object> selection = new List<object> ();

			for (int i = 0; i < selectedIds.Count; i++)
				selection.Add (list[selectedIds[i]]);

			foreach (ElementType item in selection)
				list.Remove (item);

			int itemsAboveInsertIndex = 0;
			foreach (int selectedElement in selectedIds)
			{
				if (selectedElement < insertIndex)
					itemsAboveInsertIndex++;
			}

			insertIndex -= itemsAboveInsertIndex;

			selection.Reverse ();
			foreach (ElementType item in selection)
				list.Insert (insertIndex, item);

			List<int> newSelection = new List<int> ();
			for (int i = insertIndex; i < insertIndex + selection.Count; i++)
				newSelection.Add (i);

			SetSelection (newSelection, TreeViewSelectionOptions.RevealAndFrame);

			ReloadTree ();
		}

		protected virtual void AddElementAtIndex (int insertIndex)
		{
			DoAddElementAtIndex (insertIndex, CreateElementObject ());
		}

		protected void DoAddElementAtIndex (int insertIndex, ElementType value)
		{
			list.Insert (insertIndex, value);

			ReloadTree ();
			SetSelection (new List<int> { insertIndex });
		}

		protected abstract ElementType CreateElementObject ();
		#endregion
		#endregion

		#region TreeView Shortcuts
		protected void ReloadTree ()
		{
			treeView.Reload ();
		}

		protected void RepaintTree ()
		{
			treeView.Repaint ();
		}

		protected void SetSelection (IList<int> ids)
		{
			if (ids == null)
				ids = new List<int> ();

			treeView.SetSelection (ids);
		}

		protected void SetSelection (IList<int> ids, TreeViewSelectionOptions options)
		{
			treeView.SetSelection (ids, options);
		}
		#endregion
		#endregion
	}
}
#endif