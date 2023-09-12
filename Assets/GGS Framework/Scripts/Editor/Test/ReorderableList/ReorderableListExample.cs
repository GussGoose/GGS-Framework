// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0219

namespace GGS_Framework.Editor.Testing
{
	[ExecuteInEditMode]
	public class ReorderableListExample : MonoBehaviour
	{
		#region Class members
		public List<string> list = new List<string> ();

		public List<int> hashes = new List<int> ();
		#endregion

		#region Class accesors
		#endregion

		#region Class overrides
		private void Update ()
		{
			hashes.Clear ();

			bool duplicate = false;

			for (int i = 0; i < list.Count; i++)
			{
				int hashCode = list[i].GetHashCode ();

				if (hashes.Contains (hashCode))
					duplicate = true;

				hashes.Add (hashCode);
			}
		}
		#endregion

		#region Class implementation
		#endregion
	}
}