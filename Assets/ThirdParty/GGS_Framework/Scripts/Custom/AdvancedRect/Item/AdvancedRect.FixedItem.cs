namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class FixedItem : Element
		{
			#region Class Implementation
			public FixedItem (string key, float size, Padding padding) : base (Type.Fixed, key, size, padding, true)
			{
			}

			public FixedItem (string key, float size, bool use) : base (Type.Fixed, key, size, null, use)
			{
			}

			public FixedItem (string key, float size, Padding padding = null, bool use = true) : base (Type.Fixed, key, size, padding, use)
			{
			}
			#endregion
		}
	}
}