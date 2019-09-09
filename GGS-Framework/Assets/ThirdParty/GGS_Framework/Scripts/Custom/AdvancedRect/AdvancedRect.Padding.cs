using UnityEngine;

namespace GGS_Framework
{
	public partial class AdvancedRect
	{
		public partial class Padding
		{
			#region Class Members
			private RectOffset offset;
			#endregion

			#region Class Implementation
			public Padding (int offset, Type type)
			{
				int left = ((type & Type.Left) != 0) ? offset : 0;
				int right = ((type & Type.Right) != 0) ? offset : 0;
				int top = ((type & Type.Top) != 0) ? offset : 0;
				int bottom = ((type & Type.Bottom) != 0) ? offset : 0;

				this.offset = new RectOffset (left, right, top, bottom);
			}

			public Padding (RectOffset offset)
			{
				this.offset = offset;
			}

			public Rect Apply (Rect rect)
			{
				return offset.Add (rect);
			}
			#endregion
		}
	}
}
