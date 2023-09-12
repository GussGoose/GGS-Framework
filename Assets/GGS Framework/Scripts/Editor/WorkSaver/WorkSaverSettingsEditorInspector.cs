// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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