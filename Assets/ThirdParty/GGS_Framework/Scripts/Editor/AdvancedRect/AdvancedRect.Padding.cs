using UnityEngine;

namespace GGS_Framework.Editor
{
	public partial class AdvancedRect
	{
		public partial class Padding
		{
			#region Class Accesors
			public RectOffset Offset
			{
				get; private set;
			}
			#endregion

			#region Class Implementation
			public Padding (int offset, Type type)
			{
				int left = ((type & Type.Left) != 0) ? offset : 0;
				int right = ((type & Type.Right) != 0) ? offset : 0;
				int top = ((type & Type.Top) != 0) ? offset : 0;
				int bottom = ((type & Type.Bottom) != 0) ? offset : 0;

				Offset = new RectOffset (left, right, top, bottom);
			}

			public Padding (RectOffset offset)
			{
				Offset = offset;
			}

			public Rect Apply (Rect rect)
			{
				return Offset.Add (rect);
			}
			#endregion
		}
	}
}
