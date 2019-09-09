#if UNITY_EDITOR
using UnityEngine;

namespace GGS_Framework
{
	public static partial class AdvancedLabel
	{
		#region Class Implementation
		public static void Draw (Config config)
		{
			DoDraw (config);
		}

		public static void Draw (Rect rect, Config config)
		{
			DoDraw (rect, config);
		}

		private static void DoDraw (Config config)
		{
			Rect rect = GUILayoutUtility.GetRect (new GUIContent (config.content), config.GetConfiguredStyle (), GUILayout.ExpandWidth (true));
			DoDraw (rect, config);
		}

		private static void DoDraw (Rect rect, Config config)
		{
			config.backgroundStyle.fixedHeight = rect.height;
			GUI.Box (rect, "", config.backgroundStyle);

			GUIStyle labelStyle = config.GetConfiguredStyle ();
			GUI.Label (rect, config.content, labelStyle);
		}
		#endregion
	}
}
#endif