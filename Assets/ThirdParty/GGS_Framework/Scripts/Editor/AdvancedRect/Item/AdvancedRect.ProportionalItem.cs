namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class ProportionalItem : Element
		{
			#region Class Implementation
			public ProportionalItem (string key, float percent, Padding padding) : base (Type.Proportional, key, percent, padding, true)
			{
			}

			public ProportionalItem (string key, float percent, bool use) : base (Type.Proportional, key, percent, null, use)
			{
			}

			public ProportionalItem (string key, float percent, Padding padding = null, bool use = true) : base (Type.Proportional, key, percent, padding, use)
			{
			}
			#endregion
		}
	}
}