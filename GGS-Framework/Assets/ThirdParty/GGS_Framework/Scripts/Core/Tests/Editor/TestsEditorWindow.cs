using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
    public class TestsEditorWindow : EditorWindow
    {
        #region Class Members
        #endregion

        #region Class Accesors
        private Tests Target
        {
            get { return FindObjectOfType<Tests> (); }
        }
        #endregion

        #region Class Overrides
        private void OnGUI ()
        {
        }
        #endregion

        #region Class Implementation
        [MenuItem ("Window/GGS Framework/Tests")]
        public static void Open ()
        {
            TestsEditorWindow window = GetWindow<TestsEditorWindow> ();
            window.titleContent = new GUIContent ("GGSF Tests");
            window.Show ();
        }
        #endregion
    }
}
