#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GGS_Framework.Editor
{
    internal class AdvancedDropdownWindow : EditorWindow
    {
        #region Members
        private static readonly float BorderThickness = 1f;
        private static readonly float RightMargin = 13f;

        private AdvancedDropdownGUI gui = null;
        private AdvancedDropdownDataSource dataSource = null;
        private AdvancedDropdownState state = null;

        private AdvancedDropdownItem currentlyRenderedTree;

        private AdvancedDropdownItem animationTree;
        private float newAnimTarget = 0;
        private long lastTime = 0;
        private bool scrollToSelected = true;
        private float initialSelectionPosition = 0f;
        ////FIXME: looks like a bug?
        #pragma warning disable CS0649
        private Rect buttonRectScreenPos;
        private Stack<AdvancedDropdownItem> viewsStack = new Stack<AdvancedDropdownItem> ();
        private bool dirtyList = true;

        private string search = "";

        internal bool showHeader = true;
        internal bool searchable = true;
        internal bool closeOnSelection = true;
        protected bool setInitialSelectionPosition = true;

        public event Action<AdvancedDropdownWindow> WindowClosed;
        public event Action<AdvancedDropdownItem> SelectionChanged;
        #endregion

        #region Accesors
        protected AdvancedDropdownItem RenderedTreeItem
        {
            get { return currentlyRenderedTree; }
        }

        private bool HasSearch
        {
            get { return !string.IsNullOrEmpty (search); }
        }

        protected internal string SearchString
        {
            get { return search; }
            set
            {
                search = value;
                dataSource.RebuildSearch (search);
                currentlyRenderedTree = dataSource.MainTree;

                if (HasSearch)
                {
                    currentlyRenderedTree = dataSource.SearchTree;

                    if (State.GetSelectedIndex (currentlyRenderedTree) < 0)
                        State.SetSelectedIndex (currentlyRenderedTree, 0);
                }
            }
        }

        internal bool ShowHeader
        {
            get { return showHeader; }
            set { showHeader = value; }
        }

        internal bool Searchable
        {
            get { return searchable; }
            set { searchable = value; }
        }

        internal bool CloseOnSelection
        {
            get { return closeOnSelection; }
            set { closeOnSelection = value; }
        }

        protected virtual bool IsSearchFieldDisabled { get; set; }

        protected virtual bool SetInitialSelectionPosition
        {
            get { return setInitialSelectionPosition; }
        }

        protected internal AdvancedDropdownState State
        {
            get { return state; }
            set { state = value; }
        }

        protected internal AdvancedDropdownGUI Gui
        {
            get { return gui; }
            set { gui = value; }
        }

        protected internal AdvancedDropdownDataSource DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }
        #endregion

        #region Unity Callbacks
        protected virtual void OnEnable ()
        {
            dirtyList = true;
        }

        protected virtual void OnDestroy ()
        {
            // This window sets 'editingTextField = true' continuously, through EditorGUI.FocusTextInControl(),
            // for the searchfield in its AdvancedDropdownGUI so here we ensure to clean up. This fixes the issue that
            // EditorGUI.IsEditingTextField() was returning true after e.g the Add Component Menu closes
            EditorGUIUtility.editingTextField = false;
            GUIUtility.keyboardControl = 0;
        }

        internal void OnGUI ()
        {
            GUI.Label (new Rect (0, 0, position.width, position.height), GUIContent.none, Styles.background);

            if (dirtyList)
                OnDirtyList ();

            HandleKeyboard ();
            if (Searchable)
                OnGUISearch ();

            if (newAnimTarget != 0 && Event.current.type == EventType.Layout)
            {
                long now = DateTime.Now.Ticks;
                float deltaTime = (now - lastTime) / (float) TimeSpan.TicksPerSecond;
                lastTime = now;

                newAnimTarget = Mathf.MoveTowards (newAnimTarget, 0, deltaTime * 4);

                if (newAnimTarget == 0)
                    animationTree = null;

                Repaint ();
            }

            float anim = newAnimTarget;
            // Smooth the animation
            anim = Mathf.Floor (anim) + Mathf.SmoothStep (0, 1, Mathf.Repeat (anim, 1));

            if (anim == 0)
                DrawDropdown (0, currentlyRenderedTree);
            else if (anim < 0)
            {
                // Go to parent
                // m_NewAnimTarget goes -1 -> 0
                DrawDropdown (anim, currentlyRenderedTree);
                DrawDropdown (anim + 1, animationTree);
            }
            else // > 0
            {
                // Go to child
                // m_NewAnimTarget 1 -> 0
                DrawDropdown (anim - 1, animationTree);
                DrawDropdown (anim, currentlyRenderedTree);
            }
        }
        #endregion

        #region Implementation
        public static T CreateAndInit<T> (Rect rect, AdvancedDropdownState state) where T : AdvancedDropdownWindow
        {
            T instance = CreateInstance<T> ();
            instance.state = state;
            instance.Init (rect);

            return instance;
        }

        public void Init (Rect buttonRect)
        {
            Vector2 screenPoint = GUIUtility.GUIToScreenPoint (new Vector2 (buttonRect.x, buttonRect.y));
            buttonRectScreenPos.x = screenPoint.x;
            buttonRectScreenPos.y = screenPoint.y;

            if (state == null)
                state = new AdvancedDropdownState ();
            if (dataSource == null)
                dataSource = new MultiLevelDataSource ();
            if (gui == null)
                gui = new AdvancedDropdownGUI ();

            gui.State = state;
            gui.Init ();

            // Has to be done before calling Show / ShowWithMode
            screenPoint = GUIUtility.GUIToScreenPoint (new Vector2 (buttonRect.x, buttonRect.y));
            buttonRect.x = screenPoint.x;
            buttonRect.y = screenPoint.y;

            Vector2 requiredDropdownSize;
            OnDirtyList ();
            currentlyRenderedTree = HasSearch ? dataSource.SearchTree : dataSource.MainTree;
            ShowAsDropDown (buttonRect, CalculateWindowSize (buttonRectScreenPos, out requiredDropdownSize));

            // If the dropdown is as height as the screen height, give it some margin
            if (position.height < requiredDropdownSize.y)
            {
                Rect pos = position;
                pos.y += 5;
                pos.height -= 10;
                position = pos;
            }

            if (SetInitialSelectionPosition)
                initialSelectionPosition = gui.GetSelectionHeight (dataSource, buttonRect);

            wantsMouseMove = true;
            SetSelectionFromState ();
        }

        private void SetSelectionFromState ()
        {
            int selectedIndex = state.GetSelectedIndex (currentlyRenderedTree);
            while (selectedIndex >= 0)
            {
                AdvancedDropdownItem child = state.GetSelectedChild (currentlyRenderedTree);
                if (child == null)
                    break;

                selectedIndex = state.GetSelectedIndex (child);
                if (selectedIndex < 0)
                    break;

                viewsStack.Push (currentlyRenderedTree);
                currentlyRenderedTree = child;
            }
        }

        protected virtual Vector2 CalculateWindowSize (Rect buttonRect, out Vector2 requiredDropdownSize)
        {
            requiredDropdownSize = gui.CalculateContentSize (dataSource);
            // Add 1 pixel for each border
            requiredDropdownSize.x += BorderThickness * 2;
            requiredDropdownSize.y += BorderThickness * 2;
            requiredDropdownSize.x += RightMargin;

            requiredDropdownSize.y += gui.SearchHeight;

            if (ShowHeader)
                requiredDropdownSize.y += gui.HeaderHeight;

            requiredDropdownSize.y = Mathf.Clamp (requiredDropdownSize.y, minSize.y, maxSize.y);

            Rect adjustedButtonRect = buttonRect;
            adjustedButtonRect.y = 0;
            adjustedButtonRect.height = requiredDropdownSize.y;

            // Stretch to the width of the button
            if (requiredDropdownSize.x < buttonRect.width)
                requiredDropdownSize.x = buttonRect.width;

            // Apply minimum size
            if (requiredDropdownSize.x < minSize.x)
                requiredDropdownSize.x = minSize.x;

            if (requiredDropdownSize.y < minSize.y)
                requiredDropdownSize.y = minSize.y;

            return requiredDropdownSize;
        }

        private void OnDirtyList ()
        {
            dirtyList = false;
            dataSource.ReloadData ();
            if (HasSearch)
            {
                dataSource.RebuildSearch (SearchString);
                if (State.GetSelectedIndex (currentlyRenderedTree) < 0)
                    State.SetSelectedIndex (currentlyRenderedTree, 0);
            }
        }

        private void OnGUISearch ()
        {
            gui.DrawSearchField (IsSearchFieldDisabled, search, (newSearch) =>
            {
                SearchString = newSearch;
            });
        }

        private void HandleKeyboard ()
        {
            Event evt = Event.current;
            if (evt.type == EventType.KeyDown)
            {
                // Special handling when in new script panel
                if (SpecialKeyboardHandling (evt))
                    return;

                // Always do these
                if (evt.keyCode == KeyCode.DownArrow)
                {
                    state.MoveDownSelection (currentlyRenderedTree);
                    scrollToSelected = true;
                    evt.Use ();
                }

                if (evt.keyCode == KeyCode.UpArrow)
                {
                    state.MoveUpSelection (currentlyRenderedTree);
                    scrollToSelected = true;
                    evt.Use ();
                }

                if (evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.KeypadEnter)
                {
                    AdvancedDropdownItem selected = state.GetSelectedChild (currentlyRenderedTree);
                    if (selected != null)
                    {
                        if (selected.Children.Any ())
                            GoToChild ();
                        else
                        {
                            if (SelectionChanged != null)
                                SelectionChanged (state.GetSelectedChild (currentlyRenderedTree));

                            if (CloseOnSelection)
                                CloseWindow ();
                        }
                    }

                    evt.Use ();
                }

                // Do these if we're not in search mode
                if (!HasSearch)
                {
                    if (evt.keyCode == KeyCode.LeftArrow || evt.keyCode == KeyCode.Backspace)
                    {
                        GoToParent ();
                        evt.Use ();
                    }

                    if (evt.keyCode == KeyCode.RightArrow)
                    {
                        int idx = state.GetSelectedIndex (currentlyRenderedTree);
                        if (idx > -1 && currentlyRenderedTree.Children.ElementAt (idx).Children.Any ())
                            GoToChild ();

                        evt.Use ();
                    }

                    if (evt.keyCode == KeyCode.Escape)
                    {
                        Close ();
                        evt.Use ();
                    }
                }
            }
        }

        private void CloseWindow ()
        {
            if (WindowClosed != null)
                WindowClosed (this);

            Close ();
        }

        internal AdvancedDropdownItem GetSelectedItem ()
        {
            return state.GetSelectedChild (currentlyRenderedTree);
        }

        protected virtual bool SpecialKeyboardHandling (Event evt)
        {
            return false;
        }

        private void DrawDropdown (float anim, AdvancedDropdownItem group)
        {
            // Start of animated area (the part that moves left and right)
            Rect areaPosition = new Rect (0, 0, position.width, position.height);
            // Adjust to the frame
            areaPosition.x += BorderThickness;
            areaPosition.y += BorderThickness;
            areaPosition.height -= BorderThickness * 2;
            areaPosition.width -= BorderThickness * 2;

            GUILayout.BeginArea (gui.GetAnimRect (areaPosition, anim));
            // Header
            if (ShowHeader)
                gui.DrawHeader (group, GoToParent, viewsStack.Count > 0);

            DrawList (group);
            GUILayout.EndArea ();
        }

        private void DrawList (AdvancedDropdownItem item)
        {
            // Start of scroll view list
            state.SetScrollState (item, GUILayout.BeginScrollView (state.GetScrollState (item), GUIStyle.none, GUI.skin.verticalScrollbar));
            EditorGUIUtility.SetIconSize (gui.IconSize);
            Rect selectedRect = new Rect ();
            for (int i = 0; i < item.Children.Count (); i++)
            {
                AdvancedDropdownItem child = item.Children.ElementAt (i);
                bool selected = state.GetSelectedIndex (item) == i;

                if (child.IsSeparator ())
                    gui.DrawLineSeparator ();
                else
                    gui.DrawItem (child, child.Name, child.Icon, child.Enabled, child.Children.Any (), selected, HasSearch);

                Rect r = GUILayoutUtility.GetLastRect ();
                if (selected)
                    selectedRect = r;

                // Skip input handling for the tree used for animation
                if (item != currentlyRenderedTree)
                    continue;

                // Select the element the mouse cursor is over.
                // Only do it on mouse move - keyboard controls are allowed to overwrite this until the next time the mouse moves.
                if (Event.current.type == EventType.MouseMove || Event.current.type == EventType.MouseDrag)
                {
                    if (!selected && r.Contains (Event.current.mousePosition))
                    {
                        state.SetSelectedIndex (item, i);
                        Event.current.Use ();
                    }
                }

                if (Event.current.type == EventType.MouseUp && r.Contains (Event.current.mousePosition))
                {
                    state.SetSelectedIndex (item, i);
                    AdvancedDropdownItem selectedChild = state.GetSelectedChild (item);
                    if (selectedChild.Children.Any ())
                        GoToChild ();
                    else
                    {
                        if (!selectedChild.IsSeparator () && SelectionChanged != null)
                            SelectionChanged (selectedChild);

                        if (CloseOnSelection)
                        {
                            CloseWindow ();
                            GUIUtility.ExitGUI ();
                        }
                    }

                    Event.current.Use ();
                }
            }

            EditorGUIUtility.SetIconSize (Vector2.zero);
            GUILayout.EndScrollView ();

            // Scroll to selected on windows creation
            if (scrollToSelected && initialSelectionPosition != 0)
            {
                float diffOfPopupAboveTheButton = buttonRectScreenPos.y - position.y;
                diffOfPopupAboveTheButton -= gui.SearchHeight + gui.HeaderHeight;
                state.SetScrollState (item, new Vector2 (0, initialSelectionPosition - diffOfPopupAboveTheButton));
                scrollToSelected = false;
                initialSelectionPosition = 0;
            }
            // Scroll to show selected
            else if (scrollToSelected && Event.current.type == EventType.Repaint)
            {
                scrollToSelected = false;
                Rect scrollRect = GUILayoutUtility.GetLastRect ();
                if (selectedRect.yMax - scrollRect.height > state.GetScrollState (item).y)
                {
                    state.SetScrollState (item, new Vector2 (0, selectedRect.yMax - scrollRect.height));
                    Repaint ();
                }

                if (selectedRect.y < state.GetScrollState (item).y)
                {
                    state.SetScrollState (item, new Vector2 (0, selectedRect.y));
                    Repaint ();
                }
            }
        }

        protected void GoToParent ()
        {
            if (viewsStack.Count == 0)
                return;

            lastTime = DateTime.Now.Ticks;
            if (newAnimTarget > 0)
                newAnimTarget = -1 + newAnimTarget;
            else
                newAnimTarget = -1;

            animationTree = currentlyRenderedTree;
            currentlyRenderedTree = viewsStack.Pop ();
        }

        private void GoToChild ()
        {
            viewsStack.Push (currentlyRenderedTree);
            lastTime = DateTime.Now.Ticks;

            if (newAnimTarget < 0)
                newAnimTarget = 1 + newAnimTarget;
            else
                newAnimTarget = 1;

            animationTree = currentlyRenderedTree;
            currentlyRenderedTree = state.GetSelectedChild (currentlyRenderedTree);
        }

        [DidReloadScripts]
        private static void OnScriptReload ()
        {
            CloseAllOpenWindows<AdvancedDropdownWindow> ();
        }

        protected static void CloseAllOpenWindows<T> ()
        {
            Object[] windows = Resources.FindObjectsOfTypeAll (typeof(T));
            foreach (Object window in windows)
            {
                try
                {
                    ((EditorWindow) window).Close ();
                }
                catch
                {
                    DestroyImmediate (window);
                }
            }
        }
        #endregion

        #region Nested Classes
        private static class Styles
        {
            #region Members
            public static GUIStyle background = "grey_border";
            public static GUIStyle previewHeader = new GUIStyle (EditorStyles.label);
            public static GUIStyle previewText = new GUIStyle (EditorStyles.wordWrappedLabel);
            #endregion

            #region Constructors
            static Styles ()
            {
                previewText.padding.left += 3;
                previewText.padding.right += 3;
                previewHeader.padding.left += 3 - 2;
                previewHeader.padding.right += 3;
                previewHeader.padding.top += 3;
                previewHeader.padding.bottom += 2;
            }
            #endregion
        }
        #endregion
    }
}
#endif // UNITY_EDITOR