#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public static class GGS_FrameworkStyles
	{
		#region Class Accesors
		public static GUISkin Skin
		{
			get { return EditorGUIUtility.Load ("UtilityFramework/UtilityFrameworkGUISkin.guiskin") as GUISkin; }
		}
		#endregion
	}
}
#endif