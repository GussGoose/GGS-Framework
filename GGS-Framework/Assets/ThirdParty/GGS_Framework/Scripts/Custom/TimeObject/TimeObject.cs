using System;

namespace GGS_Framework
{
	[Serializable]
	public class TimeObject
	{
		#region Class members
		public TimeObjectType type;
		public double value;
		#endregion

		#region Class accesors
		public TimeSpan TimeSpan
		{
			get
			{
				switch (type)
				{
					case TimeObjectType.Miliseconds:
						return TimeSpan.FromMilliseconds (value);
					case TimeObjectType.Seconds:
						return TimeSpan.FromSeconds (value);
					case TimeObjectType.Minutes:
						return TimeSpan.FromMinutes (value);
					case TimeObjectType.Hours:
						return TimeSpan.FromHours (value);
					case TimeObjectType.Days:
						return TimeSpan.FromDays (value);
				}

				return TimeSpan.Zero;
			}
		}
		#endregion

		#region Operators
		public static implicit operator TimeSpan (TimeObject timeObject)
		{
			return timeObject.TimeSpan;
		}
		#endregion
	}
}