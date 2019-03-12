#if UNITY_EDITOR
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenuTreeView : TreeView {

	#region Class members
	public System.Action<int> onValueSelected;
	private string[] values;

	private TreeViewState treeViewState;
	#endregion

	#region Class implementation
	public PopupMenuTreeView (TreeViewState treeViewState, string[] values) : base (treeViewState) {
		this.treeViewState = treeViewState;
		this.values = values;
		Reload ();
		SingleClickedItem (0);
	}
	#endregion

	#region Class overrides
	protected override TreeViewItem BuildRoot () {
		List<TreeViewItem> items = new List<TreeViewItem> ();
		for (int i = 0; i < values.Length; i++)
			items.Add (new TreeViewItem (i, 0, values[i].ToTitleCase ()));

		TreeViewItem root = new TreeViewItem (0, -1, "Root");
		root.children = items;
		return root;
	}

	protected override void KeyEvent () {
		base.KeyEvent ();

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
			SingleClickedItem (treeViewState.lastClickedID);
	}

	protected override void SingleClickedItem (int id) {
		base.SingleClickedItem (id);

		if (onValueSelected != null)
			onValueSelected (id);
	}
	#endregion
}
#endif