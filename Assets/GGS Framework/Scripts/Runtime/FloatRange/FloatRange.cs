// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;

namespace GGS_Framework
{
	[System.Serializable]
	public class FloatRange
	{
		#region Class Members
		public float start;
		public float end;
		#endregion

		#region Class Accesors
		public float Lenght
		{
			get { return end - start; }
		}
		#endregion

		#region Class Implementation
		public FloatRange ()
		{
			start = 0;
			end = 0;
		}

		public FloatRange (float start, float end)
		{
			this.start = start;
			this.end = end;
		}

		public bool InRange (float value)
		{
			return (value >= start && value <= end);
		}

		public float GetRandomValue ()
		{
			return Random.Range (start, end);
		}
		#endregion
	}
}