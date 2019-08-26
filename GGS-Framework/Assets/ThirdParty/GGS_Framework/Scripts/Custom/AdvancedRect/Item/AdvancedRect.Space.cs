using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGS_Framework;

namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class Space : Element
		{
			#region Class Implementation
			public Space (float size) : base (string.Empty, SizeType.Fixed, size, true)
			{
			} 
			#endregion
		}
	}
}