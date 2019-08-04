#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public static partial class ExtendedGUI
	{
		#region Class implementation
		public static void BeginInspector ()
		{
			EditorGUILayout.BeginHorizontal ();
			GUILayout.Space (-9);
			EditorGUILayout.BeginVertical ();
		}

		public static void EndInspector ()
		{
			EditorGUILayout.EndVertical ();
			EditorGUILayout.EndHorizontal ();
		}
		#endregion
	}
}
#endif