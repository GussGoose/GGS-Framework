using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
	[System.Serializable]
	public class ReorderableListExampleStruct
	{
		#region Class members
		public string displayName;
		public float exampleFloat;
		public bool exampleBool;
		#endregion

		#region Class accesors
		public string DisplayName
		{
			get { return displayName; }
		}
		#endregion
	}
}