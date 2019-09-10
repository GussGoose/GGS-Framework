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

		public bool drawBackground;

		public string title;
		public bool canSearch;
		private SearchField searchBar;

		public bool draggable;

		public bool canAddAndRemove;
		public bool canCopyAndPaste;
		private int copiedElementHashCode;
		#endregion

		#region Class Accesors
		public int Count
		{
			get { return list.Count; }
		}

		public bool HasSearch
		{
			get { return treeView.hasSearch; }
		}

		protected string SearchString
		{
			get { return treeView.searchString; }
			set { treeView.searchString = value; }
		}

		public List<int> Selection
		{
			get { return treeViewState.selectedIDs; }
		}

		protected int FirstIdOfSelection
		{
			get { return Selection[0]; }
		}

		protected int LatestIdOfSelection
		{
			get { return Selection[Selection.Count - 1]; }
		}
		#endregion

		#region Class Implementation
		protected ReorderableList (List<ElementType> list, string title = "Reorderable List")
		{
			this.list = list;

			treeViewState = new TreeViewState ();
			treeView = new TreeView (this, treeViewState);

			drawBackground = true;

			this.title = title;

			canSearch = true;
			searchBar = new SearchField ();
			searchBar.downOrUpArrowKeyPressed += treeView.SetFocusAndEnsureSelectedItem;

			draggable = true;
			canAddAndRemove = true;
			canCopyAndPaste = true;
		}

		#region Draw
		public void Draw (Rect rect)
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

			if (drawBackground)
				Styles.Background.Draw (rects["TreeView"]);

			if (Count > 0)
				treeView.OnGUI (rects["TreeView"]);
			else
				AdvancedLabel.Draw (rects["TreeView"], new AdvancedLabel.Config ("Nothing in list.", FontStyle.Bold));
		}

		private void DrawHeader (Rect rect)
		{
			bool showAddButton = CanAddElement ();

			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
				new AdvancedRect.ExpandedGroup ("SearchBar", AdvancedRect.Orientation.Horizontal,
					new AdvancedRect.FixedSpace (Styles.AddButtonWidth),
					new AdvancedRect.ExpandedItem ("Label")
				),
				new AdvancedRect.FixedItem ("AddButton", Styles.AddButtonWidth, showAddButton)
			);

			Styles.HeaderBackground.Draw (rect);

			if (canSearch)
			{
				if (Count > 0)
					DrawSearchBar (rects["SearchBar"]);
			}

			if (!canSearch || (!searchBar.HasFocus () && string.IsNullOrEmpty (SearchString)))
				GUI.Label (rects["Label"], title, Styles.Header);

			// Draw add button
			if (showAddButton)
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
				new AdvancedRect.FixedItem ("DragIcon", Styles.DragIconWidth, draggable),
				new AdvancedRect.ExpandedItem ("Element")
			);

			if (draggable)
				Styles.DragIcon.Draw (rects["DragIcon"]);

			DrawElement (rects["Element"], index);
		}

		protected abstract void DrawElement (Rect rect, int index);
		#endregion

		#region General
		protected virtual void ContextClickElement (int index)
		{
			RepaintTree ();

			bool canInsertElementAbove = CanInsertElementAbove ();
			bool canInsertElementBelow = CanInsertElementBelow ();
			bool canCopyElement = CanCopyElement ();
			bool canPasteElement = CanPasteElement ();

			AdvancedGenericDropdown.Option[] dropdownOptions = {
				new AdvancedGenericDropdown.Item ("Remove", false, canAddAndRemove),
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
			});
		}

		private void PerformDrop (int insertIndex, List<int> draggedIds)
		{
			MoveElementSelection (insertIndex, draggedIds);
		}

		protected abstract string GetNameOfElementForSearch (int elementIndex);

		protected string FindNameOfElementForSearch (int elementIndex, string variableName)
		{
			Type elementType = typeof (ElementType);
			string name = string.Empty;

			if (string.IsNullOrEmpty (variableName))
				name = list[elementIndex] as string;
			else if (elementType.GetField (variableName) != null)
				name = elementType.GetField (variableName).GetValue (list[elementIndex]) as string;
			else if (elementType.GetProperty (variableName) != null)
				name = elementType.GetProperty (variableName).GetValue (list[elementIndex], null) as string;

			return name;
		}
		#endregion

		#region Element Management
		#region Add
		protected virtual bool CanAddElement ()
		{
			return canAddAndRemove & !HasSearch;
		}

		protected virtual void AddElement ()
		{
			AddElementAtIndex (Count);
		}
		#endregion

		#region Insert
		#region Above
		protected virtual bool CanInsertElementAbove ()
		{
			if (!canAddAndRemove || HasSearch)
				return false;

			return (Selection.Count == 1);
		}

		protected virtual void InsertElementAbove ()
		{
			AddElementAtIndex (LatestIdOfSelection);
		}
		#endregion

		#region Below
		protected virtual bool CanInsertElementBelow ()
		{
			if (!canAddAndRemove || HasSearch)
				return false;

			return (Selection.Count == 1);
		}

		protected virtual void InsertElementBelow ()
		{
			AddElementAtIndex (LatestIdOfSelection + 1);
		}
		#endregion
		#endregion

		#region Copy
		protected virtual bool CanCopyElement ()
		{
			if (!canCopyAndPaste)
				return false;

			return (Selection.Count == 1);
		}

		protected virtual void CopyElement ()
		{
			copiedElementHashCode = list[FirstIdOfSelection].GetHashCode ();
		}

		public int GetCopiedElementIndex ()
		{
			int index = -1;

			if (copiedElementHashCode == 0)
				return index;

			// Find for element with copied hash code
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GetHashCode () == copiedElementHashCode)
					return i;
			}

			return index;
		}
		#endregion

		#region Paste
		protected virtual bool CanPasteElement ()
		{
			if (!canCopyAndPaste || GetCopiedElementIndex () == -1)
				return false;

			return true;
		}

		protected virtual void PasteElement ()
		{
			int copiedElementIndex = GetCopiedElementIndex ();
		}
		#endregion

		#region Remove
		protected virtual void RemoveElementSelection ()
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

		#region General
		private void MoveElementSelection (int insertIndex, List<int> selectedIds)
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

		//protected virtual ElementType CreateObjectInstanceForAdd ()
		//{
		//    // This is ugly but there are a lot of cases like null types and default constructors
		//    Type listType = list.GetType ();
		//    Type elementType = listType.GetElementType ();

		//    if (elementType == typeof (string))
		//        return string.Empty as ElementType;
		//    else if (elementType != null && elementType.GetConstructor (Type.EmptyTypes) == null)
		//        Debug.LogErrorFormat ("Cannot add element. Type {0} has no default constructor. Implement a default constructor or implement your own add behaviour.", elementType);
		//    else if (listType.GetGenericArguments ()[0] != null)
		//        return Activator.CreateInstance (listType.GetGenericArguments ()[0]);
		//    else if (elementType != null)
		//        return Activator.CreateInstance (elementType);
		//    else
		//        Debug.LogError ("Cannot add element of type Null.");

		//    return null;
		//}
		#endregion
		#endregion

		#region TreeView Sortcuts
		private void ReloadTree ()
		{
			treeView.Reload ();
		}

		private void RepaintTree ()
		{
			treeView.Repaint ();
		}

		private void SetSelection (IList<int> ids)
		{
			if (ids == null)
				ids = new List<int> ();

			treeView.SetSelection (ids);
		}

		private void SetSelection (IList<int> ids, TreeViewSelectionOptions options)
		{
			treeView.SetSelection (ids, options);
		}
		#endregion
		#endregion
	}
}
#endif