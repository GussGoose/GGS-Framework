namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class ProportionalItem : Element
		{
			#region Class Implementation
			public ProportionalItem (string key, float percent, bool use = true) : base (key, SizeType.Proportional, percent, use)
			{
			}
			#endregion
		}
	}
}