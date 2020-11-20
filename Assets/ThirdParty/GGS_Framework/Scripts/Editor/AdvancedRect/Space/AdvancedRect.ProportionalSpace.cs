namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class ProportionalSpace : Element
		{
			#region Class Implementation
			public ProportionalSpace (float percent, bool use = true) : base (Type.Proportional, string.Empty, percent, null, use)
			{
			}
			#endregion
		}
	}
}