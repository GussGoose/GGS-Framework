using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GGS_Framework.Editor
{
    public class SceneViewOverlay
    {
        #region Members
        public static event Action<SceneView> duringSceneGUI;

        private static List<OverlayWindow> windows;

        private Rect lastRect;
        private GUIStyle titleStyle;
        #endregion

        #region Constructors
        public SceneViewOverlay ()
        {
            if (windows != null)
                return;

            windows = new List<OverlayWindow> ();
        }
        #endregion

        #region Implementation
        public void Begin (SceneView sceneView)
        {
            if (Event.current.type == EventType.Layout)
                windows.Clear ();

            if (titleStyle == null)
            {
                titleStyle = new GUIStyle (GUI.skin.window);
                titleStyle.padding.top = titleStyle.padding.bottom;
            }
        }

        public void End (SceneView sceneView)
        {
            duringSceneGUI?.Invoke (sceneView);

            windows.Sort ();

            if (windows.Count > 0 && Event.current.type != EventType.Used)
            {
                Handles.BeginGUI ();

                Rect windowRect = GUIUtility.ScreenToGUIRect (sceneView.position);
                windowRect.y += 19;

                GUILayout.BeginArea (windowRect);
                WindowTrampoline (sceneView);
                GUILayout.EndArea ();

                Handles.EndGUI ();
            }
        }

        private void WindowTrampoline (SceneView sceneView)
        {
            GUILayout.BeginHorizontal (Styles.SceneViewOverlayTransparentBackground);
            GUILayout.BeginVertical ();
            GUILayout.FlexibleSpace ();
            EditorGUILayout.BeginVertical (GUILayout.MinWidth (210));

            float spacingOffset = -9;
            foreach (OverlayWindow window in windows)
            {
                GUILayout.Space (9 + spacingOffset);
                spacingOffset = 0;

                ResetGUIState ();
                if (window.CanCollapse)
                {
                    GUILayout.BeginVertical (titleStyle);
                    window.IsExpanded = EditorGUILayout.Foldout (window.IsExpanded, window.Title, true);

                    if (window.IsExpanded)
                        window.OnGUICallback (sceneView);

                    GUILayout.EndVertical ();
                }
                else
                {
                    GUILayout.BeginVertical (window.Title, GUI.skin.window);
                    window.OnGUICallback (sceneView);
                    GUILayout.EndVertical ();
                }
            }

            GUILayout.EndVertical ();
            HandleEvent (GUILayoutUtility.GetLastRect ());
            GUILayout.EndVertical ();
            GUILayout.FlexibleSpace ();
            GUILayout.EndHorizontal ();
        }

        private static void ResetGUIState ()
        {
            GUI.skin = (GUISkin) null;
            GUI.backgroundColor = GUI.contentColor = Color.white;
            GUI.color = Color.white;
            GUI.enabled = true;
            GUI.changed = false;
            EditorGUI.indentLevel = 0;
            EditorGUIUtility.fieldWidth = 0.0f;
            EditorGUIUtility.labelWidth = 0.0f;
            EditorGUIUtility.hierarchyMode = false;
            EditorGUIUtility.wideMode = false;

            #region Unity Internal Functions
            // EditorGUI.ClearStacks();
            // EditorGUIUtility.currentViewWidth = -1f;
            // EditorGUIUtility.SetBoldDefaultFont(false);
            // EditorGUIUtility.UnlockContextWidth();
            // EditorGUIUtility.comparisonViewMode = EditorGUIUtility.ComparisonViewMode.None;
            // EditorGUIUtility.leftMarginCoord = 0.0f;
            // ScriptAttributeUtility.propertyHandlerCache = (PropertyHandlerCache) null;
            #endregion
        }

        private void HandleEvent (Rect position)
        {
            if (Event.current.type == EventType.Layout)
                EditorGUI.DrawRect (position, Color.red);

            EditorGUIUtility.AddCursorRect (position, MouseCursor.MoveArrow);
            int controlId = GUIUtility.GetControlID (nameof(SceneViewOverlay).GetHashCode (), FocusType.Passive, position);
            switch (Event.current.GetTypeForControl (controlId))
            {
                case EventType.MouseDown:
                    if (!position.Contains (Event.current.mousePosition))
                        break;
                    GUIUtility.hotControl = controlId;
                    Event.current.Use ();
                    break;
                case EventType.MouseUp:
                    if (GUIUtility.hotControl != controlId)
                        break;
                    GUIUtility.hotControl = 0;
                    Event.current.Use ();
                    break;
                case EventType.MouseDrag:
                    if (GUIUtility.hotControl != controlId)
                        break;
                    Event.current.Use ();
                    break;
                case EventType.ScrollWheel:
                    if (!position.Contains (Event.current.mousePosition))
                        break;
                    Event.current.Use ();
                    break;
                case EventType.Repaint:
                    lastRect = position;
                    break;
                case EventType.Layout:
                    HandleUtility.AddControl (controlId, lastRect.Contains (Event.current.mousePosition) ? 0.0f : float.PositiveInfinity);
                    break;
            }
        }

        public static void Window (GUIContent title, int order, Action<SceneView> onGUICallback)
        {
            Window (title, order, null, WindowDisplayOption.MultipleWindowsPerTarget, onGUICallback);
        }

        public static void Window (GUIContent title, int order, Object target, WindowDisplayOption option, Action<SceneView> onGUICallback)
        {
            if (Event.current.type != EventType.Layout)
                return;

            foreach (OverlayWindow currentWindow in windows)
            {
                if (option == WindowDisplayOption.OneWindowPerTarget && currentWindow.Target == target && target != null)
                    return;
            }

            OverlayWindow overlayWindow = new OverlayWindow (title, order, target, option, onGUICallback)
            {
                SecondaryOrder = windows.Count,
                CanCollapse = false
            };

            windows.Add (overlayWindow);
        }

        public static void ShowWindow (OverlayWindow window)
        {
            if (Event.current.type != EventType.Layout)
                return;

            foreach (OverlayWindow currentWindow in windows)
            {
                if (window.DisplayOption == WindowDisplayOption.OneWindowPerTarget && currentWindow.Target == window.Target && window.Target != null)
                    return;
            }

            window.SecondaryOrder = windows.Count;
            windows.Add (window);
        }
        #endregion

        #region Nested Classes
        [InitializeOnLoad]
        public class Initializer
        {
            #region Members
            private static readonly SceneViewOverlay sceneViewOverlay;
            #endregion

            #region Constructors
            static Initializer ()
            {
                if (sceneViewOverlay == null)
                    sceneViewOverlay = new SceneViewOverlay ();

                SceneView.beforeSceneGui += sceneViewOverlay.Begin;
                SceneView.duringSceneGui += sceneViewOverlay.End;
            }
            #endregion
        }
        
        public enum WindowDisplayOption
        {
            MultipleWindowsPerTarget,
            OneWindowPerTarget
        }

        private static class Styles
        {
            public static readonly GUIStyle SceneViewOverlayTransparentBackground = (GUIStyle) "SceneViewOverlayTransparentBackground";
        }
        #endregion
    }
}