using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GGS_Framework.Editor
{
    internal class ReorderableListTreeView : TreeView
    {
        #region Members
        private readonly ReorderableList list;
        #endregion

        #region Properties
        private string DragKey
        {
            get { return $"ReorderableListDrag_{UniqueId}"; }
        }

        public int ItemCount
        {
            get { return rootItem.children.Count; }
        }

        private int UniqueId
        {
            get { return list.State.UniqueID; }
        }

        public bool ShowAlternatingRowBackgrounds
        {
            get { return showAlternatingRowBackgrounds; }
            set { showAlternatingRowBackgrounds = value; }
        }

        #region Events
        public Action<string> SearchChangedCallback { get; set; }

        public Func<int, string, bool> DoesElementMatchSearchCallback { get; set; }

        public Func<int, bool> CanMultiSelectCallback { get; set; }

        public Action KeyboardInputChangedCallback { get; set; }

        public Action<int> SingleClickedElementCallback { get; set; }

        public Action<int> DoubleClickedElementCallback { get; set; }

        public Action<int[]> SelectionChangedCallback { get; set; }

        public Action<int> ContextClickedElementCallback { get; set; }

        public Action ContextClickedOutsideElementsCallback { get; set; }

        public Action<int, Rect> ElementDrawedCallback { get; set; }

        public Action<int, int[]> ElementsDraggingCallback { get; set; }

        public Func<int, bool> CanRenameElementCallback { get; set; }

        public Func<int, Rect, Rect> GetElementRenameRectCallback { get; set; }

        public Action<int, bool, string, string> ElementRenamedCallback { get; set; }
        #endregion
        #endregion

        #region Constructors
        public ReorderableListTreeView (ReorderableList list, TreeViewState treeViewState) : base (treeViewState)
        {
            this.list = list;

            Reload ();
        }
        #endregion

        #region Implementation
        protected override TreeViewItem BuildRoot ()
        {
            TreeViewItem root = new TreeViewItem (-1, -1, "Root");
            root.children = new List<TreeViewItem> ();

            for (int i = 0; i < list.Count; i++)
                root.AddChild (new TreeViewItem (list.State.UniqueID + i, -1, "Element"));

            return root;
        }

        protected override void SearchChanged (string newSearch)
        {
            SearchChangedCallback?.Invoke (newSearch);
        }

        protected override bool DoesItemMatchSearch (TreeViewItem item, string search)
        {
            return DoesElementMatchSearchCallback != null && DoesElementMatchSearchCallback (GetItemId (item), search);
        }

        protected override bool CanMultiSelect (TreeViewItem item)
        {
            return CanMultiSelectCallback != null && CanMultiSelectCallback (GetItemId (item));
        }

        protected override void KeyEvent ()
        {
            KeyboardInputChangedCallback?.Invoke ();
        }

        protected override void SingleClickedItem (int id)
        {
            SingleClickedElementCallback?.Invoke (id - UniqueId);
        }

        protected override void DoubleClickedItem (int id)
        {
            DoubleClickedElementCallback?.Invoke (id - UniqueId);
        }

        protected override void SelectionChanged (IList<int> selectedIds)
        {
            SelectionChangedCallback?.Invoke (selectedIds.Select (id => id - UniqueId).ToArray ());
        }

        protected override void ContextClickedItem (int id)
        {
            ContextClickedElementCallback (id - UniqueId);
        }

        protected override void ContextClicked ()
        {
            ContextClickedOutsideElementsCallback?.Invoke ();
        }

        internal new void RefreshCustomRowHeights ()
        {
            base.RefreshCustomRowHeights ();
        }

        protected override float GetCustomRowHeight (int row, TreeViewItem item)
        {
            return list.GetElementHeight (GetItemId (item));
        }

        protected override void RowGUI (RowGUIArgs args)
        {
            args.item.displayName = "Item";
            list.DrawElementBase (args.rowRect, GetItemId (args.item));
        }

        protected override bool CanStartDrag (CanStartDragArgs args)
        {
            return list.CanReorder ();
        }

        protected override void SetupDragAndDrop (SetupDragAndDropArgs args)
        {
            if (hasSearch)
                return;

            DragAndDrop.PrepareStartDrag ();

            List<TreeViewItem> draggedItems = GetRows ().Where (item => args.draggedItemIDs.Contains (item.id)).ToList ();

            DragAndDrop.SetGenericData (DragKey, draggedItems);
            DragAndDrop.objectReferences = new Object[] { };

            string dragTitle = (draggedItems.Count == 1) ? draggedItems[0].displayName : "<Multiple>";
            DragAndDrop.StartDrag (dragTitle);
        }

        protected override DragAndDropVisualMode HandleDragAndDrop (DragAndDropArgs args)
        {
            List<TreeViewItem> draggedItems = DragAndDrop.GetGenericData (DragKey) as List<TreeViewItem>;

            if (draggedItems == null)
                return DragAndDropVisualMode.None;

            int[] draggedIds = draggedItems.Select (GetItemId).ToArray ();

            switch (args.dragAndDropPosition)
            {
                case DragAndDropPosition.BetweenItems:
                {
                    if (args.performDrop)
                        ElementsDraggingCallback?.Invoke (args.insertAtIndex, draggedIds);

                    return DragAndDropVisualMode.Move;
                }
                default:
                    return DragAndDropVisualMode.None;
            }
        }

        protected override bool CanRename (TreeViewItem item)
        {
            return CanRenameElementCallback != null && CanRenameElementCallback (GetItemId (item));
        }

        protected override Rect GetRenameRect (Rect rowRect, int row, TreeViewItem item)
        {
            return GetElementRenameRectCallback?.Invoke (GetItemId (item), rowRect) ?? Rect.zero;
        }

        protected override void RenameEnded (RenameEndedArgs args)
        {
            ElementRenamedCallback?.Invoke (args.itemID - UniqueId, args.acceptedRename, args.originalName, args.newName);
        }

        private int GetItemId (TreeViewItem item)
        {
            return item.id - UniqueId;
        }
        #endregion
    }
}