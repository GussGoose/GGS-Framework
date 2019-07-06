#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UtilityFramework;

public class WorkSaverSettings : ScriptableObject
{
	#region Class members
	public bool state;

	public AnimationCurve blinkCurve;

	public Color startBlinkColor;
	public Color endBlinkColor;

	public float minBlinkSpeed;
	public float maxBlinkSpeed;

	public TimeObject timeForReachMaxBlinkSpeed;

	public TimeObject timeBetweenSaves;
	#endregion

	#region Class accesors
	#endregion

	#region Class overrides
	#endregion

	#region Class implementation
	public WorkSaverSettings (WorkSaverSettings settings)
	{
		state = settings.state;
		blinkCurve = new AnimationCurve (settings.blinkCurve.keys);

		startBlinkColor = settings.startBlinkColor;
		endBlinkColor = settings.endBlinkColor;

		minBlinkSpeed = settings.minBlinkSpeed;
		maxBlinkSpeed = settings.maxBlinkSpeed;

		timeForReachMaxBlinkSpeed = settings.timeForReachMaxBlinkSpeed;

		timeBetweenSaves = settings.timeBetweenSaves;
	}

	public static WorkSaverSettings Create (string path)
	{
#if UNITY_EDITOR
		WorkSaverSettings data = CreateInstance<WorkSaverSettings> ();
		AssetDatabase.CreateAsset (data, path);

		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh ();

		return data; 
#endif
	}
	#endregion

	#region Interface implementation
	#endregion
}