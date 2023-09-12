// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public class PopupMenuTreeView : TreeView
    {
        #region Class members
        public System.Action<int> onValueSelected;
        private string[] values;

        private TreeViewState treeViewState;
        #endregion

        #region Class implementation
        public PopupMenuTreeView (TreeViewState treeViewState, string[] values) : base (treeViewState)
        {
            this.treeViewState = treeViewState;
            this.values = values;
            Reload ();
            SingleClickedItem (0);
        }
        #endregion

        #region Class overrides
        protected override TreeViewItem BuildRoot ()
        {
            List<TreeViewItem> items = new List<TreeViewItem> ();
            for (int i = 0; i < values.Length; i++)
                items.Add (new TreeViewItem (i, 0, values[i].ToTitleCase ()));

            TreeViewItem root = new TreeViewItem (0, -1, "Root");
            root.children = items;
            return root;
        }

        protected override void KeyEvent ()
        {
            base.KeyEvent ();

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
                SingleClickedItem (treeViewState.lastClickedID);
        }

        protected override void SingleClickedItem (int id)
        {
            base.SingleClickedItem (id);

            if (onValueSelected != null)
                onValueSelected (id);
        }
        #endregion
    }
}