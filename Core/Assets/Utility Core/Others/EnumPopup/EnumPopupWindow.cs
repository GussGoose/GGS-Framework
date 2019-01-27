using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class EnumPopupWindow : PopupWindowContent {

	#region Class members
	private SerializedProperty serializedProperty;
	private string[] enumNames;

	private SearchField searchField;

	private TreeViewState treeViewState;
	private EnumPopupTreeView treeView;
	#endregion

	#region Class overrides
	public override void OnOpen () {
		enumNames = serializedProperty.enumNames;

		TreeViewState treeViewState = new TreeViewState ();
		treeView = new EnumPopupTreeView (treeViewState, enumNames);
		treeView.onEnumSelected += OnEnumSelected;

		searchField = new SearchField ();
		searchField.downOrUpArrowKeyPressed += treeView.SetFocusAndEnsureSelectedItem;
		searchField.SetFocus ();
	}

	public override void OnClose () {
		treeView.onEnumSelected -= OnEnumSelected;
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
	public EnumPopupWindow (SerializedProperty serializedProperty) {
		this.serializedProperty = serializedProperty;
	}

	private void OnEnumSelected (int index) {
		serializedProperty.enumValueIndex = index;
		serializedProperty.serializedObject.ApplyModifiedProperties ();

		OnClose ();
		editorWindow.Close ();
	}
	#endregion
}