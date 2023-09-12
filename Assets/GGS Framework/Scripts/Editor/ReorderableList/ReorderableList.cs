// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Styles = GGS_Framework.Editor.ReorderableListStyles;

namespace GGS_Framework.Editor
{
    public abstract class ReorderableList
    {
        #region Members
        private ReorderableListTreeView treeView;
        private ReorderableListState state;
        private SearchField searchBar;

        private bool showContextMenuInNextDraw;
        #endregion

        #region Properties
        public abstract int ElementCount { get; }

        public Action ElementsChanged { get; set; }

        public ReorderableListState State { get { return state; } }

        public string Title { get; private set; }

        public bool ShowAlternatingRowBackgrounds
        {
            get { return treeView.ShowAlternatingRowBackgrounds; }
            set { treeView.ShowAlternatingRowBackgrounds = value; }
        }

        public bool HasSearch { get { return treeView.hasSearch; } }

        public string SearchString
        {
            get { return treeView.searchString; }
            private set { treeView.searchString = value; }
        }

        public Action<string> SearchChanged { get; set; }

        public Action KeyboardInputChanged { get; set; }

        public Action<int> SingleClickedElement { get; set; }

        public Action<int> DoubleClickedElement { get; set; }

        public Action<int[]> SelectionChanged { get; set; }

        public List<int> Selection { get { return state.SelectedIDs; } }

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

        public Action<int> ContextClickedElement { get; set; }

        public Action ContextClickedOutsideElements { get; set; }

        public bool ShowingScrollBar { get { return treeView.ShowingScrollBar; } }
        
        public float TotalElementsHeight { get { return treeView.TotalHeight; } }
        
        public Action<int, int[]> ElementsDragging { get; set; }

        public Action<int, int[]> ElementsDragged { get; set; }
        #endregion

        #region Implementation
        internal void Initialize (ReorderableListState state, string title)
        {
            Title = title;

            if (state == null)
                state = new ReorderableListState ();

            this.state = state;
            treeView = new ReorderableListTreeView (this, state.TreeViewState);

            searchBar = new SearchField ();
            searchBar.downOrUpArrowKeyPressed += treeView.SetFocusAndEnsureSelectedItem;

            InitializeTreeViewCallbacks ();
        }

        private void InitializeTreeViewCallbacks ()
        {
            treeView.SearchChangedCallback += (newSearch) =>
            {
                SearchChanged?.Invoke (newSearch);
                OnSearchChanged (newSearch);
            };

            treeView.DoesElementMatchSearchCallback += DoesElementMatchSearch;

            treeView.KeyboardInputChangedCallback += () =>
            {
                KeyboardInputChanged?.Invoke ();
                OnKeyboardInputChanged ();
            };

            treeView.SingleClickedElementCallback += id =>
            {
                SingleClickedElement?.Invoke (id);
                OnSingleClickedElement (id);
            };

            treeView.DoubleClickedElementCallback += id =>
            {
                DoubleClickedElement?.Invoke (id);
                OnDoubleClickedElement (id);
            };

            treeView.CanMultiSelectCallback += CanMultiSelect;

            treeView.SelectionChangedCallback += selection =>
            {
                SelectionChanged?.Invoke (selection);
                OnSelectionChanged (selection);
            };

            treeView.ContextClickedElementCallback += id =>
            {
                showContextMenuInNextDraw = true;
                RepaintTree ();

                ContextClickedElement?.Invoke (id);
                OnContextClickedElement (id);
            };

            treeView.ContextClickedOutsideElementsCallback += () =>
            {
                ContextClickedOutsideElements?.Invoke ();
                OnContextClickedOutsideElements ();
            };

            treeView.ElementDrawedCallback += (id, rect) =>
            {
                DrawElementBase (rect, id);
            };

            treeView.ElementsDraggingCallback += (insertIndex, draggedIDs) =>
            {
                ElementsDragging?.Invoke (insertIndex, draggedIDs);
                OnElementsDragging (insertIndex, draggedIDs);

                MoveElementSelection (insertIndex, draggedIDs);

                ElementsDragged?.Invoke (insertIndex, draggedIDs);
                OnElementsDragged (insertIndex, draggedIDs);
            };

            treeView.CanRenameElementCallback += CanRenameElement;
            treeView.GetElementRenameRectCallback += GetElementRenameRect;
            treeView.ElementRenamedCallback += RenameElement;
        }

