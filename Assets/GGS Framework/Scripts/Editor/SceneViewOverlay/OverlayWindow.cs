// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GGS_Framework.Editor
{
    public class OverlayWindow : IComparable<OverlayWindow>
    {
        #region Properties
        public Action<SceneView> OnGUICallback { get; }

        public int PrimaryOrder { get; }

        public int SecondaryOrder { get; set; }

        public Object Target { get; }

        public SceneViewOverlay.WindowDisplayOption DisplayOption { get; }

        public bool CanCollapse { get; set; }

        public bool IsExpanded { get; set; }

        public GUIContent Title { get; }
        #endregion

        #region Constructors
        public OverlayWindow (GUIContent title, int primaryOrder, Action<SceneView> onGUICallback) : this (title, primaryOrder, null, SceneViewOverlay.WindowDisplayOption.MultipleWindowsPerTarget, onGUICallback)
        {
        }

        public OverlayWindow (GUIContent title, int primaryOrder, Object target, SceneViewOverlay.WindowDisplayOption displayOption, Action<SceneView> onGUICallback)
        {
            this.Title = title;
            this.PrimaryOrder = primaryOrder;
            this.DisplayOption = displayOption;
            this.Target = target;
            this.OnGUICallback = onGUICallback;

            this.CanCollapse = true;
            this.IsExpanded = true;
        }
        #endregion

        #region Implementation
        public int CompareTo (OverlayWindow other)
        {
            int compared = other.PrimaryOrder.CompareTo (this.PrimaryOrder);

            if (compared == 0)
                compared = other.SecondaryOrder.CompareTo (this.SecondaryOrder);

            return compared;
        }
        #endregion
    }
}