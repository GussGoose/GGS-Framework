// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;

namespace GGS_Framework
{
	public class AdvancedPlayerPrefs
	{
		#region Class Implementation
		public static void SetBool (string key, bool value)
		{
			PlayerPrefs.SetInt (key, value ? 1 : 0);
		}

		public static bool GetBool (string key, bool defaultValue)
		{
			return PlayerPrefs.GetInt (key, defaultValue ? 1 : 0) == 1 ? true : false;
		}

		public static bool GetBool (string key)
		{
			return PlayerPrefs.GetInt (key) == 1 ? true : false;
		}
		#endregion
	}
}