// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;

namespace GGS_Framework.Editor
{
	public static partial class AdvancedLabel
	{
		#region Class Implementation
		public static void Draw (Config config)
		{
			DoDraw (config, config.GetConfiguredStyle ());
		}

		public static void Draw (Config config, GUIStyle configuredLabelStyle)
		{
			DoDraw (config, configuredLabelStyle);
		}

		public static void Draw (Rect rect, Config config)
		{
			DoDraw (rect, config, config.GetConfiguredStyle ());
		}

		public static void Draw (Rect rect, Config config, GUIStyle configuredLabelStyle)
		{
			DoDraw (rect, config, configuredLabelStyle);
		}

		private static void DoDraw (Config config, GUIStyle configuredLabelStyle)
		{
			Rect rect = GUILayoutUtility.GetRect (new GUIContent (config.content), config.GetConfiguredStyle (), GUILayout.ExpandWidth (true));
			DoDraw (rect, config, configuredLabelStyle);
		}

		private static void DoDraw (Rect rect, Config config, GUIStyle configuredLabelStyle)
		{
			if (config.backgroundStyle != GUIStyle.none)
			{
				config.backgroundStyle.fixedHeight = rect.height;
				GUI.Box (rect, "", config.backgroundStyle);
			}

			GUI.Label (rect, config.content, configuredLabelStyle);
		}
		#endregion
	}
}