using UnityEngine;

namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public abstract partial class Element
		{
			#region Class Members
			protected Padding padding;
			#endregion

			#region Class Accesors
			public Type ElementType
			{
				get; private set;
			}

			public string Key
			{
				get; private set;
			}

			public float Size
			{
				get; protected set;
			}

			public Rect Rect
			{
				get; internal set;
			}

			public bool Use
			{
				get; protected set;
			}
			#endregion

			#region Class Implementation
			protected Element (Type type, string key, float size, Padding padding, bool use)
			{
				ElementType = type;

				Key = key;
				Size = size;

				this.padding = padding;

				Use = use;
			}

			public void ApplyPadding ()
			{
				if (padding != null)
					Rect = padding.Apply (Rect);
			}
			#endregion
		}
	}
}