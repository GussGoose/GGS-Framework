// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
	public static class DatesManager
	{
		#region Class Members
		private const string DateKey = "SavedDateKey_";

		private static Dictionary<string, DateTime> dates = new Dictionary<string, DateTime> ();
		private static bool dataLoaded;
		#endregion

		#region Class Implementation
		// Called to update the date of an existing key, if it doesnt it generates a new one.
		public static void UpdateDate (string key, DateTime date)
		{
			if (!dataLoaded)
				LoadData ();

			if (!dates.ContainsKey (key))
				dates.Add (key, date);
			else
				dates[key] = date;

			SaveData ();
		}

		// Called in order to get an existing key, if it doesnt exists it returns the min value of System.DateTime
		public static DateTime GetDate (string key)
		{
			if (!dataLoaded)
				LoadData ();

			return (dates.ContainsKey (key)) ? dates[key] : DateTime.MinValue;
		}

		// Using the size of the dates dictionary, It stores first the keys with their position in the dictionary and then it stores as a pair the key and its DateTime.
		private static void SaveData ()
		{
			int iterator = 0;
			foreach (KeyValuePair<string, DateTime> pair in dates)
			{
				PlayerPrefs.SetString (string.Concat (DateKey, iterator), pair.Key);
				PlayerPrefs.SetString (pair.Key, pair.Value.ToString ());
				iterator++;
			}

			string key = string.Concat (DateKey, iterator + 1);
			if (PlayerPrefs.HasKey (key))
			{
				PlayerPrefs.DeleteKey (key);
				PlayerPrefs.DeleteKey (PlayerPrefs.GetString (key));
			}

		}

		private static void LoadData ()
		{
			dates.Clear ();

			for (int i = 0; ; i++)
			{
				string key = string.Concat (DateKey, i);
				string savedKey = PlayerPrefs.GetString (key);

				if (PlayerPrefs.HasKey (key))
				{
					if (PlayerPrefs.HasKey (savedKey) && !dates.ContainsKey (savedKey))
					{
						DateTime date = DateTime.Parse (PlayerPrefs.GetString (savedKey), System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.AssumeLocal);
						dates.Add (savedKey, date);
					}
				}
				else
					break;
			}

			dataLoaded = true;
		}
		#endregion
	}
}