// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
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
        protected internal readonly SerializedProperty elements;
        #endregion

        #region Properties
        public override int ElementCount { get { return elements.arraySize; } }

        public SerializedObject SerializedObject { get { return elements?.serializedObject; } }

        public SerializedProperty Elements { get { return elements; } }
        #endregion

        #region Constructors
        protected SerializableReorderableList (ReorderableListState state, SerializedProperty elements, string title = "Reorderable List")
        {
            this.elements = elements;

            Initialize (state, title);

            Undo.undoRedoPerformed += delegate
            {
                SerializedObject.Update ();
                SerializedObject.ApplyModifiedProperties ();
                Refresh ();
            };
        }
        #endregion

        #region Implementation
        protected internal override void DoDraw (Rect rect)
        {
            SerializedObject.Update ();
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

            SerializedObject.ApplyModifiedProperties ();

            try
            {
                elements.GetArrayElementAtIndex (insertIndex).SetValue (value);
            }
            catch
            {
                // Ignored:
                // NullReferenceException: Object reference not set to an instance of an object
                // GGS_Framework.Editor.SerializedPropertyExtensions.SetValueNoRecord (UnityEditor.SerializedProperty property, System.Object value)
                // (at Assets/ThirdParty/GGS Framework/Scripts/Editor/Extensions/SerializedPropertyExtensions.cs:57)
            }

            SerializedObject.ApplyModifiedPropertiesWithoutUndo ();
            SerializedObject.Update ();

            ReloadTree ();
            SetSelection (new List<int> {insertIndex});
            ElementsChanged?.Invoke ();
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

            SerializedObject.ApplyModifiedProperties ();
            SerializedObject.Update ();

            SetSelection (selectedIds, TreeViewSelectionOptions.RevealAndFrame | TreeViewSelectionOptions.FireSelectionChanged);
            ReloadTree ();
            ElementsChanged?.Invoke ();
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

            SerializedObject.ApplyModifiedProperties ();
            SerializedObject.Update ();

            Refresh ();
            SetSelection (null);
            ElementsChanged?.Invoke ();
        }
        #endregion
    }
}