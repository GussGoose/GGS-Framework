using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Styles = GGS_Framework.Editor.ReorderableListStyles;
using ElementOptions = GGS_Framework.Editor.ReorderableListElementOptions;

namespace GGS_Framework.Editor
{
    public abstract class SerializableReorderableList : ReorderableList
    {
        #region Class Members
        protected readonly SerializedObject serializedObject;
        protected readonly SerializedProperty elements;
        #endregion

        #region Class Accesors
        public override int Count
        {
            get { return elements.arraySize; }
        }
        #endregion

        #region Constructors
        protected SerializableReorderableList (ReorderableListState state, SerializedObject serializedObject, SerializedProperty elements, string title = "Reorderable List")
        {
            this.serializedObject = serializedObject;
            this.elements = elements;

            Initialize (state, title);

            Undo.undoRedoPerformed += delegate
            {
                serializedObject.Update ();
                serializedObject.ApplyModifiedProperties ();
                ReloadTree ();
                RepaintTree ();
            };
        }
        #endregion

        #region Overrides
        public override void Draw (Rect rect)
        {
            serializedObject.Update ();
            base.Draw (rect);
        }

        protected SerializedProperty GetPropertyAtIndex (int index)
        {
            return elements.GetArrayElementAtIndex (index);
        }
        
        protected override void DoMoveElementSelection (int insertIndex, int[] selectedIds)
        {
            int originalInsert = insertIndex;
            foreach (int selectedElement in selectedIds)
            {
                if (selectedElement < originalInsert)
                    insertIndex -= 1;
            }

            for (int i = selectedIds.Length - 1; i > -1; i--)
            {
                elements.MoveArrayElement (selectedIds[i], Count - 1);
                selectedIds[i] = Count - selectedIds.Length + i;
            }

            for (int i = 0; i < selectedIds.Length; i++)
            {
                elements.MoveArrayElement (selectedIds[i], insertIndex);
                selectedIds[i] = insertIndex + i;
            }

            serializedObject.ApplyModifiedProperties ();
            serializedObject.Update ();

            SetSelection (selectedIds, TreeViewSelectionOptions.RevealAndFrame);
            ReloadTree ();
            onChanged?.Invoke ();
        }

        protected abstract void ElementAdded (SerializedProperty element, int index);

        protected override void DoAddElementAtIndex (int insertIndex)
        {
            elements.InsertArrayElementAtIndex (insertIndex);
            ElementAdded (GetPropertyAtIndex (insertIndex), insertIndex);

            serializedObject.ApplyModifiedProperties ();
            serializedObject.Update ();

            ReloadTree ();
            SetSelection (new List<int> {insertIndex});
            onChanged?.Invoke ();
        }
        
        protected override void DoRemoveElementSelection ()
        {
            List<int> selection = new List<int> (Selection);

            // Sort elements by descending 
            if (selection.Count > 1)
                selection.Sort ((a, b) => -1 * a.CompareTo (b));

            foreach (int id in selection)
                elements.DeleteArrayElementAtIndex (id);

            serializedObject.ApplyModifiedProperties ();
            serializedObject.Update ();

            ReloadTree ();
            SetSelection (null);
            onChanged?.Invoke ();
        }

        protected override int GetCopiedElementIndex ()
        {
            int index = -1;
            if (CopiedElementHashCode == 0)
                return index;

            // Find for element with copied hash code
            for (int i = 0; i < Count; i++)
            {
                if (GetPropertyAtIndex (i).GetHashCode () == CopiedElementHashCode)
                    return i;
            }

            return index;
        }
        #endregion
    }
}