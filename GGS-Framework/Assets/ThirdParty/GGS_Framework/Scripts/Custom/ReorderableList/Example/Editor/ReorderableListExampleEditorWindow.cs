using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GGS_Framework
{
	public class ReorderableListExampleEditorWindow : EditorWindow
	{
		#region Class members
		private ReorderableList reorderableList;
		#endregion

		#region Class accesors
		private ReorderableListExample Target
		{
			get { return FindObjectOfType<ReorderableListExample> (); }
		}

		private Rect Rect
		{
			get { return new Rect (Vector2.zero, position.size); }
		}
		#endregion

		#region Class overrides
		private void OnEnable ()
		{
			Initialize ();
		}

		private void OnFocus ()
		{
			Initialize ();
		}

		private void OnLostFocus ()
		{
			//Initialize ();
		}

		private void OnGUI ()
		{
			reorderableList.Draw (Rect);
		}
		#endregion

		#region Class implementation
		[MenuItem ("Window/GGS Framework/Reorderable List Example")]
		public static void Open ()
		{
			ReorderableListExampleEditorWindow window = CreateInstance<ReorderableListExampleEditorWindow> ();
			window.titleContent = new GUIContent ("Reorderable List Example");
			window.Show ();
		}

		private void Initialize ()
		{
			reorderableList = new ReorderableList (Target.exampleList, typeof (ReorderableListExampleStruct), "DisplayName");
			reorderableList.onElementDraw += OnElementDraw;
		}

		private void OnElementDraw (Rect rect, int index)
		{
			GUI.Label (rect, Target.exampleList[index].DisplayName);
		}
		#endregion

		#region Interface implementation
		#endregion
	}
}