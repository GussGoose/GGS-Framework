#if UNITY_EDITOR
using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

namespace GGS_Framework
{
	[InitializeOnLoad]
	public class WorkSaver
	{
		#region Class members
		private static Vector2 windowSize = new Vector2 (140, 59);
		private static int windowOffset = 4;

		private static WorkSaverSettings settings;
		private static string dataPathInResources = "WorkSaver";
		private static string dataAssetName = "WorkSaverSettings";

		#region Save
		private const string LastSaveDateKey = "WorkSaver_LastSaveDate";
		private static UnityEvent onGlobalKeyPress = new UnityEvent ();
		#endregion

		#region Blink
		private static float lastRealtimeSinceStartup;
		private static float elapsedTime;
		#endregion
		#endregion

		#region Class accesors
		public static WorkSaverSettings Settings
		{
			get
			{
				if (settings == null)
				{
					string fullPathInResources = string.Concat (dataPathInResources, "/", dataAssetName);
					settings = Resources.Load<WorkSaverSettings> (fullPathInResources);

					if (settings == null)
					{
						string fullPath = string.Concat ("Assets/GGS_Framework/Resources/", dataPathInResources);

						if (!Directory.Exists (fullPath))
						{
							Directory.CreateDirectory (fullPath);
							settings = WorkSaverSettings.Create (string.Concat (fullPath, "/", dataAssetName, ".asset"));
						}
					}
				}

				return settings;
			}
			set
			{
				settings = value;
			}
		}

		#region Save
		private static string LastSaveDateString
		{
			get { return EditorPrefs.GetString (LastSaveDateKey); }
			set { EditorPrefs.SetString (LastSaveDateKey, value); }
		}

		private static DateTime LastSaveDate
		{
			get
			{
				if (string.IsNullOrEmpty (LastSaveDateString))
					LastSaveDate = DateTime.Now;

				return DateTime.Parse (LastSaveDateString, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.AssumeLocal);
			}
			set
			{
				LastSaveDateString = value.ToString ();
			}
		}

		private static TimeSpan LeftTimeForRequiredSave
		{
			get
			{
				TimeSpan leftTime = LastSaveDate.Add (Settings.timeBetweenSaves).Subtract (DateTime.Now);

				if (leftTime.Ticks <= 0)
					leftTime = TimeSpan.Zero;

				return leftTime;
			}
		}

		private static string SaveKeyCombo
		{
			get { return string.Format ("PRESS {0}+S", (Application.platform == RuntimePlatform.WindowsEditor) ? "CTRL+ALT" : "CTRL+CMD"); }
		}
		#endregion

		#region Blink
		private static Color StartBlinkColor
		{
			get { return (EditorGUIUtility.isProSkin) ? Settings.startBlinkColor : Settings.endBlinkColor; }
		}

		private static Color EndBlinkColor
		{
			get { return (EditorGUIUtility.isProSkin) ? Settings.endBlinkColor : Settings.startBlinkColor; }
		}

		private static float BlinkTime
		{
			get
			{
				TimeSpan elapsedTimeSinceRequired = DateTime.Now.Subtract (LastSaveDate.Add (Settings.timeBetweenSaves));
				float elapsedTimeProgressSinceRequired = (float) elapsedTimeSinceRequired.TotalSeconds / (float) ((TimeSpan) Settings.timeForReachMaxBlinkSpeed).TotalSeconds;
				float blinkSpeed = Mathf.Lerp (Settings.minBlinkSpeed, Settings.maxBlinkSpeed, elapsedTimeProgressSinceRequired);

				float deltaTime = Time.realtimeSinceStartup - lastRealtimeSinceStartup;
				lastRealtimeSinceStartup = Time.realtimeSinceStartup;

				elapsedTime += deltaTime * blinkSpeed;
				return Settings.blinkCurve.Evaluate (Mathf.Repeat (elapsedTime, 1));
			}
		}

		private static Color BlinkColor
		{
			get { return Color.Lerp (StartBlinkColor, EndBlinkColor, BlinkTime); }
		}

		private static Color InverseBlinkColor
		{
			get { return Color.Lerp (EndBlinkColor, StartBlinkColor, BlinkTime); }
		}
		#endregion
		#endregion

		#region Class implementation
		static WorkSaver ()
		{
			EditorApplication.update -= OnEditorUpdate;
			EditorApplication.update += OnEditorUpdate;
			SceneView.onSceneGUIDelegate -= OnSceneView;
			SceneView.onSceneGUIDelegate += OnSceneView;

			AddGlobalKeyPressEvent ();
			onGlobalKeyPress.AddListener (OnGlobalKeyPress);
		}

		private static void Save ()
		{
			EditorSceneManager.SaveOpenScenes ();
			AssetDatabase.SaveAssets ();

			ResetSaveDate ();
		}

		public static void ResetSaveDate ()
		{
			elapsedTime = 0;
			LastSaveDate = DateTime.Now;
		}

		private static void AddGlobalKeyPressEvent ()
		{
			FieldInfo fieldInfo = typeof (EditorApplication).GetField ("globalEventHandler", BindingFlags.Static | BindingFlags.NonPublic);
			EditorApplication.CallbackFunction value = (EditorApplication.CallbackFunction) fieldInfo.GetValue (null);
			value += onGlobalKeyPress.Invoke;
			fieldInfo.SetValue (null, value);
		}

		private static void OnGlobalKeyPress ()
		{
			Event currentEvent = Event.current;

			bool controlKey = (Application.platform == RuntimePlatform.WindowsEditor) ? currentEvent.control && currentEvent.alt : currentEvent.control && currentEvent.command;
			if (controlKey)
			{
				if (currentEvent.type == EventType.KeyDown && currentEvent.keyCode == KeyCode.S)
					Save ();
			}
		}

		private static void OnEditorUpdate ()
		{
			if (!Settings.State)
				return;

			if (LeftTimeForRequiredSave == TimeSpan.Zero)
			{
				SceneView.RepaintAll ();
				Color color = BlinkColor;
			}
		}

		private static void OnSceneView (SceneView sceneView)
		{
			if (!Settings.State)
				return;

			if (LeftTimeForRequiredSave != TimeSpan.Zero)
				return;

			Handles.BeginGUI ();

			Rect rect = new Rect (Vector2.zero, sceneView.position.size - Vector2.up * 18);
			Rect windowRect = new Rect (rect.x + windowOffset, rect.yMax - windowSize.y - windowOffset, windowSize.x, windowSize.y);

			EditorGUI.DrawRect (windowRect, BlinkColor);

			GUILayout.BeginArea (windowRect);

			ExtendedGUI.DrawLabel ("Don't lose your work!", InverseBlinkColor, FontStyle.Bold);
			ExtendedGUI.DrawLabel (SaveKeyCombo, InverseBlinkColor, FontStyle.Bold);

			GUIStyle buttonStyle = new GUIStyle ("Button");
			buttonStyle.normal.textColor = (EditorGUIUtility.isProSkin) ? Color.white : Color.black;
			if (GUILayout.Button ("Or Press Here", buttonStyle))
				Save ();

			GUILayout.EndArea ();

			Handles.EndGUI ();
		}
		#endregion
	}
}
#endif