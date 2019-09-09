namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class FixedSpace : Element
		{
			#region Class Implementation
			public FixedSpace (float size, bool use = true) : base (Type.Fixed, string.Empty, size, null, use)
			{
			}
			#endregion
		}
	}
}