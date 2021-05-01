using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Styles = GGS_Framework.Editor.ReorderableListStyles;

namespace GGS_Framework.Editor
{
    public abstract class SerializableReorderableList : ReorderableList
    {
        #region Members
        protected readonly SerializedObject serializedObject;
        protected readonly SerializedProperty elements;
        #endregion

        #region Properties
        public override int ElementCount { get { return elements.arraySize; } }
        #endregion

        #region Constructors
        protected SerializableReorderableList (ReorderableListState state, SerializedProperty elements, string title = "Reorderable List")
        {
            this.serializedObject = elements.serializedObject;
            this.elements = elements;

            Initialize (state, title);

            Undo.undoRedoPerformed += delegate
            {
                serializedObject.Update ();
                serializedObject.ApplyModifiedProperties ();
                Refresh ();
            };
        }
        #endregion

        #region Implementation
        protected internal override void DoDraw (Rect rect)
        {
            serializedObject.Update ();
            base.DoDraw (rect);
        }

        protected SerializedProperty GetElementAtIndex (int index)
        {
            return elements.GetArrayElementAtIndex (index);
        }

        protected override void AddElementAtIndex (int insertIndex)
        {
            AddElementAtIndex (insertIndex, null);
        }

        protected void AddElementAtIndex (int insertIndex, object value)
        {
            elements.InsertArrayElementAtIndex (insertIndex);

            serializedObject.ApplyModifiedProperties ();
            elements.GetArrayElementAtIndex (insertIndex).SetValue (value);

            serializedObject.ApplyModifiedPropertiesWithoutUndo ();
            serializedObject.Update ();

            ReloadTree ();
            SetSelection (new List<int> {insertIndex});
            ElementsListChanged?.Invoke ();
        }

        protected override void MoveElementSelection (int insertIndex, int[] selectedIds)
        {
            int originalInsert = insertIndex;
            foreach (int selectedElement in selectedIds)
            {
                if (selectedElement < originalInsert)
                    insertIndex -= 1;
            }

            for (int i = selectedIds.Length - 1; i > -1; i--)
            {
                elements.MoveArrayElement (selectedIds[i], ElementCount - 1);
                selectedIds[i] = ElementCount - selectedIds.Length + i;
            }

            for (int i = 0; i < selectedIds.Length; i++)
            {
                elements.MoveArrayElement (selectedIds[i], insertIndex);
                selectedIds[i] = insertIndex + i;
            }

            serializedObject.ApplyModifiedProperties ();
            serializedObject.Update ();

            SetSelection (selectedIds, TreeViewSelectionOptions.RevealAndFrame | TreeViewSelectionOptions.FireSelectionChanged);
            ReloadTree ();
            ElementsListChanged?.Invoke ();
        }

        protected override void RemoveElementSelection ()
        {
            List<int> selection = new List<int> (Selection);

            // Sort elements by descending 
            if (selection.Count > 1)
                selection.Sort ((a, b) => -1 * a.CompareTo (b));

            foreach (int id in selection)
            {
                SerializedProperty elementProperty = elements.GetArrayElementAtIndex (id);

                if (elementProperty.propertyType == SerializedPropertyType.ObjectReference)
                {
                    if (elementProperty.objectReferenceValue != null)
                        elementProperty.objectReferenceValue = null;
                }

                elements.DeleteArrayElementAtIndex (id);
            }

            serializedObject.ApplyModifiedProperties ();
            serializedObject.Update ();

            Refresh ();
            SetSelection (null);
            ElementsListChanged?.Invoke ();
        }
        #endregion
    }
}