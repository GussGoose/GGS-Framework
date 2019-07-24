namespace UtilityFramework.Development
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

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

			Debug.Log (duplicate); //
		}
		#endregion

		#region Class implementation
		#endregion

		#region Interface implementation
		#endregion
	}
}