using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CoreUtilityStyles {

	#region Class accesors
	public static GUISkin Skin { get { return EditorGUIUtility.Load ("CoreUtility/CoreUtilitySkin.guiskin") as GUISkin; } }

	public static GUIStyle TextPoint { get { return Skin.GetStyle ("TextIndicator"); } }
	#endregion

	#region Class implementation
	#endregion
}