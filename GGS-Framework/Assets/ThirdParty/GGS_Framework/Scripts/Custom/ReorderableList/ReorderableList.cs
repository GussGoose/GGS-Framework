#if UNITY_EDITOR
using System.Collections;
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

		public delegate void AddElementDelegate (int addedIndex);
		public AddElementDelegate onElementAdd;

		public delegate void DuplicateElementDelegate (int duplicatedIndex, int targetIndex);
		public DuplicateElementDelegate onElementDuplicated;

		public delegate void CopyElementDelegate (int copiedIndex);
		public CopyElementDelegate onElementCopied;

		public delegate void PasteElementDelegate (int copiedIndex, int[] pastedIndexes);
		public PasteElementDelegate onElementPasted;

		public delegate void RemoveElementDelegate (int removedIndex);
		public RemoveElementDelegate onElementRemoved;

		public delegate void DrawElementDelegate (Rect rect, int index);
		public DrawElementDelegate onElementDraw;
		#endregion

		#region Class accesors
		public int Count
		{
			get { return list.Count; }
		}
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
#endif