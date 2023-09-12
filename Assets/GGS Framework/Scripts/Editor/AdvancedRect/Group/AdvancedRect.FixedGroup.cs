// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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