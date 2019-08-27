#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
    public partial class ReorderableList
    {
        #region Class Members
        private TreeView treeView;

        private IList list;
        private System.Type elementType;

        private string searchVariableName;

        public string title;

        public bool canAddAndRemove;
        public delegate void AddElementDelegate (int addIndex);
        public AddElementDelegate onElementAdd;
        public AddElementDelegate onAfterElementAdd;

        public bool canCopyAndPaste;
        public delegate void CopyElementDelegate (int copyIndex);
        public CopyElementDelegate onElementCopy;
        public CopyElementDelegate onAfterElementCopy;

        public delegate void PasteElementDelegate (int copyIndex, int[] pasteIndexes);
        public PasteElementDelegate onElementPaste;
        public PasteElementDelegate onAfterElementPaste;

        public delegate void RemoveElementDelegate (List<int> removedIndexes);
        public RemoveElementDelegate onAfterElementsRemove;

        public delegate void DrawElementDelegate (Rect rect, int index);
        public DrawElementDelegate onElementDraw;
        #endregion

        #region Class Accesors
        public int Count
        {
            get { return list.Count; }
        }
        #endregion

        #region Class Implementation
        public ReorderableList (IList list, System.Type elementType, string searchVariableName, string title = "Reorderable List")
        {
            this.list = list;
            this.elementType = elementType;

            this.searchVariableName = searchVariableName;

            this.title = title;

            canAddAndRemove = true;
            canCopyAndPaste = true;

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