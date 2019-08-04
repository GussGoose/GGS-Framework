#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public static class EasingCurvesStyles
	{
		#region Class members
		public static readonly GUISkin darkSkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("EasingCurves/EasingCurves_DarkGUISkin.guiskin") as GUISkin;
		public static readonly GUISkin lightkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("EasingCurves/EasingCurves_LightGUISkin.guiskin") as GUISkin;

		public static readonly GUISkin skin = (EditorGUIUtility.isProSkin) ? darkSkin : lightkin;

		public static readonly GUIStyle background = skin.GetStyle ("Background");
		public static readonly GUIStyle ease = skin.GetStyle ("Ease");
		public static readonly GUIStyle header = skin.GetStyle ("Header");
		public static readonly GUIStyle circle = skin.GetStyle ("Circle");
		public static readonly GUIStyle arrow = skin.GetStyle ("Arrow");
		#endregion
	}
}
#endif