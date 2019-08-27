#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GGS_Framework
{
	public partial class ReorderableList
	{
		public class Styles
		{
			#region Class Members
			public static readonly GUISkin darkSkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("ReorderableList/ReorderableList_DarkGUISkin.guiskin") as GUISkin;
			public static readonly GUISkin lightkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("ReorderableList/ReorderableList_LightGUISkin.guiskin") as GUISkin;

			public static readonly GUISkin skin = (EditorGUIUtility.isProSkin) ? darkSkin : lightkin;

			public static readonly int defaultSpacing = 2;
			public static readonly int defaultPadding = -1;

			public static readonly float headerHeight = 16;
			public static readonly GUIStyle headerBackground = skin.GetStyle ("HeaderBackground");
			public static readonly GUIStyle header = skin.GetStyle ("Header");

			public static readonly GUIStyle searchBar = skin.GetStyle ("SearchBar");
			public static readonly int searchBarCancelButtonPadding = -3;
			public static readonly GUIStyle searchBarCancelButton = skin.GetStyle ("SearchBarCancelButton");

			public static readonly float addButtonWidth = 16;
			public static readonly GUIStyle addButton = skin.GetStyle ("AddButton");

			public static readonly float dragIconWidth = 16;
			public static readonly GUIStyle dragIcon = skin.GetStyle ("DragIcon");
			#endregion
		}
	}
}
#endif