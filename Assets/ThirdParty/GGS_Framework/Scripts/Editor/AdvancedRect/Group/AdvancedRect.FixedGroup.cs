namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class FixedGroup : Group
		{
			#region Class Implementation
			public FixedGroup (string key, Orientation orientation, float size, params Element[] elements) : base (Type.Fixed, key, orientation, size, null, elements, true)
			{
			}

			public FixedGroup (string key, Orientation orientation, float size, RectPadding padding, params Element[] elements) : base (Type.Fixed, key, orientation, size, padding, elements, true)
			{
			}

			public FixedGroup (string key, Orientation orientation, float size, bool use, params Element[] elements) : base (Type.Fixed, key, orientation, size, null, elements, use)
			{
			}

			public FixedGroup (string key, Orientation orientation, float size, RectPadding padding, bool use, params Element[] elements) : base (Type.Fixed, key, orientation, size, padding, elements, use)
			{
			}

			public FixedGroup (Orientation orientation, float size, RectPadding padding, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, size, padding, elements, true)
			{
			}

			public FixedGroup (Orientation orientation, float size, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, size, null, elements, true)
			{
			}

			public FixedGroup (Orientation orientation, float size, bool use, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, size, null, elements, use)
			{
			}

			public FixedGroup (Orientation orientation, float size, RectPadding padding, bool use, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, size, padding, elements, use)
			{
			}
			#endregion
		}
	}
}