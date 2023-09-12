// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static class GGS_FrameworkEditorResources
    {
        #region Class Implementation
        public static Object LoadAsset<T> (string path) where T : class
        {
            string assetPath = $"{GGS_FrameworkEditorPaths.EditorResourcesDirectoryPath}/{path}";
            return AssetDatabase.LoadAssetAtPath (assetPath, typeof (T));
        }
        #endregion
    }
}
#endif