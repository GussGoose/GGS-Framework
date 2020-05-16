using System;
using UnityEngine;

namespace GGS_Framework
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

			public AutomaticGroup (string key, Orientation orientation, Padding padding, params Element[] elements) : base (Type.Fixed, key, orientation, 0, padding, elements, true)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (string key, Orientation orientation, bool use, params Element[] elements) : base (Type.Fixed, key, orientation, 0, null, elements, use)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (string key, Orientation orientation, Padding padding, bool use, params Element[] elements) : base (Type.Fixed, key, orientation, 0, padding, elements, use)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (Orientation orientation, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, 0, null, elements, true)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (Orientation orientation, Padding padding, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, 0, padding, elements, true)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (Orientation orientation, bool use, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, 0, null, elements, use)
			{
				Size += GetSize (orientation, elements);
			}

			public AutomaticGroup (Orientation orientation, Padding padding, bool use, params Element[] elements) : base (Type.Fixed, string.Empty, orientation, 0, padding, elements, use)
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