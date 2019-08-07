using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework.Development.Test
{
	public class RectSettings
	{
		#region Class members
		public bool use;
		public float size;

		public Rect rect;
		#endregion

		#region Class accesors
		public bool Expanded { get { return size == 0; } }
		#endregion

		#region Class implementation
		public RectSettings (float size)
		{
			this.size = size;
			this.use = true;
		}
		#endregion
	}
}