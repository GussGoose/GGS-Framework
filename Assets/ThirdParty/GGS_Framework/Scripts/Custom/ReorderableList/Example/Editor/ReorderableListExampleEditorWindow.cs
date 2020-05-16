using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public class ReorderableListExampleEditorWindow : EditorWindow
	{
		#region Class Members
		[SerializeField] private List<ReorderableListExampleStruct> list = new List<ReorderableListExampleStruct> ();

		private ReorderableListExampleReorderableList reorderableList;
		[SerializeField] private ReorderableListState reorderableListState;
		#endregion

		#region Class Accesors
		private Rect Rect
		{
			get { return new Rect (Vector2.zero, position.size); }
		}
		#endregion

		#region Class Overrides
		private void OnEnable ()
		{
			Initialize ();
		}

		private void OnGUI ()
		{
			if (reorderableList == null)
				Initialize ();
			
			reorderableList.Draw (Rect);
		}
		#endregion

		#region Class Implementation
		[MenuItem ("Window/GGS Framework/Examples/Reorderable List")]
		public static void Open ()
		{
			ReorderableListExampleEditorWindow window = CreateInstance<ReorderableListExampleEditorWindow> ();
			window.titleContent = new GUIContent ("RL Example");
			window.Show ();
		}

		private void Initialize ()
		{
			if (reorderableListState == null)
				reorderableListState = new ReorderableListState ();

			reorderableList = new ReorderableListExampleReorderableList (reorderableListState, list);
		}
		#endregion
	}
}