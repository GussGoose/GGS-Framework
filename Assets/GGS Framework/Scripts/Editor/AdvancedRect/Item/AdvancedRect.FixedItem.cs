// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class FixedItem : Element
		{
			#region Class Implementation
			public FixedItem (string key, float size, RectPadding padding) : base (Type.Fixed, key, size, padding, true)
			{
			}

			public FixedItem (string key, float size, bool use) : base (Type.Fixed, key, size, (RectPadding) null, use)
			{
			}

			public FixedItem (string key, float size, RectPadding padding = null, bool use = true) : base (Type.Fixed, key, size, padding, use)
			{
			}
			#endregion
		}
	}
}