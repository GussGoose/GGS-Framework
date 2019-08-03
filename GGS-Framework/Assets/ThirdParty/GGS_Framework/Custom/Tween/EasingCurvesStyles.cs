#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public static class EasingCurvesStyles
	{
		#region Class accesors
		public static GUISkin DarkSkin
		{
			get { return EditorGUIUtility.Load ("UtilityFramework/TweenEasingCurves/TweenEasingCurves_DarkGUISkin.guiskin") as GUISkin; }
		}

		public static GUISkin Lightkin
		{
			get { return EditorGUIUtility.Load ("UtilityFramework/TweenEasingCurves/TweenEasingCurves_LightGUISkin.guiskin") as GUISkin; }
		}

		public static GUISkin Skin
		{
			get { return (EditorGUIUtility.isProSkin) ? DarkSkin : Lightkin; }
		}

		public static GUIStyle Background
		{
			get { return Skin.GetStyle ("Background"); }
		}

		public static GUIStyle Ease
		{
			get { return Skin.GetStyle ("Ease"); }
		}

		public static GUIStyle Header
		{
			get { return Skin.GetStyle ("Header"); }
		}

		public static GUIStyle Circle
		{
			get { return Skin.GetStyle ("Circle"); }
		}

		public static GUIStyle Arrow
		{
			get { return Skin.GetStyle ("Arrow"); }
		}
		#endregion
	}
#endif
}