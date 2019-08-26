using UnityEngine;

namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class Element
		{
			#region Class Accesors
			public string Key
			{
				get; private set;
			}

			public bool Use
			{
				get; private set;
			}

			internal SizeType SizeType
			{
				get; private set;
			}

			public float Size
			{
				get; private set;
			}

			public Rect Rect
			{
				get; internal set;
			}
			#endregion

			#region Class Implementation
			internal Element (string key, SizeType sizeType, float size, bool use)
			{
				Key = key;
				Use = use;

				SizeType = sizeType;
				Size = size;
			}
			#endregion
		}
	}
}