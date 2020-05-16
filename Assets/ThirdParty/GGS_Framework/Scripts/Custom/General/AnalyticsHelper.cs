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