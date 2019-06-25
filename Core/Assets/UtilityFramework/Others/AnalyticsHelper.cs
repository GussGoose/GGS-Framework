namespace UtilityFramework
{
	using UnityEngine;
	using UnityEngine.Analytics;

	public static class AnalyticsHelper
	{
		#region Class members
		private const bool DebugResult = true;
		#endregion

		#region Class implementation
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