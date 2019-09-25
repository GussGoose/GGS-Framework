#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public class WorkSaverSettingsEditorWindow : EditorWindow
	{
		#region Class Members
		private bool editBlinkSettings;
		#endregion

		#region Class Accesors
		private WorkSaverSettings Settings
		{
			get { return WorkSaver.Settings; }
		}
		#endregion

		#region Class Implementation
		[MenuItem ("Window/GGS Framework/Work Saver Settings")]
		public static void Open ()
		{
			WorkSaverSettingsEditorWindow window = CreateInstance<WorkSaverSettingsEditorWindow> ();
			window.titleContent = new GUIContent ("Work Saver Settings");
			window.ShowUtility ();
		}

		private void OnGUI ()
		{
			if (Settings == null)
			{
				DrawNullSettingsMessage ();
				return;
			}

			EditorGUI.BeginChangeCheck ();
			Settings.State = GUILayout.Toggle (Settings.State, "State", "Button");
			if (EditorGUI.EndChangeCheck ())
			{
				if (Settings.State)
					WorkSaver.ResetSaveDate ();

				SceneView.RepaintAll ();
			}
			EditorGUILayout.Space ();

			EditorGUI.BeginChangeCheck ();
			bool editBlinkSettingsToggle = GUILayout.Toggle (editBlinkSettings, "Edit Blink Settings", EditorStyles.miniButton);
			if (EditorGUI.EndChangeCheck ())
			{
				if (editBlinkSettingsToggle)
				{
					if (EditorUtility.DisplayDialog ("Edit Blink Settings", "Are you sure you want edit blink settings?", "Ok", "Cancel"))
						editBlinkSettings = true;
				}
				else
					editBlinkSettings = false;
			}

			if (editBlinkSettings)
				EditorGUILayout.Space ();

			EditorGUI.BeginDisabledGroup (!editBlinkSettings);

			Settings.blinkCurve = EditorGUILayout.CurveField ("Curve", Settings.blinkCurve);
			EditorGUILayout.Space ();

			Settings.startBlinkColor = EditorGUILayout.ColorField ("Start Color", Settings.startBlinkColor);
			Settings.endBlinkColor = EditorGUILayout.ColorField ("End Color", Settings.endBlinkColor);
			EditorGUILayout.Space ();

			Settings.minBlinkSpeed = EditorGUILayout.FloatField ("Min Speed", Settings.minBlinkSpeed);
			Settings.maxBlinkSpeed = EditorGUILayout.FloatField ("Max Speed", Settings.maxBlinkSpeed);

			Settings.timeForReachMaxBlinkSpeed = ExtendedGUILayout.TimeObject ("Time For Reach Max Speed", Settings.timeForReachMaxBlinkSpeed);
			EditorGUILayout.Space ();
			EditorGUI.EndDisabledGroup ();

			Settings.timeBetweenSaves = ExtendedGUILayout.TimeObject ("Time Between Saves", Settings.timeBetweenSaves);
			EditorGUILayout.Space ();

			GUI.backgroundColor = Color.green;
			if (GUILayout.Button ("Save Settings"))
				SaveSettings ();
			GUI.backgroundColor = Color.white;
		}

		private void DrawNullSettingsMessage ()
		{
			string message = string.Format ("{0} doesn't exist in {1}, press Create button to create it.", WorkSaverSettings.DataAssetName, WorkSaverSettings.DataFullPath);

			GUILayout.FlexibleSpace ();

			AdvancedLabel.Draw (new AdvancedLabel.Config (message, EditorStyles.helpBox, FontStyle.Bold, TextAnchor.MiddleCenter));

			GUI.backgroundColor = Color.green;
			if (GUILayout.Button ("Create"))
			{
				WorkSaverSettings.Create ();
				Repaint ();
			}
			GUI.backgroundColor = Color.white;

			GUILayout.FlexibleSpace ();
		}

		private void SaveSettings ()
		{
			WorkSaver.ResetSaveDate ();
			EditorUtility.SetDirty (Settings);

			AssetDatabase.SaveAssets ();
			AssetDatabase.Refresh ();
		}
		#endregion
	}
}
#endif