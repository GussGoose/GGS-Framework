using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public class EnumPopupWindow : PopupWindowContent
    {
        #region Class members
        private SerializedProperty serializedProperty;
        private string[] enumNames;

        private SearchField searchField;

        private TreeViewState treeViewState;
        private EnumPopupTreeView treeView;
        #endregion

        #region Class overrides
        public override void OnOpen ()
        {
            enumNames = serializedProperty.enumNames;

            TreeViewState treeViewState = new TreeViewState ();
            treeView = new EnumPopupTreeView (treeViewState, enumNames);
            treeView.onEnumSelected += OnEnumSelected;

            searchField = new SearchField ();
            searchField.downOrUpArrowKeyPressed += treeView.SetFocusAndEnsureSelectedItem;
            searchField.SetFocus ();
        }

        public override void OnClose ()
        {
            treeView.onEnumSelected -= OnEnumSelected;
            searchField.downOrUpArrowKeyPressed -= treeView.SetFocusAndEnsureSelectedItem;
        }

        public override void OnGUI (Rect rect)
        {
            Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Vertical,
                new AdvancedRect.FixedSpace (4),
                new AdvancedRect.FixedItem ("SearchBar", 18, new RectPadding (-2, RectPaddingType.Horizontal)),
                new AdvancedRect.ExpandedItem ("List")
            );

            treeView.searchString = searchField.OnToolbarGUI (rects["SearchBar"], treeView.searchString);
            treeView.OnGUI (rects["List"]);
        }
        #endregion

        #region Class implementation
        public EnumPopupWindow (SerializedProperty serializedProperty)
        {
            this.serializedProperty = serializedProperty;
        }

        private void OnEnumSelected (int index)
        {
            serializedProperty.enumValueIndex = index;
            serializedProperty.serializedObject.ApplyModifiedProperties ();

            OnClose ();
            editorWindow.Close ();
        }
        #endregion
    }
}