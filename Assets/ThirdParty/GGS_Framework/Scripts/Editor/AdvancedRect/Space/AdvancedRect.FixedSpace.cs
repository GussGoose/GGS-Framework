namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class FixedSpace : Element
		{
			#region Class Implementation
			public FixedSpace (float size, bool use = true) : base (Type.Fixed, string.Empty, size, (RectPadding) null, use)
			{
			}
			#endregion
		}
	}
}