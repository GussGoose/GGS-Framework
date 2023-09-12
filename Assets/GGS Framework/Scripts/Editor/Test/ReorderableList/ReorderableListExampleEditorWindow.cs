// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor.Testing
{
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

            if (targetClass == null)
                return;

            reorderableList = new ReorderableList (targetClass.list, typeof (string), ReorderableListOptions.Default);
        }

        private void OnFocus ()
        {
            if (targetClass == null)
                OnEnable ();
        }

        private void OnGUI ()
        {
            if (targetClass == null)
                return;

            Rect rect = position;
            rect.position = Vector2.zero;

            reorderableList.Draw (rect);
        }
        #endregion

        #region Class implementation
        [MenuItem ("Window/GGS Framework/Development/Reorderable List Example")]
        public static void Open ()
        {
            EditorWindow window = GetWindow<ReorderableListExampleEditorWindow> ();
            window.Show ();
        }
        #endregion
    }
}