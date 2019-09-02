using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	[CustomEditor (typeof (WorkSaverSettings))]
	public class WorkSaverSettingsEditorInspector : Editor
	{
		#region Class Implementation
		public override void OnInspectorGUI ()
		{
			if (GUILayout.Button ("Edit Settings"))
				WorkSaverSettingsEditorWindow.Open ();
		}
		#endregion
	}
}