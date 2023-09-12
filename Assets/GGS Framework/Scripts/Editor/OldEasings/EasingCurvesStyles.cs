// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	public static class EasingCurvesStyles
	{
		#region Class Members
		public static readonly GUISkin DarkSkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("EasingCurves/EasingCurves_DarkGUISkin.guiskin") as GUISkin;
		public static readonly GUISkin Lightkin = GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("EasingCurves/EasingCurves_LightGUISkin.guiskin") as GUISkin;

		public static readonly GUISkin Skin = (EditorGUIUtility.isProSkin) ? DarkSkin : Lightkin;

		public static readonly GUIStyle Background = Skin.GetStyle ("Background");
		public static readonly GUIStyle Ease = Skin.GetStyle ("Ease");
		public static readonly GUIStyle Header = Skin.GetStyle ("Header");
		public static readonly GUIStyle Circle = Skin.GetStyle ("Circle");
		public static readonly GUIStyle Arrow = Skin.GetStyle ("Arrow");
		#endregion
	}
}