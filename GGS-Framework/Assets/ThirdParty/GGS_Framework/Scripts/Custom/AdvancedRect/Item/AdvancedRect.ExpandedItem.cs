using UnityEngine;
using System.Collections;

namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class ExpandedItem : Element
		{
			#region Class Implementation
			public ExpandedItem (string key, bool use) : base (key, SizeType.Expanded, 0, use)
			{
			} 
			#endregion
		}
	}
}