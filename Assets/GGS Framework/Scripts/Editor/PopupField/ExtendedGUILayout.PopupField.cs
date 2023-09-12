// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public partial class ExtendedGUILayout
    {
        #region Class Implementation
        public static bool PopupField (string label, string selected)
        {
            return PopupField (new GUIContent (label), selected);
        }

        public static bool PopupField (GUIContent label, string selected)
        {
            Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (EditorGUIUtility.singleLineHeight));
            return ExtendedGUI.PopupField (rect, label, selected);
        }
        #endregion
    }
}