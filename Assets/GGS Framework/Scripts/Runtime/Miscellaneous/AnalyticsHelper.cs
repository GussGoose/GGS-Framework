// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;
using UnityEngine.Analytics;

namespace GGS_Framework
{
	public static class AnalyticsHelper
	{
		#region Class Members
		private const bool DebugResult = true;
		#endregion

		#region Class Implementation
		public static void CustomEvent (string customEventName)
		{
			if (!Analytics.enabled)
				return;

			AnalyticsResult result = Analytics.CustomEvent (customEventName);

			if (DebugResult)
				Debug.Log (string.Format ("{0} - {1}", customEventName, result));
		}
		#endregion
	}
}