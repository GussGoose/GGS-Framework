#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static class GGS_FrameworkEditorResources
    {
        #region Class Implementation
        public static Object LoadAsset<T> (string path)
        {
            string assetPath = $"{GGS_FrameworkEditorPaths.EditorResourcesFolderFullPath}/{path}";
            return AssetDatabase.LoadAssetAtPath (assetPath, typeof (T));
        }
        #endregion
    }
}
#endif