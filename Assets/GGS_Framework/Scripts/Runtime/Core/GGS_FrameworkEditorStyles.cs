#if UNITY_EDITOR
using UnityEngine;

namespace GGS_Framework.Editor
{
    public static class GGS_FrameworkEditorStyles
    {
        #region Accesors
        public static readonly GUISkin GeneralSkin = (GUISkin) GGS_FrameworkEditorResources.LoadAsset<GUISkin> ("General/General_GUISkin.guiskin");
        #endregion
    }
}
#endif