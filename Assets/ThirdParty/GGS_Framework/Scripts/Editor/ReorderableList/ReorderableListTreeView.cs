using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public class ReorderableListTreeView : TreeView
    {
        #region Class Members
        private readonly ReorderableList list;
        #endregion

        #region Class Accesors
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
            get { return list.state.UniqueId; }
        }

        public bool ShowAlternatingRowBackgrounds
        {
            get { return showAlternatingRowBackgrounds; }
            set { showAlternatingRowBackgrounds = value; }
        }
        #endregion

        #region Constructor
        public ReorderableListTreeView (ReorderableList list, TreeViewState treeViewState) : base (treeViewState)
        {
            this.list = list;
            Reload ();
        }
        #endregion

        #region Class Overrides
        protected override TreeViewItem BuildRoot ()
        {
            TreeViewItem root = new TreeViewItem (-1, -1, "Root");
            root.children = new List<TreeViewItem> ();

            for (int i = 0; i < list.Count; i++)
                root.AddChild (new TreeViewItem (list.state.UniqueId + i, -1, "Element"));

            return root;
        }

        protected override float GetCustomRowHeight (int row, TreeViewItem item)
        {
            int index = item.id - UniqueId;
            return list.GetElementHeight (index);
        }

        protected override void RowGUI (RowGUIArgs args)
        {
            int index = args.item.id - UniqueId;

            args.item.displayName = list.PerformGetDisplayName (index);
            list.DrawElementBase (args.rowRect, index);
        }

        protected override void ContextClickedItem (int id)
        {
            base.ContextClickedItem (id);

            int index = id - UniqueId;
            list.PerformRightClickElement (index);
        }

        protected override void SelectionChanged (IList<int> selectedIds)
        {
            base.SelectionChanged (selectedIds);
            list.PerformSelectionChanged (selectedIds.Select (id => id - UniqueId).ToArray ());
        }

        protected override bool DoesItemMatchSearch (TreeViewItem item, string search)
        {
            int index = item.id - UniqueId;
            return list.PerformDoesElementMatchSearch (index, search);
        }

        protected override void SearchChanged (string newSearch)
        {
            list.PerformSearchChanged (newSearch);
        }

        #region Drag
        protected override bool CanStartDrag (CanStartDragArgs args)
        {
            return list.PerformCanReorder ();
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

            int[] draggedIds = draggedItems.Select (item => item.id - UniqueId).ToArray ();

            switch (args.dragAndDropPosition)
            {
                case DragAndDropPosition.BetweenItems:
                {
                    if (args.performDrop)
                        list.PerformDrop (args.insertAtIndex, draggedIds);

                    return DragAndDropVisualMode.Move;
                }
                default:
                    return DragAndDropVisualMode.None;
            }
        }
        #endregion
        #endregion
    }
}