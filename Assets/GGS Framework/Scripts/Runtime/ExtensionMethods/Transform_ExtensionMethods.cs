// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;

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