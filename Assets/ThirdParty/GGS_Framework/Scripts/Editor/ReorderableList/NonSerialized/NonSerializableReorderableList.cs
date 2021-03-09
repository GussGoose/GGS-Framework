using System;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using Styles = GGS_Framework.Editor.ReorderableListStyles;

namespace GGS_Framework.Editor
{
    public abstract class NonSerializableReorderableList<TElement> : ReorderableList
    {
        #region Members
        protected List<TElement> list;
        #endregion

        #region Properties
        public override int Count
        {
            get { return list.Count; }
        }
        #endregion

        #region Constructors
        protected NonSerializableReorderableList (ReorderableListState state, List<TElement> list, string title = "Reorderable List")
        {
            this.list = list;
            Initialize (state, title);
        }
        #endregion

        #region Implementation
        protected override void AddElementAtIndex (int insertIndex)
        {
            AddElementAtIndex (insertIndex, CreateElementObject ());
        }

        protected void AddElementAtIndex (int insertIndex, TElement value)
        {
            list.Insert (insertIndex, value);

            ReloadTree ();
            SetSelection (new List<int> {insertIndex});
            ListChanged?.Invoke ();
        }

        protected virtual TElement CreateElementObject ()
        {
            throw new NotImplementedException ();
            // return Activator.CreateInstance<TElement> ();
        }

        protected override void MoveElementSelection (int insertIndex, int[] selectedIds)
        {
            if (insertIndex < 0)
                return;

            List<object> selection = new List<object> ();

            for (int i = 0; i < selectedIds.Length; i++)
                selection.Add (list[selectedIds[i]]);

            foreach (TElement item in selection)
                list.Remove (item);

            int itemsAboveInsertIndex = 0;
            foreach (int selectedElement in selectedIds)
            {
                if (selectedElement < insertIndex)
                    itemsAboveInsertIndex++;
            }

            insertIndex -= itemsAboveInsertIndex;

            selection.Reverse ();
            foreach (TElement item in selection)
                list.Insert (insertIndex, item);

            List<int> newSelection = new List<int> ();
            for (int i = insertIndex; i < insertIndex + selection.Count; i++)
                newSelection.Add (i);

            SetSelection (newSelection, TreeViewSelectionOptions.RevealAndFrame | TreeViewSelectionOptions.FireSelectionChanged);
            ReloadTree ();
            ListChanged?.Invoke ();
        }

        protected override void RemoveElementSelection ()
        {
            List<int> selection = new List<int> (Selection);

            // Sort elements by descending 
            if (selection.Count > 1)
                selection.Sort ((a, b) => -1 * a.CompareTo (b));

            foreach (int id in selection)
                list.RemoveAt (id);

            Refresh ();
            SetSelection (null);
            ListChanged?.Invoke ();
        }
        #endregion
    }
}