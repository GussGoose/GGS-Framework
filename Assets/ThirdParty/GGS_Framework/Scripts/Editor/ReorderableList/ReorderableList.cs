using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Styles = GGS_Framework.Editor.ReorderableListStyles;
using ElementOptions = GGS_Framework.Editor.ReorderableListElementOptions;

namespace GGS_Framework.Editor
{
    public abstract class ReorderableList
    {
        #region Class Members
        internal ReorderableListTreeView treeView;
        internal ReorderableListState state;
        internal SearchField searchBar;

        internal bool showContextMenuInNextDraw;

        #region Events
        public Action onChanged;
        public Action<int[]> onSelectionChanged;
        public Action<int> onRightClickElement;
        #endregion
        #endregion

        #region Class Accesors
        public abstract int Count
        {
            get;
        }

        public string Title
        {
            get;
            private set;
        }

        public bool ShowAlternatingRowBackgrounds
        {
            get { return treeView.ShowAlternatingRowBackgrounds; }
            set { treeView.ShowAlternatingRowBackgrounds = value; }
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

        public int[] Selection
        {
            get { return state.TreeViewState.selectedIDs.Select (id => id - state.UniqueID).ToArray (); }
        }

        public int FirstOfSelection
        {
            get
            {
                if (Selection.Length == 0)
                    return -1;

                return Selection[0];
            }
        }

        public int LatestOfSelection
        {
            get
            {
                if (Selection.Length == 0)
                    return -1;

                return Selection[Selection.Length - 1];
            }
        }

        internal int CopiedElementHashCode
        {
            get;
            set;
        }
        #endregion

        #region Class Implementation
        internal void Initialize (ReorderableListState state, string title)
        {
            Title = title;

            if (state == null)
                state = new ReorderableListState ();

            this.state = state;
            treeView = new ReorderableListTreeView (this, state.TreeViewState);

            searchBar = new SearchField ();
            searchBar.downOrUpArrowKeyPressed += treeView.SetFocusAndEnsureSelectedItem;
        }

        #region Draw
        public virtual void Draw (float height)
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
            if (treeView.ItemCount != Count)
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
                // rects["TreeView"] = AdvancedRect.ExpandRect (rects["TreeView"], new RectPadding (Styles.DefaultPadding, RectPaddingType.All));
                // rects["TreeView"] = ExtendedRect.Expand (rects["TreeView"], new RectPadding (Styles.DefaultPadding, RectPaddingType.All));
            }

            if (Count > 0)
            {
                if (showContextMenuInNextDraw)
                {
                    showContextMenuInNextDraw = false;
                    ShowContextMenu ();
                }

                treeView.OnGUI (rects["TreeView"]);
            }
            else
                AdvancedLabel.Draw (rects["TreeView"], new AdvancedLabel.Config (GetEmptyListMessage (), FontStyle.Bold));
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

        public virtual float GetElementHeight (int index)
        {
            return 16;
        }

        [Obsolete ("This function is no longer used, you should remove the override.")]
        protected virtual string GetDisplayName (int index)
        {
            return string.Empty;
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
        #endregion

        #region General
        internal void PerformSingleClickedItem (int index)
        {
            SingleClickedItem (index);
        }

        protected virtual void SingleClickedItem (int index)
        {
        }

        internal void PerformSelectionChanged (int[] selection)
        {
            SelectionChanged (selection);
        }

        protected virtual void SelectionChanged (int[] selection)
        {
            onSelectionChanged?.Invoke (selection);
        }

        internal void PerformRightClickElement (int index)
        {
            RightClickElement (index);
        }

        protected virtual void RightClickElement (int index)
        {
            showContextMenuInNextDraw = true;
            RepaintTree ();
            onRightClickElement?.Invoke (index);
        }

        protected internal void ShowContextMenu ()
        {
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
        }

        internal void PerformDrop (int insertIndex, int[] draggedElements)
        {
            MoveElementSelection (insertIndex, draggedElements);
        }

        protected abstract int GetElementHashCode (int index);

        internal bool PerformDoesElementMatchSearch (int index, string search)
        {
            return DoesElementMatchSearch (index, search);
        }

        protected abstract bool DoesElementMatchSearch (int index, string search);

        internal void PerformSearchChanged (string newSearch)
        {
            SearchChanged (newSearch);
        }

        protected virtual void SearchChanged (string newSearch)
        {
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

        internal bool PerformCanReorder ()
        {
            return CanReorder ();
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
            return (Selection.Length == 1);
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
            return (Selection.Length == 1);
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

        protected abstract void DoRemoveElementSelection ();
        #endregion

        #region Copy
        protected bool CanCopyElement ()
        {
            if (!CanCopyAndPaste ())
                return false;
            return (Selection.Length == 1);
        }

        protected void CopyElement ()
        {
            CopiedElementHashCode = GetElementHashCode (FirstOfSelection);
        }

        protected abstract int GetCopiedElementIndex ();
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
        protected virtual void MoveElementSelection (int insertIndex, int[] selectedIds)
        {
            DoMoveElementSelection (insertIndex, selectedIds);
        }

        protected abstract void DoMoveElementSelection (int insertIndex, int[] selectedIds);

        protected virtual void AddElementAtIndex (int insertIndex)
        {
            DoAddElementAtIndex (insertIndex);
        }

        protected abstract void DoAddElementAtIndex (int insertIndex);
        #endregion
        #endregion

        #region TreeView Shortcuts
        protected internal void ReloadTree ()
        {
            treeView.Reload ();
        }

        protected internal void RepaintTree ()
        {
            treeView.Repaint ();
        }

        protected internal void RefreshCustomRowHeights ()
        {
            treeView.RefreshCustomRowHeights ();
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
        #endregion
        #endregion
    }
}