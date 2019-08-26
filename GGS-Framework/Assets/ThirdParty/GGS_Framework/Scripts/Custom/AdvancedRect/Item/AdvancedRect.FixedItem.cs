namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class FixedItem : Element
		{
			#region Class Implementation
			public FixedItem (string key, float size, bool use = true) : base (key, SizeType.Fixed, size, use)
			{
			} 
			#endregion
		}
	}
}