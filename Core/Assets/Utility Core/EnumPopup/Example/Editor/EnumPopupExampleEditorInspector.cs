using UnityEditor;

[CustomEditor (typeof (EnumPopupExample))]
public class EnumPopupExampleEditorInspector : Editor {

	#region Class members
	private EnumPopupExample targetScript;
	#endregion

	#region Class overrides
	private void OnEnable () {
		targetScript = (EnumPopupExample) base.target;
	}

	public override void OnInspectorGUI () {
		DrawDefaultInspector ();

		SerializedObject serializedObject = new SerializedObject (targetScript);
		SerializedProperty serializedProperty = serializedObject.FindProperty ("customInspectorExample");

		EnumPopup.Popup (serializedProperty, "Custom Inspector Example");
	}
	#endregion
}