        protected internal void ReloadTree ()
        {
            treeView.Reload ();
        }

        protected internal void RepaintTree ()
        {
            treeView.Repaint ();
        }

        public void Refresh ()
        {
            treeView.Reload ();
            treeView.Repaint ();
        }

        #region General Draw
        public virtual void LayoutDraw (float height)
        {
            Rect rect = EditorGUILayout.GetControlRect (false, height);
            DoDraw (rect);
        }

        public virtual void Draw (Rect rect)
        {
            DoDraw (rect);
        }

        protected internal virtual void DoDraw (Rect rect)
        {
            if (treeView.ItemCount != ElementCount)
            {
                ReloadTree ();
                RepaintTree ();
                return;
            }

            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Vertical,
                new AdvancedRect.FixedSpace (Styles.DefaultSpacing),
                new AdvancedRect.FixedItem ("Header", Styles.HeaderHeight, new RectPadding (Styles.DefaultPadding, RectPaddingType.Horizontal)),
                new AdvancedRect.FixedSpace (Styles.DefaultSpacing),
                new AdvancedRect.ExpandedItem ("TreeView")
            );

            DrawHeader (rects["Header"]);

            if (CanDrawBackground ())
            {
                Styles.Background.Draw (rects["TreeView"]);
                rects["TreeView"] = rects["TreeView"].Expand (new RectPadding (Styles.DefaultPadding, RectPaddingType.All));
            }

            if (ElementCount > 0)
            {
                if (showContextMenuInNextDraw)
                {
                    showContextMenuInNextDraw = false;
                    ShowContextElementMenu ();
                }

                treeView.OnGUI (rects["TreeView"]);
            }
            else
                AdvancedLabel.Draw (rects["TreeView"], new AdvancedLabel.Config (GetEmptyListMessage (), FontStyle.Bold));
        }

        protected virtual bool CanDrawBackground ()
        {
            return true;
        }

        protected virtual string GetEmptyListMessage ()
        {
            return "Nothing in list.";
        }

        protected internal virtual void DrawHeader (Rect rect)
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
                if (ElementCount > 0)
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
        #endregion

        #region Search
        protected virtual bool CanSearch ()
        {
            return true;
        }

        protected internal void DrawSearchBar (Rect rect)
        {
            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.ExpandedItem ("Bar"),
                new AdvancedRect.FixedItem ("CancelButton", Styles.HeaderHeight, new RectPadding (Styles.SearchBarCancelButtonPadding, RectPaddingType.All))
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

        protected virtual void OnSearchChanged (string newSearch)
        {
        }

        protected virtual bool DoesElementMatchSearch (int index, string search)
        {
            throw new NotImplementedException ();
        }
        #endregion

        #region Selection
        protected virtual bool CanMultiSelect (int index)
        {
            return true;
        }

        protected virtual void OnKeyboardInputChanged ()
        {
        }

        protected void SetSelection (IList<int> ids)
        {
            if (ids == null)
                ids = new List<int> ();

            treeView.SetSelection (ids.Select (id => id + state.UniqueID).ToList (), TreeViewSelectionOptions.RevealAndFrame | TreeViewSelectionOptions.FireSelectionChanged);
        }

        protected void SetSelection (IList<int> ids, TreeViewSelectionOptions options)
        {
            treeView.SetSelection (ids.Select (id => id + state.UniqueID).ToList (), options);
        }

        protected virtual void OnSingleClickedElement (int index)
        {
        }

        protected virtual void OnDoubleClickedElement (int index)
        {
        }

        protected virtual void OnSelectionChanged (int[] selection)
        {
        }
        #endregion

        #region Element
        protected virtual void OnContextClickedElement (int index)
        {
        }

        protected virtual void OnContextClickedOutsideElements ()
        {
        }

        protected internal void RefreshElementHeights ()
        {
            treeView.RefreshCustomRowHeights ();
        }

        public virtual float GetElementHeight (int index)
        {
            return 16;
        }

        internal void DrawElementBase (Rect rect, int index)
        {
            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
                new AdvancedRect.FixedItem ("ReorderIcon", Styles.DragIconWidth, CanReorder ()),
                new AdvancedRect.ExpandedItem ("Element")
            );

            if (CanReorder ())
                Styles.DragIcon.Draw (ExtendedRect.Align (new Vector2 (rects["ReorderIcon"].width, 16), rects["ReorderIcon"], RectAlignment.Center));

            DrawElement (rects["Element"], index);
        }

