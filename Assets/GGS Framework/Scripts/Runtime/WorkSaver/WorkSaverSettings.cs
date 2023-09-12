// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public class WorkSaverSettings : ScriptableObject
	{
		#region Nested Classes
		public static class Defaults
		{
			#region Class Members
			private static readonly Color StartBlinkColor = Color.black;
			private static readonly Color EndBlinkColor = Color.white;

			private static readonly float MinBlinkSpeed = 1;
			private static readonly float MaxBlinkSpeed = 5;

			private static readonly TimeObject TimeForReachMaxBlinkSpeed = new TimeObject (2, TimeObjectType.Minutes);

			private static readonly TimeObject TimeBetweenSaves = new TimeObject (6, TimeObjectType.Minutes);
			#endregion

			#region Class Accesors
			private static AnimationCurve BlinkCurve
			{
				get
				{
					Keyframe[] keyframes = {
						new Keyframe
						{
							time = 0,
							value = 0,
							inTangent = -0.01690376f,
							outTangent = -0.01690376f,
							inWeight = 0,
							outWeight = 1,
							weightedMode = WeightedMode.Both
						},
						new Keyframe
						{
							time = 0.7820438f,
							value = 1,
							inTangent = 3.685174f,
							outTangent = 0.09995888f,
							inWeight = 0.03617449f,
							outWeight = 0.1638594f,
							weightedMode = WeightedMode.None
						},
						new Keyframe
						{
							time = 0.9804688f,
							value = 0.01581192f,
							inTangent = -0.04822022f,
							outTangent = -0.04822022f,
							inWeight = 0.2402737f,
							outWeight = 0,
							weightedMode = WeightedMode.None
						}
					};

					return new AnimationCurve (keyframes);
				}
			}
			#endregion

			#region Class Implementation
			public static WorkSaverSettings Apply (WorkSaverSettings settings)
			{
				settings.blinkCurve = BlinkCurve;

				settings.startBlinkColor = StartBlinkColor;
				settings.endBlinkColor = EndBlinkColor;

				settings.minBlinkSpeed = MinBlinkSpeed;
				settings.maxBlinkSpeed = MaxBlinkSpeed;

				settings.timeForReachMaxBlinkSpeed = TimeForReachMaxBlinkSpeed;
				settings.timeBetweenSaves = TimeBetweenSaves;

				return settings;
			}
			#endregion
		}
		#endregion

		#region Class Members
		private const string ResourcesFolderFullPath = "Assets/Resources";

		public const string FolderName = "WorkSaver";
		public const string DataAssetName = "WorkSaverSettings";

		public static readonly string FolderPathInResourcesFolder = FolderName;
		public static readonly string DataPathInResourcesFolder = string.Concat (FolderPathInResourcesFolder, "/", DataAssetName);

		public static readonly string FolderFullPath = string.Concat (ResourcesFolderFullPath, "/", FolderPathInResourcesFolder);
		public static readonly string DataFullPath = string.Concat (ResourcesFolderFullPath, "/", DataPathInResourcesFolder, ".asset");

		private const string StateKey = "WorkSaverState";

		private static WorkSaverSettings instance;

		public AnimationCurve blinkCurve;

		public Color startBlinkColor;
		public Color endBlinkColor;

		public float minBlinkSpeed;
		public float maxBlinkSpeed;

		public TimeObject timeForReachMaxBlinkSpeed;

		public TimeObject timeBetweenSaves;
		#endregion

		#region Class Accesors
		public static WorkSaverSettings Instance
		{
			get
			{
				if (instance == null)
					instance = Resources.Load<WorkSaverSettings> (DataPathInResourcesFolder);

				return instance;
			}
		}

		public static bool Exist
		{
			get { return Resources.Load<WorkSaverSettings> (DataPathInResourcesFolder) != null; }
		}

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

		#region Class Implementation
		public static WorkSaverSettings Create ()
		{
			if (!Directory.Exists (FolderFullPath))
				Directory.CreateDirectory (FolderFullPath);

			WorkSaverSettings data = CreateInstance<WorkSaverSettings> ();
			data = Defaults.Apply (data);

			AssetDatabase.CreateAsset (data, DataFullPath);

			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();

			return data;
		}
		#endregion
	}
}
#endif