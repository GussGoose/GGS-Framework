#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace GGS_Framework
{
	public partial class ReorderableList<ElementType>
	{
		private class TreeView : UnityEditor.IMGUI.Controls.TreeView
		{
			#region Class Members
			private const string DragKey = "ReorderableListDrag";

			private ReorderableList<ElementType> reorderableList;
			#endregion

			#region Class Accesors
			public int ItemCount
			{
				get { return rootItem.children.Count; }
			}
			#endregion

			#region Class Overrides
			protected override TreeViewItem BuildRoot ()
			{
				TreeViewItem root = new TreeViewItem (-1, -1, "Root");

				root.children = new List<TreeViewItem> ();
				for (int i = 0; i < reorderableList.Count; i++)
				{
					string displayName = reorderableList.GetNameOfElementForSearch (i);

					if (string.IsNullOrEmpty (displayName) || string.IsNullOrWhiteSpace (displayName))
						displayName = "Unnamed";

					root.AddChild (new TreeViewItem (i, -1, displayName));
				}

				return root;
			}

			protected override void RowGUI (RowGUIArgs args)
			{
				reorderableList.DrawElementBase (args.rowRect, args.item.id);
			}

			protected override void ContextClickedItem (int id)
			{
				base.ContextClickedItem (id);
				reorderableList.ContextClickElement (id);
			}

			#region Drag
			protected override bool CanStartDrag (CanStartDragArgs args)
			{
				return reorderableList.CanReorder ();
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
								reorderableList.PerformDrop (args.insertAtIndex, draggedIds);

							return DragAndDropVisualMode.Move;
						}
					default:
						return DragAndDropVisualMode.None;
				}
			}
			#endregion
			#endregion

			#region Class Implementation
			public TreeView (ReorderableList<ElementType> reorderableList, TreeViewState treeViewState) : base (treeViewState)
			{
				this.reorderableList = reorderableList;
				Reload ();
			}
			#endregion
		}
	}
}
#endif