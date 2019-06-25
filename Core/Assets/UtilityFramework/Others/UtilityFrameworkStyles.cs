namespace UtilityFramework
{
#if UNITY_EDITOR
	using UnityEditor;
	using UnityEngine;

	public static class UtilityFrameworkStyles
	{
		#region Class accesors
		public static GUISkin Skin
		{
			get { return EditorGUIUtility.Load ("UtilityFramework/UtilityFrameworkGUISkin.guiskin") as GUISkin; }
		}
		#endregion
	} 
#endif
}