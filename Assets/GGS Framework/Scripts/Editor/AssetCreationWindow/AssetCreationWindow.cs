using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GGS_Framework.Editor
{
    public abstract class AssetCreationWindow : EditorWindow
    {
        #region Members
        private static bool initialized;

        private Rect originalWindowRect;
        private Rect lastRect;
        private Vector2 scrollPosition;

        [SerializeField] private string creationInfoJson;
        private AssetCreationInfo creationInfoScriptableObject;

        private SerializedObject creationInfoSerializedObject;
        private AssetCreationInfoEditor creationInfoEditor;

        [SerializeField] private AssetCreationMode creationMode;
        [SerializeField] private int batchSize;
        [SerializeField] private bool canEditIDs = true;
        [SerializeField] private IntRange assetIDRange = new IntRange ();
        [SerializeField] private List<AssetCreationArgs> singleCreationInfo = new List<AssetCreationArgs> () {new AssetCreationArgs (String.Empty, 0)};
        #endregion

        #region Properties
        protected abstract string WindowGUID { get; }

        private string StoredWindowJson
        {
            get { return SessionState.GetString ($"AssetCreationWindow_{WindowGUID}_Json", string.Empty); }
            set { SessionState.SetString ($"AssetCreationWindow_{WindowGUID}_Json", value); }
        }

        public abstract ScriptableObject CreationInfoObjectContainer { get; }

        public SerializedObject CreationInfoSerializedObject { get { return creationInfoSerializedObject; } }

        public AssetCreationInfo CreationInfo { get { return creationInfoScriptableObject; } }

        public AssetCreationMode CreationMode { get { return creationMode; } }

        public List<AssetCreationArgs> SingleCreationInfo { get { return singleCreationInfo; } }

        public Action<AssetCreationWindow> AssetCreated { get; set; }

        public bool WasAssetSuccesfullyCreated { get; set; }
        #endregion

        #region Unity Callbacks
        private void OnGUI ()
        {
            if (!initialized)
            {
                DestroyPostRecompilingWindow ();
                return;
            }

            VerifyCreationInfoType ();

            if (HandleKeyboardEvents ())
                return;

            EditorGUIUtility.labelWidth = GetGUIWidth ();
            DrawGUI ();
            EditorGUIUtility.labelWidth = 0;

            VerifyWindowSize ();
        }

        private void OnDestroy ()
        {
            if (initialized)
                WriteWindowJson ();
        }
        #endregion

        #region Implementation
        public static void Open<WindowType> (Vector2 windowPosition, AssetCreationWindowArgs windowArgs, Action<WindowType> assetCreated) where WindowType : AssetCreationWindow
        {
            CloseAllOpenWindows ();

            AssetCreationWindow window = CreateInstance<WindowType> ();

            OverwriteWindowFromStoredJson (window);

            window.position = new Rect (windowPosition, new Vector2 (270, 200));
            window.ShowPopup ();
            window.Focus ();

            window.AssetCreated += creationWindow => assetCreated.Invoke ((WindowType) creationWindow);
            window.Initialize (windowArgs);
        }

        private static void OverwriteWindowFromStoredJson (AssetCreationWindow windowToOverwrite)
        {
            if (!String.IsNullOrEmpty (windowToOverwrite.StoredWindowJson))
                JsonUtility.FromJsonOverwrite (windowToOverwrite.StoredWindowJson, windowToOverwrite);
        }

        protected virtual void Initialize (AssetCreationWindowArgs windowArgs)
        {
            initialized = true;

            Initializing (windowArgs);

            originalWindowRect = position;

            LoadCreationInfoObject ();

            int nextHighestId = GetCurrentHighestID () + 1;
            if (creationMode == AssetCreationMode.Single)
                singleCreationInfo[0].ID = nextHighestId;
        }

        protected virtual void Initializing (AssetCreationWindowArgs windowArgs) { }

        protected abstract int GetCurrentHighestID ();

        protected virtual int GetHighestPossibleID ()
        {
            return int.MaxValue;
        }

        protected void LoadCreationInfoObject ()
        {
            if (creationInfoScriptableObject == null)
            {
                AssetCreationInfo foundCreationInfo = FindCreationInfoSubAsset ();

                if (foundCreationInfo == null)
                {
                    foundCreationInfo = (AssetCreationInfo) CreateInstance (GetCreationInfoType ());
                    foundCreationInfo.hideFlags = HideFlags.HideInHierarchy;

                    AssetDatabase.AddObjectToAsset (foundCreationInfo, CreationInfoObjectContainer);
                    AssetDatabase.ImportAsset (AssetDatabase.GetAssetPath (CreationInfoObjectContainer));

                    creationInfoScriptableObject = foundCreationInfo;
                }
                else
                    creationInfoScriptableObject = foundCreationInfo;
            }

            if (!string.IsNullOrEmpty (creationInfoJson))
                JsonUtility.FromJsonOverwrite (creationInfoJson, creationInfoScriptableObject);

            if (creationInfoSerializedObject == null)
            {
                creationInfoSerializedObject = new SerializedObject (creationInfoScriptableObject);
                creationInfoEditor = InitializeCreationInfoView (creationInfoSerializedObject);
            }
        }

        protected virtual AssetCreationInfoEditor InitializeCreationInfoView (SerializedObject serializedObject)
        {
            return new AssetCreationInfoEditor (serializedObject);
        }

        private AssetCreationInfo FindCreationInfoSubAsset ()
        {
            Object[] containerSubAssets = AssetDatabase.LoadAllAssetsAtPath (AssetDatabase.GetAssetPath (CreationInfoObjectContainer));
            AssetCreationInfo foundCreationInfo = null;

            if (containerSubAssets.Length != 0)
                foundCreationInfo = containerSubAssets.FirstOrDefault (asset => asset.GetType ().BaseType == typeof(AssetCreationInfo)) as AssetCreationInfo;

            return foundCreationInfo;
        }

        protected abstract Type GetCreationInfoType ();

        private void VerifyWindowSize ()
        {
            if (Event.current.type == EventType.Repaint)
                lastRect = GUILayoutUtility.GetLastRect ();

            if (Math.Abs (maxSize.y - lastRect.y + 23) > 1f)
            {
                maxSize = new Vector2 (270, lastRect.y + 23);
                minSize = maxSize;
            }
        }

        private bool HandleKeyboardEvents ()
        {
            if (Event.current.type == EventType.KeyDown)
            {
                if (Event.current.keyCode == KeyCode.Escape)
                {
                    CloseWindow ();
                    return true;
                }

                if (Event.current.keyCode == KeyCode.Return)
                {
                    CreateAsset ();
                    return true;
                }
            }

            return false;
        }

        private void VerifyCreationInfoType ()
        {
            if (creationInfoScriptableObject.GetType () != GetCreationInfoType ())
            {
                GGS_FrameworkEditorUtility.ChangeScriptableObjectScriptType (creationInfoSerializedObject, GetCreationInfoType ());
                creationInfoScriptableObject = (AssetCreationInfo) creationInfoSerializedObject.targetObject;
            }
        }

        protected virtual float GetGUIWidth ()
        {
            return 60;
        }

        protected virtual void DrawGUI ()
        {
            DrawCreationInfoBase ();
            DrawCreationMode ();
            DrawCreationInfoList ();

            EditorGUILayout.BeginHorizontal ();
            GUI.backgroundColor = Color.green;

            if (GUILayout.Button ("Create"))
                CreateAsset ();

            GUI.backgroundColor = Color.red;

            if (GUILayout.Button ("Cancel", GUILayout.Width (50)))
                CloseWindow ();

            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndHorizontal ();
        }

        private void DrawCreationInfoBase ()
        {
            EditorGUILayout.BeginVertical (EditorStyles.helpBox);

            float lastLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = creationInfoEditor.GetLabelWidth ();
            EditorGUIUtility.wideMode = true;

            creationInfoEditor.OnGUI ();

            EditorGUIUtility.wideMode = false;
            EditorGUIUtility.labelWidth = lastLabelWidth;
            EditorGUILayout.EndVertical ();
        }

        private void DrawCreationMode ()
        {
            EditorGUI.BeginChangeCheck ();
            GUIStyle popupStyle = new GUIStyle (EditorStyles.popup) {alignment = TextAnchor.MiddleCenter};
            creationMode = (AssetCreationMode) EditorGUILayout.Popup (string.Empty, (int) creationMode, new[] {"Mode : Single", "Mode : Batch"}, popupStyle);
            if (EditorGUI.EndChangeCheck ())
            {
                if (creationMode == AssetCreationMode.Single)
                {
                    singleCreationInfo = new List<AssetCreationArgs> {new AssetCreationArgs ()};
                    assetIDRange.start = assetIDRange.end = 1;
                    canEditIDs = true;
                }
            }

            if (creationMode == AssetCreationMode.Batch)
            {
                EditorGUILayout.BeginHorizontal ();
                canEditIDs = GUILayout.Toggle (canEditIDs, "Editable IDs", EditorStyles.miniButton, GUILayout.Width (65));

                if (canEditIDs)
                {
                    batchSize = EditorGUILayout.IntField ("Batch Size", batchSize);
                    batchSize = Mathf.Clamp (batchSize, 2, int.MaxValue);
                }
                else
                {
                    assetIDRange = ExtendedGUILayout.IntRange ("ID Range", assetIDRange);
                    assetIDRange.start = Mathf.Clamp (assetIDRange.start, 1, GetHighestPossibleID ());
                    assetIDRange.end = Mathf.Clamp (assetIDRange.end, assetIDRange.start + 1, GetHighestPossibleID ());
                }

                EditorGUILayout.EndHorizontal ();
            }
        }

        private void DrawCreationInfoList ()
        {
            // Check if the list needs to add elements or remove
            int totalCount = (creationMode == AssetCreationMode.Batch && canEditIDs) ? batchSize : assetIDRange.Lenght + 1;
            if (singleCreationInfo.Count != totalCount)
            {
                while (singleCreationInfo.Count != totalCount)
                {
                    if (singleCreationInfo.Count > totalCount)
                        singleCreationInfo.RemoveAt (singleCreationInfo.Count - 1);
                    else
                        singleCreationInfo.Add (new AssetCreationArgs ());
                }
            }

            EditorGUIUtility.labelWidth = 20;
            if (creationMode == AssetCreationMode.Batch)
                scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition, EditorStyles.helpBox, GUILayout.Height (270));

            for (int i = 0; i < singleCreationInfo.Count; i++)
            {
                AssetCreationArgs info = singleCreationInfo[i];
                EditorGUILayout.BeginHorizontal ();

                if (canEditIDs)
                    info.ID = Mathf.Clamp (EditorGUILayout.IntField ("ID", info.ID, GUILayout.Width (60)), 1, GetHighestPossibleID ());
                else
                {
                    info.ID = assetIDRange.start + i;
                    EditorGUILayout.LabelField ($"ID ({info.ID.ToString ()})", GUILayout.Width (60));
                }

                info.Name = EditorGUILayout.TextField (GUIContent.none, info.Name);

                EditorGUILayout.EndHorizontal ();
                singleCreationInfo[i] = info;
            }

            if (creationMode == AssetCreationMode.Batch)
                EditorGUILayout.EndScrollView ();

            EditorGUIUtility.labelWidth = 0;
        }

        private void CreateAsset ()
        {
            if (creationMode == AssetCreationMode.Single)
                SingleCreationInfo[0].CreationInfo = CreationInfo;
            else
            {
                foreach (AssetCreationArgs assetCreationArgs in SingleCreationInfo)
                    assetCreationArgs.CreationInfo = CreationInfo;
            }

            WasAssetSuccesfullyCreated = DoCreateAsset ();
            AssetCreated?.Invoke (this);

            if (WasAssetSuccesfullyCreated)
                CloseWindow ();
        }

        protected abstract bool DoCreateAsset ();

        private static void CloseAllOpenWindows ()
        {
            Object[] openWindows = Resources.FindObjectsOfTypeAll (typeof(AssetCreationWindow));
            foreach (Object windowObject in openWindows)
            {
                EditorWindow window = (EditorWindow) windowObject;

                if (window != null)
                    window.Close ();
            }
        }

        protected void CloseWindow ()
        {
            AssetDatabase.RemoveObjectFromAsset (creationInfoScriptableObject);

            OnWindowClose ();
            Close ();
        }

        protected virtual void OnWindowClose () { }

        private void DestroyPostRecompilingWindow ()
        {
            AssetCreationInfo creationInfoSubAsset = FindCreationInfoSubAsset ();

            if (creationInfoSubAsset != null)
                AssetDatabase.RemoveObjectFromAsset (creationInfoSubAsset);

            Close ();
        }

        private void WriteWindowJson ()
        {
            creationInfoJson = JsonUtility.ToJson (CreationInfoSerializedObject.targetObject);
            StoredWindowJson = JsonUtility.ToJson (this);
        }
        #endregion

        #region Nested Classes
        public enum AssetCreationMode
        {
            Single,
            Batch
        }
        #endregion
    }
}