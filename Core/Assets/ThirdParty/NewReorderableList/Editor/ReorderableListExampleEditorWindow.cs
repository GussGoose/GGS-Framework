namespace UtilityFramework.Development
{
	using UnityEditor;
	using UnityEngine;

	public class ReorderableListExampleEditorWindow : EditorWindow
	{
		#region Class members
		private ReorderableList reorderableList;
		private ReorderableListExample targetClass;
		#endregion

		#region Class accesors
		#endregion

		#region Class overrides
		private void OnEnable ()
		{
			targetClass = FindObjectOfType<ReorderableListExample> ();
			reorderableList = new ReorderableList (targetClass.list, typeof (string), ReorderableListOptions.Default);
		}

		private void OnGUI ()
		{
			Rect rect = position;
			rect.position = Vector2.zero;

			reorderableList.Draw (rect);
		}
		#endregion

		#region Class implementation
		[MenuItem ("Window/Utility Framework/Development/Reorderable List Example")]
		public static void Initialize ()
		{
			EditorWindow window = EditorWindow.GetWindow<ReorderableListExampleEditorWindow> ();
			window.Show ();
		}
		#endregion

		#region Interface implementation
		#endregion
	}
}