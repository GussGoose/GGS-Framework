﻿// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class ExpandedGroup : Group
		{
			#region Class Implementation
			public ExpandedGroup (string key, Orientation orientation, params Element[] elements) : base (Type.Expanded, key, orientation, 0, null, elements, true)
			{
			}

			public ExpandedGroup (string key, Orientation orientation, RectPadding padding, params Element[] elements) : base (Type.Expanded, key, orientation, 0, padding, elements, true)
			{
			}

			public ExpandedGroup (string key, Orientation orientation, bool use, params Element[] elements) : base (Type.Expanded, key, orientation, 0, null, elements, use)
			{
			}

			public ExpandedGroup (string key, Orientation orientation, RectPadding padding, bool use, params Element[] elements) : base (Type.Expanded, key, orientation, 0, padding, elements, use)
			{
			}

			public ExpandedGroup (Orientation orientation, params Element[] elements) : base (Type.Expanded, string.Empty, orientation, 0, null, elements, true)
			{
			}

			public ExpandedGroup (Orientation orientation, RectPadding padding, params Element[] elements) : base (Type.Expanded, string.Empty, orientation, 0, padding, elements, true)
			{
			}

			public ExpandedGroup (Orientation orientation, bool use, params Element[] elements) : base (Type.Expanded, string.Empty, orientation, 0, null, elements, use)
			{
			}

			public ExpandedGroup (Orientation orientation, RectPadding padding, bool use, params Element[] elements) : base (Type.Expanded, string.Empty, orientation, 0, padding, elements, use)
			{
			}
			#endregion
		}
	}
}