namespace UtilityFramework
{
	using UnityEditor;

	[CustomEditor (typeof (EnumPopupExample))]
	public class EnumPopupExampleEditorInspector : Editor
	{
		#region Class members
		private EnumPopupExample targetClass;
		#endregion

		#region Class overrides
		private void OnEnable ()
		{
			targetClass = (EnumPopupExample) base.target;
		}

		public override void OnInspectorGUI ()
		{
			DrawDefaultInspector ();

			SerializedObject serializedObject = new SerializedObject (targetClass);
			SerializedProperty serializedProperty = serializedObject.FindProperty ("customInspectorExample");

			EnumPopup.Popup (serializedProperty, "Custom Inspector Example");
		}
		#endregion
	} 
}