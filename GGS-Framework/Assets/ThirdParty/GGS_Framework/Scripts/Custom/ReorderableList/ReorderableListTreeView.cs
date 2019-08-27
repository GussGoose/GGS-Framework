#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework
{
    public partial class ReorderableList
    {
        public class TreeView : UnityEditor.IMGUI.Controls.TreeView
        {
            #region Class Members
            private const string DragKey = "ReorderableListDrag";

            private ReorderableList rl;

            private SearchField searchField;
            #endregion

            #region Class Accesors
            private int ItemCount
            {
                get { return rootItem.children.Count; }
            }

            private int LastSelectionItemId
            {
                get
                {
                    IList<int> selection = GetSelection ();
                    return selection[selection.Count - 1];
                }
            }
            #endregion

            #region Class Overrides
            protected override TreeViewItem BuildRoot ()
            {
                TreeViewItem root = new TreeViewItem (-1, -1, "Root");

                if (rl.Count > 0)
                {
                    for (int i = 0; i < rl.Count; i++)
                    {
                        string displayName = GetDisplayNameOfElement (i, rl.searchVariableName);
                        root.AddChild (new TreeViewItem (i, -1, displayName));
                    }
                }
                else
                    root.children = new List<TreeViewItem> ();

                return root;
            }

            protected override void RowGUI (RowGUIArgs args)
            {
                Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (args.rowRect,
                    new RectLayoutElement ("DragZone", Styles.dragZoneWidth),
                    new RectLayoutElement ("Element")
                );

                Styles.dragZone.Draw (rects["DragZone"]);

                if (rl.onElementDraw != null)
                    rl.onElementDraw (rects["Element"], args.item.id);
            }

            protected override void ContextClickedItem (int id)
            {
                base.ContextClickedItem (id);
                DoElementOptionsMenu ();
            }

            #region Drag
            protected override bool CanStartDrag (CanStartDragArgs args)
            {
                return true;
            }

            protected override void SetupDragAndDrop (SetupDragAndDropArgs args)
            {
                if (hasSearch)
                    return;

                DragAndDrop.PrepareStartDrag ();

                List<TreeViewItem> draggedItems = GetRows ().Where (item => args.draggedItemIDs.Contains (item.id)).ToList ();

                DragAndDrop.SetGenericData (DragKey, draggedItems);
                DragAndDrop.objectReferences = new UnityEngine.Object[] { };

                string dragTitle = (draggedItems.Count == 1) ? draggedItems[0].displayName : "<Multiple>";
                DragAndDrop.StartDrag (dragTitle);
            }

            protected override DragAndDropVisualMode HandleDragAndDrop (DragAndDropArgs args)
            {
                List<TreeViewItem> draggedItems = DragAndDrop.GetGenericData (DragKey) as List<TreeViewItem>;
                List<int> draggedIds = draggedItems.Select (item => item.id).ToList ();

                if (draggedItems == null)
                    return DragAndDropVisualMode.None;

                switch (args.dragAndDropPosition)
                {
                    case DragAndDropPosition.BetweenItems:
                        {
                            if (args.performDrop)
                                MoveElementSelection (args.insertAtIndex, draggedIds);

                            return DragAndDropVisualMode.Move;
                        }
                    default:
                        return DragAndDropVisualMode.None;
                }
            }
            #endregion
            #endregion

            #region Class Implementation
            public TreeView (ReorderableList reorderableList) : base (new TreeViewState ())
            {
                rl = reorderableList;

                searchField = new SearchField ();
                searchField.downOrUpArrowKeyPressed += SetFocusAndEnsureSelectedItem;

                Reload ();
            }

            public void Draw (Rect rect)
            {
                if (ItemCount != rl.Count)
                {
                    Reload ();
                    return;
                }

                Dictionary<string, Rect> rects = ExtendedRect.VerticalRects (rect,
                    new RectLayoutElement ("Header", Styles.headerHeight),
                    new RectLayoutElement (Styles.defaultSpacing),
                    new RectLayoutElement ("List")
                );

                DrawHeader (rects["Header"].HorizontalExpand (Styles.defaultPadding));

                if (rl.Count > 0)
                    OnGUI (rects["List"]);
                else
                    AdvancedGUILabel.Draw (rects["List"], new AdvancedGUILabelConfig ("Nothing in list.", FontStyle.Bold));
            }

            private void DrawHeader (Rect rect)
            {
                bool showAddButton = rl.canAddAndRemove && !hasSearch;

                Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (rect,
                    new RectLayoutElement ("Title"),
                    new RectLayoutElement ("AddButton", Styles.addButtonWidth, showAddButton)
                );

                Styles.headerBackground.Draw (rect);

                DrawTitle (rects["Title"]);

                if (showAddButton)
                    DrawAddButton (rects["AddButton"]);
            }

            private void DrawTitle (Rect rect)
            {
                Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (rect,
                    new RectLayoutElement (Styles.addButtonWidth),
                    new RectLayoutElement ("Title")
                );

                if (rl.Count > 0)
                    DrawSerchBar (rect);

                if (!searchField.HasFocus () && string.IsNullOrEmpty (searchString))
                    GUI.Label (rects["Title"], rl.title, Styles.header);
            }

            private void DrawSerchBar (Rect rect)
            {
                Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (rect,
                    new RectLayoutElement ("Bar"),
                    new RectLayoutElement ("Button", Styles.headerHeight)
                );

                searchString = searchField.OnGUI (rects["Bar"], searchString, Styles.searchBar, GUIStyle.none, GUIStyle.none);

                if (!string.IsNullOrEmpty (searchString))
                {
                    if (GUI.Button (rects["Button"].Expand (Styles.searchBarCancelButtonPadding), string.Empty, Styles.searchBarCancelButton))
                    {
                        searchString = string.Empty;
                        GUI.FocusControl (null);
                    }
                }
            }

            private void DrawAddButton (Rect rect)
            {
                if (GUI.Button (rect, string.Empty, Styles.addButton))
                    AddElement (rl.Count);
            }

            private void DoElementOptionsMenu ()
            {
                Repaint ();

                bool canInsertElementAbove = CanInsertElementAbove ();
                bool canInsertElementBelow = CanInsertElementBelow ();
                bool canCopyElement = CanCopyElement ();
                bool canPasteElement = CanPasteElement ();

                List<AdvancedGenericMenu.Item> items = new List<AdvancedGenericMenu.Item>
                {
                    new AdvancedGenericMenu.Item ("Remove", false, rl.canAddAndRemove),
                    new AdvancedGenericMenu.Separator (rl.canAddAndRemove),
                    new AdvancedGenericMenu.Item ("Copy", false, canCopyElement),
                    new AdvancedGenericMenu.Item ("Paste", false, canPasteElement),
                    new AdvancedGenericMenu.Separator (canCopyElement || canPasteElement),
                    new AdvancedGenericMenu.Item ("Insert Above", false, canInsertElementAbove),
                    new AdvancedGenericMenu.Item ("Insert Below", false, canInsertElementBelow)
                };

                AdvancedGenericMenu.Draw<ElementOptions> (items.ToArray (), item =>
                 {
                     ElementOptions option = (ElementOptions) item;

                     switch (option)
                     {
                         case ElementOptions.Remove:
                             RemoveElementSelection ();
                             break;
                         case ElementOptions.Copy:
                             CopyElement ();
                             break;
                         case ElementOptions.Paste:
                             PasteElement ();
                             break;
                         case ElementOptions.InsertAbove:
                             InsertElementAbove ();
                             break;
                         case ElementOptions.InsertBelow:
                             InsertElementBelow ();
                             break;
                     }
                 });
            }

            private string GetDisplayNameOfElement (int elementIndex, string variableName)
            {
                Type elementType = rl.elementType;
                string name = string.Empty;

                if (string.IsNullOrEmpty (variableName))
                    name = rl.list[elementIndex] as string;
                else if (elementType.GetField (variableName) != null)
                    name = elementType.GetField (variableName).GetValue (rl.list[elementIndex]) as string;
                else if (elementType.GetProperty (variableName) != null)
                    name = elementType.GetProperty (variableName).GetValue (rl.list[elementIndex], null) as string;

                if (string.IsNullOrEmpty (name))
                    name = "Unnamed";

                return name;
            }

            #region Element Management
            private void MoveElementSelection (int insertIndex, List<int> selectedIds)
            {
                if (insertIndex < 0)
                    return;

                List<object> selection = new List<object> ();

                for (int i = 0; i < selectedIds.Count; i++)
                    selection.Add (rl.list[selectedIds[i]]);

                foreach (object item in selection)
                    rl.list.Remove (item);

                int itemsAboveInsertIndex = 0;
                foreach (int selectedElement in selectedIds)
                {
                    if (selectedElement < insertIndex)
                        itemsAboveInsertIndex++;
                }

                insertIndex -= itemsAboveInsertIndex;

                selection.Reverse ();
                foreach (object item in selection)
                    rl.list.Insert (insertIndex, item);

                List<int> newSelection = new List<int> ();
                for (int i = insertIndex; i < insertIndex + selection.Count; i++)
                    newSelection.Add (i);

                SetSelection (newSelection, TreeViewSelectionOptions.RevealAndFrame);

                Reload ();
            }

            private void AddElement (int insertIndex)
            {
                if (rl.onElementAdd == null)
                {
                    object objectForAdd = GetObjectForAdd (rl.list);
                    rl.list.Insert (insertIndex, objectForAdd);
                }
                else
                    rl.onElementAdd (insertIndex);

                if (rl.onAfterElementAdd != null)
                    rl.onAfterElementAdd (insertIndex);

                Reload ();
                SetSelection (new List<int> { insertIndex });
            }

            private object GetObjectForAdd (IList list)
            {
                // This is ugly but there are a lot of cases like null types and default constructors
                Type listType = list.GetType ();
                Type elementType = listType.GetElementType ();

                if (elementType == typeof (string))
                    return "";
                else if (elementType != null && elementType.GetConstructor (Type.EmptyTypes) == null)
                    Debug.LogError ("Cannot add element. Type " + elementType.ToString () + " has no default constructor. Implement a default constructor or implement your own add behaviour.");
                else if (listType.GetGenericArguments ()[0] != null)
                    return Activator.CreateInstance (listType.GetGenericArguments ()[0]);
                else if (elementType != null)
                    return Activator.CreateInstance (elementType);
                else
                    Debug.LogError ("Cannot add element of type Null.");

                return null;
            }

            #region Insert
            private bool CanInsertElementAbove ()
            {
                if (!rl.canAddAndRemove || hasSearch)
                    return false;

                return (GetSelection ().Count == 1);
            }

            private void InsertElementAbove ()
            {
                AddElement (LastSelectionItemId);
            }

            private bool CanInsertElementBelow ()
            {
                if (!rl.canAddAndRemove || hasSearch)
                    return false;

                return (GetSelection ().Count == 1);
            }

            private void InsertElementBelow ()
            {
                AddElement (LastSelectionItemId + 1);
            }
            #endregion

            #region Copy
            private bool CanCopyElement ()
            {
                if (!rl.canCopyAndPaste)
                    return false;

                return (GetSelection ().Count == 1);
            }

            private void CopyElement ()
            {
                // TODO: Implement method

                if (rl.onElementCopy == null)
                {
                    // Implement copy
                }
                else
                {
                    //rl.onElementCopy (0);
                }

                //rl.onAfterElementCopy (0);
            }
            #endregion

            #region Paste
            private bool CanPasteElement ()
            {
                if (!rl.canCopyAndPaste)
                    return false;

                return false;
            }

            private void PasteElement ()
            {
                if (rl.onElementPaste == null)
                {
                    // Implement paste
                }
                else
                {
                    //rl.onElementPaste (0, null);
                }

                //rl.onAfterElementPaste (0, null);
            }
            #endregion

            #region Remove
            private void RemoveElementSelection ()
            {
                List<int> selection = state.selectedIDs;

                // Sort elements by descending 
                if (selection.Count > 1)
                    selection.Sort ((a, b) => -1 * a.CompareTo (b));

                foreach (int id in selection)
                    rl.list.RemoveAt (id);

                if (rl.onAfterElementsRemove != null)
                    rl.onAfterElementsRemove (selection);

                Reload ();
                SetSelection (new List<int> ());
            }
            #endregion
            #endregion
            #endregion
        }
    }
}
#endif