using UnityEngine;

namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public abstract class Group : Element
		{
			#region Class Accesors
			public Orientation Orientation
			{
				get;
				private set;
			}

			public Element[] Elements
			{
				get;
				private set;
			}
			#endregion

			#region Class Implementation
			public Group (Type type, string key, Orientation orientation, float size, Padding padding, Element[] elements, bool use) : base (type, key, size, padding, use)
			{
				if (padding != null)
				{
					if (orientation == Orientation.Horizontal)
						Size -= base.padding.Offset.horizontal;
					else
						Size -= base.padding.Offset.vertical;
				}

				Orientation = orientation;
				Elements = elements;
			}

			public void ComputeElements ()
			{
				Rect rect = Rect;

				if (padding != null)
					rect = padding.Apply (rect);

				ComputeElementsRect (rect, Orientation, Elements);
			}
			#endregion
		}
	}
}