        protected abstract void DrawElement (Rect rect, int index);

        protected internal void ShowContextElementMenu ()
        {
            bool canRemove = CanRemoveSelection ();
            bool canInsertElementAbove = CanInsertElementAboveSelection ();
            bool canInsertElementBelow = CanInsertElementBelowSelection ();

            GenericMenu contextMenu = new GenericMenu ();

            if (canRemove)
                contextMenu.AddItem (new GUIContent ("Remove"), false, RemoveElementSelection);
            if (canInsertElementAbove || canInsertElementBelow)
                contextMenu.AddSeparator (string.Empty);
            if (canInsertElementAbove)
                contextMenu.AddItem (new GUIContent ("Insert Above"), false, InsertElementAboveSelection);
            if (canInsertElementBelow)
                contextMenu.AddItem (new GUIContent ("Insert Below"), false, InsertElementBelowSelection);

            contextMenu.ShowAsContext ();
        }
        #endregion

        #region Reorder
        protected internal virtual bool CanReorder ()
        {
            return true;
        }

        protected virtual void OnElementsDragging (int insertIndex, int[] draggedIDs)
        {
        }

        protected virtual void OnElementsDragged (int insertIndex, int[] draggedIDs)
        {
        }

        protected abstract void MoveElementSelection (int insertIndex, int[] selectedIds);
        #endregion

        #region Rename
        protected virtual bool CanRenameElement (int index)
        {
            return false;
        }

        protected virtual Rect GetElementRenameRect (int index, Rect totalRect)
        {
            return totalRect;
        }

        protected virtual void RenameElement (int index, bool renameAccepted, string originalName, string newName)
        {
            throw new NotImplementedException ();
        }
        #endregion

        #region List Management
        protected abstract void AddElementAtIndex (int insertIndex);

        #region Add
        protected virtual bool CanAdd ()
        {
            return true;
        }

        protected bool CanAddElement ()
        {
            return CanAdd () & !HasSearch;
        }

        protected virtual void AddElement ()
        {
            AddElementAtIndex (ElementCount);
        }
        #endregion

        #region Insert
        protected virtual bool CanInsert ()
        {
            return true;
        }

        protected bool CanInsertElement ()
        {
            return CanInsert () && CanAddElement ();
        }

        protected bool CanInsertElementAboveSelection ()
        {
            if (!CanInsertElement ())
                return false;

            return (Selection.Count == 1);
        }

        protected virtual void InsertElementAboveSelection ()
        {
            AddElementAtIndex (LatestOfSelection);
        }

        protected bool CanInsertElementBelowSelection ()
        {
            if (!CanInsertElement ())
                return false;

            return (Selection.Count == 1);
        }

        protected virtual void InsertElementBelowSelection ()
        {
            AddElementAtIndex (LatestOfSelection + 1);
        }
        #endregion

        #region Remove
        protected virtual bool CanRemove ()
        {
            return true;
        }

        protected bool CanRemoveSelection ()
        {
            return CanRemove ();
        }

        protected abstract void RemoveElementSelection ();
        #endregion
        #endregion
        #endregion
    }
}