// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
using UnityEngine;

namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class AutomaticGroup : Group
		{
			#region Class Implementation
			#region Overloads
			public AutomaticGroup (string key, Orientation orientation, params Element[] elements) : base (Type.Fixed, key, orientation, 0, null, elements, true)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (string key, Orientation orientation, RectPadding padding, params Element[] elements) : base (Type.Fixed, key, orientation, 0, padding, elements, true)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (string key, Orientation orientation, bool use, params Element[] elements) : base (Type.Fixed, key, orientation, 0, null, elements, use)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (string key, Orientation orientation, RectPadding padding, bool use, params Element[] elements) : base (Type.Fixed, key, orientation, 0, padding, elements, use)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (Orientation orientation, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, 0, null, elements, true)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (Orientation orientation, RectPadding padding, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, 0, padding, elements, true)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (Orientation orientation, bool use, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, 0, null, elements, use)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (Orientation orientation, RectPadding padding, bool use, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, 0, padding, elements, use)
			{
				Size += GetSize (orientation, elements);
			}
			#endregion

			private float GetSize (Orientation orientation, Element[] elements)
			{
				Element[] elementsCopy = new Element[elements.Length];
				Array.Copy (elements, elementsCopy, elements.Length);

				Vector2 totalSize = ComputeElementsRect (Rect.zero, orientation, elementsCopy);

				return (orientation == Orientation.Horizontal) ? totalSize.x : totalSize.y;
			}
			#endregion
		}
	}
}