using UnityEngine;

namespace GGS_Framework
{
	[System.Serializable]
	public class FloatRange
	{
		#region Class members
		public float start;
		public float end;
		#endregion

		#region Class accesors
		public float Lenght
		{
			get { return end - start; }
		}
		#endregion

		#region Class implementation
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