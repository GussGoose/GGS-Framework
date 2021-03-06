﻿using UnityEngine;

namespace GGS_Framework
{
	public static partial class ExtensionMethods
	{
		#region Class Implementation
		public static void GlobalReset (this Transform transform)
		{
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.Euler (Vector3.zero);
			transform.localScale = Vector3.one;
		}

		public static void LocalReset (this Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.Euler (Vector3.zero);
			transform.localScale = Vector3.one;
		}
		#endregion
	}
}