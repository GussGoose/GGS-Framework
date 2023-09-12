// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class ProportionalItem : Element
		{
			#region Class Implementation
			public ProportionalItem (string key, float percent, RectPadding padding) : base (Type.Proportional, key, percent, padding, true)
			{
			}

			public ProportionalItem (string key, float percent, bool use) : base (Type.Proportional, key, percent, (RectPadding) null, use)
			{
			}

			public ProportionalItem (string key, float percent, RectPadding padding = null, bool use = true) : base (Type.Proportional, key, percent, padding, use)
			{
			}
			#endregion
		}
	}
}