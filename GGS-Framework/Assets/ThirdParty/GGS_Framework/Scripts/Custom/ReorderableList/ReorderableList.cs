using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework
{
	public partial class ReorderableList
	{
		#region Class members
		private TreeView treeView;

		private IList list;
		private System.Type elementType;

		private string searchVariableName;

		public string title;

		public delegate void DrawElementDelegate (Rect rect, int index);
		public DrawElementDelegate onDrawElement;
		#endregion

		#region Class accesors
		#endregion

		#region Class overrides
		#endregion

		#region Class implementation
		public ReorderableList (IList list, System.Type elementType, string searchVariableName, string title = "Reorderable List")
		{
			this.list = list;
			this.elementType = elementType;

			this.searchVariableName = searchVariableName;

			this.title = title;

			treeView = new TreeView (this);
		}

		public void Draw (Rect rect)
		{
			treeView.Draw (rect);
		}
		#endregion
	}
}