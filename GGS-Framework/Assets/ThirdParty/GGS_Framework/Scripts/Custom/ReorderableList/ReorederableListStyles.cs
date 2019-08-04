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
			#region Class members
			public static readonly GUISkin darkSkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("ReorderableList/ReorderableList_DarkGUISkin.guiskin") as GUISkin;
			public static readonly GUISkin lightkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("ReorderableList/ReorderableList_LightGUISkin.guiskin") as GUISkin;

			public static readonly GUISkin skin = (EditorGUIUtility.isProSkin) ? darkSkin : lightkin;

			public static readonly float defaultSpacing = 2;
			public static readonly float defaultPadding = 2;

			public static readonly float headerHeight = 16;
			public static readonly GUIStyle headerBackground = skin.GetStyle ("HeaderBackground");
			public static readonly GUIStyle header = skin.GetStyle ("Header");

			public static readonly GUIStyle searchField = skin.GetStyle ("SearchField");

			public static readonly float addButtonWidth = 16;
			public static readonly GUIStyle addButton = skin.GetStyle ("AddButton");

			public static readonly float dragZoneWidth = 16;
			public static readonly GUIStyle dragZone = skin.GetStyle ("DragZone");
			#endregion
		}
	}
}