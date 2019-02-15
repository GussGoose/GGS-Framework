using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class PopupMenuWindow : PopupWindowContent {

	#region Class members
	private string[] values;
	private System.Action<int, string> onValueSelected;

	private SearchField searchField;

	private TreeViewState treeViewState;
	private PopupMenuTreeView treeView;
	#endregion

	#region Class overrides
	public override void OnOpen () {
		TreeViewState treeViewState = new TreeViewState ();
		treeView = new PopupMenuTreeView (treeViewState, values);
		treeView.onValueSelected += OnValueSelected;

		searchField = new SearchField ();
		searchField.downOrUpArrowKeyPressed += treeView.SetFocusAndEnsureSelectedItem;
		searchField.SetFocus ();
	}

	public override void OnClose () {
		treeView.onValueSelected -= OnValueSelected;
		searchField.downOrUpArrowKeyPressed -= treeView.SetFocusAndEnsureSelectedItem;
	}

	public override void OnGUI (Rect rect) {
		Dictionary<string, Rect> rects = ExtendedRect.VerticalRects (rect,
			new RectLayoutElement (4),
			new RectLayoutElement ("SearchField", 18),
			new RectLayoutElement ("List"));

		treeView.searchString = searchField.OnToolbarGUI (rects["SearchField"].HorizontalExpand (4), treeView.searchString);
		treeView.OnGUI (rects["List"]);
	}
	#endregion

	#region Class implementation
	public PopupMenuWindow (System.Action<int, string> onValueSelected, string[] values) {
		this.onValueSelected = onValueSelected;
		this.values = values;
	}

	private void OnValueSelected (int index) {
		if (onValueSelected != null)
			onValueSelected.Invoke (index, values[index]);

		OnClose ();
		editorWindow.Close ();
	}
	#endregion
}