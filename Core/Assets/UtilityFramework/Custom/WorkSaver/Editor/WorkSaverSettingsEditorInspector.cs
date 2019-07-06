namespace UtilityFramework
{
	using UnityEditor;
	using UnityEngine;

	[CustomEditor (typeof (WorkSaverSettings))]
	public class WorkSaverSettingsEditorInspector : Editor
	{
		#region Class implementation
		public override void OnInspectorGUI ()
		{
			if (GUILayout.Button ("Edit Settings"))
				WorkSaverSettingsEditorWindow.Open ();
		}
		#endregion
	}
}