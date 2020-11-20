using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    [CustomEditor (typeof(WorkSaverSettings))]
    public class WorkSaverSettingsEditorInspector : UnityEditor.Editor
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