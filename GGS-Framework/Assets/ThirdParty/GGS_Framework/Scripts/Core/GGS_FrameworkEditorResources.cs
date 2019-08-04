#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public static class GGS_FrameworkEditorResources
	{
		#region Class implementation
		public static Object LoadAsset<T> (string path)
		{
			string assetPath = string.Concat (GGS_FrameworkPaths.EditorResources, "/", path);
			return AssetDatabase.LoadAssetAtPath (assetPath, typeof (T));
		}
		#endregion
	}
} 
#endif