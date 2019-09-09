#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
    public partial class ReorderableList<ElementType>
    {
        public static class Styles
        {
            #region Class Members
            public static readonly GUISkin DarkSkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("ReorderableList/ReorderableList_DarkGUISkin.guiskin") as GUISkin;
            public static readonly GUISkin Lightkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("ReorderableList/ReorderableList_LightGUISkin.guiskin") as GUISkin;

            public static readonly GUISkin Skin = (EditorGUIUtility.isProSkin) ? DarkSkin : Lightkin;

            public static readonly int DefaultSpacing = 2;
            public static readonly int DefaultPadding = -1;

            public static readonly GUIStyle Background = EditorStyles.helpBox;

            public static readonly float HeaderHeight = 16;
            public static readonly GUIStyle HeaderBackground = Skin.GetStyle ("HeaderBackground");
            public static readonly GUIStyle Header = Skin.GetStyle ("Header");

            public static readonly GUIStyle SearchBar = Skin.GetStyle ("SearchBar");
            public static readonly int SearchBarCancelButtonPadding = -3;
            public static readonly GUIStyle SearchBarCancelButton = Skin.GetStyle ("SearchBarCancelButton");

            public static readonly float AddButtonWidth = 16;
            public static readonly GUIStyle AddButton = Skin.GetStyle ("AddButton");

            public static readonly float DragIconWidth = 16;
            public static readonly GUIStyle DragIcon = Skin.GetStyle ("DragIcon");
            #endregion
        }
    }
}
#endif