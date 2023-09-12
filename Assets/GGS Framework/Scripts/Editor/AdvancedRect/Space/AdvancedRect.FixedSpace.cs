// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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