// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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