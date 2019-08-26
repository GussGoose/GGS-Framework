namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class FixedGroup : Group
		{
			#region Class Implementation
			public FixedGroup (string key, Orientation orientation, float size, params Element[] elements) : base (key, orientation, SizeType.Fixed, size, true, elements)
			{
			}

			public FixedGroup (string key, Orientation orientation, float size, bool use, params Element[] elements) : base (key, orientation, SizeType.Fixed, size, use, elements)
			{
			}

			public FixedGroup (Orientation orientation, float size, params Element[] elements) : base (string.Empty, orientation, SizeType.Fixed, size, true, elements)
			{
			}

			public FixedGroup (Orientation orientation, float size, bool use, params Element[] elements) : base (string.Empty, orientation, SizeType.Fixed, size, use, elements)
			{
			} 
			#endregion
		}
	}
}