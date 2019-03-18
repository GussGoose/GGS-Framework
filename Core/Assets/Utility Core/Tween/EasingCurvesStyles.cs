using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class EasingCurvesStyles {

	#region Class accesors
	public static GUISkin DarkSkin { get { return EditorGUIUtility.Load ("CoreUtility/TweenEasingCurves/TweenEasingCurves_DarkSkin.guiskin") as GUISkin; } }
	public static GUISkin Lightkin { get { return EditorGUIUtility.Load ("CoreUtility/TweenEasingCurves/TweenEasingCurves_LightSkin.guiskin") as GUISkin; } }

	public static GUISkin Skin { get { return (EditorGUIUtility.isProSkin) ? DarkSkin : Lightkin; } }

	public static GUIStyle Background { get { return Skin.GetStyle ("Background"); } }
	public static GUIStyle Ease { get { return Skin.GetStyle ("Ease"); } }
	public static GUIStyle Header { get { return Skin.GetStyle ("Header"); } }
	public static GUIStyle Circle { get { return Skin.GetStyle ("Circle"); } }
	public static GUIStyle Arrow { get { return Skin.GetStyle ("Arrow"); } }
	#endregion
}