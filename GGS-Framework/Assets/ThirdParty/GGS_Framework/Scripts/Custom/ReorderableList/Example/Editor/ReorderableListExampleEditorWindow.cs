using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public class ReorderableListExampleEditorWindow : EditorWindow
	{
		#region Class Members
		private ReorderableListExampleReorderableList reorderableList;
		#endregion

		#region Class Accesors
		private ReorderableListExample Target
		{
			get { return FindObjectOfType<ReorderableListExample> (); }
		}

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
			if (Target == null)
			{
				string message = "Go to Assets/ThirdParty/GGS_Framework/Scripts/Custom/ReorderableList/Example/Example.unity and open the scene.";

				GUILayout.FlexibleSpace ();
				AdvancedLabel.Draw (new AdvancedLabel.Config (message));
				GUILayout.FlexibleSpace ();

				Initialize ();

				if (Target == null)
					return;
			}

			if (reorderableList == null)
				Initialize ();

			reorderableList.Draw (AdvancedRect.ExpandRect (Rect, new AdvancedRect.Padding (-2, AdvancedRect.Padding.Type.All)));
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
			if (Target == null)
				return;

			reorderableList = new ReorderableListExampleReorderableList (Target.exampleList);
		}
		#endregion
	}
}