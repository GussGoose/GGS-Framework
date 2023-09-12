// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class ExpandedItem : Element
		{
			#region Class Implementation
			public ExpandedItem (string key, RectPadding padding) : base (Type.Expanded, key, 0, padding, true)
			{
			}

			public ExpandedItem (string key, bool use) : base (Type.Expanded, key, 0, (RectPadding) null, use)
			{
			}

			public ExpandedItem (string key, RectPadding padding = null, bool use = true) : base (Type.Expanded, key, 0, padding, use)
			{
			}
			#endregion
		}
	}
}