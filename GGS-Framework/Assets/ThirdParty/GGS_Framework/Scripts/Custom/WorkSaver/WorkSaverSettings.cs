#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using GGS_Framework;

namespace GGS_Framework
{
	public class WorkSaverSettings : ScriptableObject
	{
		#region Class members
		private const string StateKey = "WorkSaverState";

		public AnimationCurve blinkCurve;

		public Color startBlinkColor;
		public Color endBlinkColor;

		public float minBlinkSpeed;
		public float maxBlinkSpeed;

		public TimeObject timeForReachMaxBlinkSpeed;

		public TimeObject timeBetweenSaves;
		#endregion

		#region Class accesors
		public bool State
		{
			get { return EditorPrefs.GetBool (StateKey, false); }
			set { EditorPrefs.SetBool (StateKey, value); }
		}

		public bool StateKeyExist
		{
			get { return EditorPrefs.HasKey (StateKey); }
		}
		#endregion

		#region Class implementation
		public static WorkSaverSettings Create (string path)
		{
			WorkSaverSettings data = CreateInstance<WorkSaverSettings> ();
			AssetDatabase.CreateAsset (data, path);

			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();

			return data;
		}
		#endregion
	}
}
#endif