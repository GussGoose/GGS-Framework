using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using Styles = GGS_Framework.Editor.ReorderableListStyles;
using ElementOptions = GGS_Framework.Editor.ReorderableListElementOptions;

namespace GGS_Framework.Editor
{
	public abstract class NonSerializableReorderableList<TElement> : ReorderableList
	{
		#region Class Members
		protected List<TElement> list;
		#endregion

		#region Class Accesors
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
		
		#region Class Implementation
		#region General
		protected override void DoRemoveElementSelection ()
		{
			List<int> selection = new List<int> (Selection);

			// Sort elements by descending 
			if (selection.Count > 1)
				selection.Sort ((a, b) => -1 * a.CompareTo (b));

			foreach (int id in selection)
				list.RemoveAt (id);

			ReloadTree ();
			SetSelection (null);

			onChanged?.Invoke ();
		}
		#endregion

		#region Copy
		protected override int GetCopiedElementIndex ()
		{
			int index = -1;

			if (CopiedElementHashCode == 0)
				return index;

			// Find for element with copied hash code
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].GetHashCode () == CopiedElementHashCode)
					return i;
			}

			return index;
		}
		#endregion

		#region General
		protected override void DoMoveElementSelection (int insertIndex, int[] selectedIds)
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

			SetSelection (newSelection, TreeViewSelectionOptions.RevealAndFrame);
			ReloadTree ();
			onChanged?.Invoke ();
		}

		protected override void DoAddElementAtIndex (int insertIndex)
		{
			DoAddElementAtIndex (insertIndex, CreateElementObject ());
		}

		protected void DoAddElementAtIndex (int insertIndex, TElement value)
		{
			list.Insert (insertIndex, value);

			ReloadTree ();
			SetSelection (new List<int> { insertIndex });
			onChanged?.Invoke ();
		}

		protected abstract TElement CreateElementObject ();
		#endregion
		#endregion
	}
}