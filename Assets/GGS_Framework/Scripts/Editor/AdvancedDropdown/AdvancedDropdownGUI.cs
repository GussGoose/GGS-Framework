#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace GGS_Framework.Editor
{
    internal class AdvancedDropdownGUI
    {
        #region Members
        //This should ideally match line height
        private Vector2 iconSize = new Vector2 (13, 13);

        internal Rect searchRect;
        internal Rect headerRect;
        private bool focusSet;

        private SearchField searchField = new SearchField ();
        #endregion

        #region Accesors
        internal virtual float SearchHeight
        {
            get { return searchRect.height; }
        }

        internal virtual float HeaderHeight
        {
            get { return headerRect.height; }
        }

        internal virtual GUIStyle LineStyle
        {
            get { return Styles.itemStyle; }
        }

        internal virtual Vector2 IconSize
        {
            get { return iconSize; }
        }

        internal AdvancedDropdownState State
        {
            get;
            set;
        }
        #endregion

        #region Implementation
        public void Init ()
        {
            focusSet = false;
        }

        internal virtual void DrawItem (AdvancedDropdownItem item, string name, Texture2D icon, bool enabled, bool drawArrow, bool selected, bool hasSearch)
        {
            GUIContent content = new GUIContent (name, icon);
            Texture imgTemp = content.image;
            //we need to pretend we have an icon to calculate proper width in case
            if (content.image == null)
                content.image = Texture2D.whiteTexture;
            Rect rect = GUILayoutUtility.GetRect (content, LineStyle, GUILayout.ExpandWidth (true));
            content.image = imgTemp;

            if (Event.current.type != EventType.Repaint)
                return;

            Texture imageTemp = content.image;
            if (content.image == null)
            {
                LineStyle.Draw (rect, GUIContent.none, false, false, selected, selected);
                rect.x += IconSize.x + 1;
                rect.width -= IconSize.x + 1;
            }

            EditorGUI.BeginDisabledGroup (!enabled);
            LineStyle.Draw (rect, content, false, false, selected, selected);
            content.image = imageTemp;
            if (drawArrow)
            {
                float size = LineStyle.lineHeight;
                Rect arrowRect = new Rect (rect.x + rect.width - size, rect.y, size, size);
                LineStyle.Draw (arrowRect, Styles.arrowRightContent, false, false, false, false);
            }

            EditorGUI.EndDisabledGroup ();
        }

        internal virtual void DrawLineSeparator ()
        {
            Rect rect = GUILayoutUtility.GetRect (GUIContent.none, Styles.lineSeparator, GUILayout.ExpandWidth (true));
            if (Event.current.type != EventType.Repaint)
                return;
            
            Color orgColor = GUI.color;
            Color tintColor = (EditorGUIUtility.isProSkin) ? new Color (0.12f, 0.12f, 0.12f, 1.333f) : new Color (0.6f, 0.6f, 0.6f, 1.333f);
            GUI.color = GUI.color * tintColor;
            GUI.DrawTexture (rect, EditorGUIUtility.whiteTexture);
            GUI.color = orgColor;
        }

        internal void DrawHeader (AdvancedDropdownItem group, Action backButtonPressed, bool hasParent)
        {
            GUIContent content = new GUIContent (group.Name, group.Icon);
            headerRect = GUILayoutUtility.GetRect (content, Styles.header, GUILayout.ExpandWidth (true));

            if (Event.current.type == EventType.Repaint)
                Styles.header.Draw (headerRect, content, false, false, false, false);

            // Back button
            if (hasParent)
            {
                int arrowWidth = 13;
                Rect arrowRect = new Rect (headerRect.x, headerRect.y, arrowWidth, headerRect.height);
                if (Event.current.type == EventType.Repaint)
                    Styles.headerArrow.Draw (arrowRect, Styles.arrowLeftContent, false, false, false, false);
                if (Event.current.type == EventType.MouseDown && headerRect.Contains (Event.current.mousePosition))
                {
                    backButtonPressed ();
                    Event.current.Use ();
                }
            }
        }

        internal void DrawSearchField (bool isSearchFieldDisabled, string searchString, Action<string> searchChanged)
        {
            if (!isSearchFieldDisabled && !focusSet)
            {
                focusSet = true;
                searchField.SetFocus ();
            }

            using (new EditorGUI.DisabledScope (isSearchFieldDisabled))
            {
                string newSearch = DrawSearchFieldControl (searchString);

                if (newSearch != searchString) 
                    searchChanged (newSearch);
            }
        }

        internal virtual string DrawSearchFieldControl (string searchString)
        {
            float paddingX = 8f;
            float paddingY = 2f;
            Rect rect = GUILayoutUtility.GetRect (0, 0, Styles.toolbarSearchField);
            rect.x += paddingX;
            rect.y += paddingY + 1; // Add one for the border
            rect.height += Styles.toolbarSearchField.fixedHeight + paddingY * 3;
            rect.width -= paddingX * 2;
            searchRect = rect;
            searchString = searchField.OnToolbarGUI (searchRect, searchString);
            return searchString;
        }

        internal Rect GetAnimRect (Rect position, float anim)
        {
            // Calculate rect for animated area
            Rect rect = new Rect (position);
            rect.x = position.x + position.width * anim;
            rect.y += SearchHeight;
            rect.height -= SearchHeight;
            return rect;
        }

        internal Vector2 CalculateContentSize (AdvancedDropdownDataSource dataSource)
        {
            float maxWidth = 0;
            float maxHeight = 0;
            bool includeArrow = false;
            float arrowWidth = 0;

            foreach (AdvancedDropdownItem child in dataSource.MainTree.Children)
            {
                GUIContent content = new GUIContent (child.Name, child.Icon);
                Vector2 a = LineStyle.CalcSize (content);
                a.x += IconSize.x + 1;

                if (maxWidth < a.x)
                {
                    maxWidth = a.x + 1;
                    includeArrow |= child.Children.Any ();
                }

                if (child.IsSeparator ())
                    maxHeight += Styles.lineSeparator.CalcHeight (content, maxWidth) + Styles.lineSeparator.margin.vertical;
                else
                    maxHeight += LineStyle.CalcHeight (content, maxWidth);

                if (arrowWidth == 0) 
                    LineStyle.CalcMinMaxWidth (Styles.arrowRightContent, out arrowWidth, out arrowWidth);
            }

            if (includeArrow) 
                maxWidth += arrowWidth;

            return new Vector2 (maxWidth, maxHeight);
        }

        internal float GetSelectionHeight (AdvancedDropdownDataSource dataSource, Rect buttonRect)
        {
            if (State.GetSelectedIndex (dataSource.MainTree) == -1)
                return 0;
            
            float heigth = 0;
            for (int i = 0; i < dataSource.MainTree.Children.Count (); i++)
            {
                AdvancedDropdownItem child = dataSource.MainTree.Children.ElementAt (i);
                GUIContent content = new GUIContent (child.Name, child.Icon);
                if (State.GetSelectedIndex (dataSource.MainTree) == i)
                {
                    float diff = (LineStyle.CalcHeight (content, 0) - buttonRect.height) / 2f;
                    return heigth + diff;
                }

                if (child.IsSeparator ())
                    heigth += Styles.lineSeparator.CalcHeight (content, 0) + Styles.lineSeparator.margin.vertical;
                else
                    heigth += LineStyle.CalcHeight (content, 0);
            }

            return heigth;
        }
        #endregion

        #region Nested Classes
        private static class Styles
        {
            public static GUIStyle toolbarSearchField = "ToolbarSeachTextField";

            public static GUIStyle itemStyle = new GUIStyle ("PR Label");
            public static GUIStyle header = new GUIStyle ("In BigTitle");
            public static GUIStyle headerArrow = new GUIStyle ();
            public static GUIStyle checkMark = new GUIStyle ("PR Label");
            public static GUIStyle lineSeparator = new GUIStyle ();
            public static GUIContent arrowRightContent = new GUIContent ("▸");
            public static GUIContent arrowLeftContent = new GUIContent ("◂");

            static Styles ()
            {
                itemStyle.alignment = TextAnchor.MiddleLeft;
                itemStyle.padding = new RectOffset (0, 0, 0, 0);
                itemStyle.margin = new RectOffset (0, 0, 0, 0);
                itemStyle.fixedHeight += 1;

                header.font = EditorStyles.boldLabel.font;
                header.margin = new RectOffset (0, 0, 0, 0);
                header.border = new RectOffset (0, 0, 3, 3);
                header.padding = new RectOffset (6, 6, 6, 6);
                header.contentOffset = Vector2.zero;

                headerArrow.alignment = TextAnchor.MiddleCenter;
                headerArrow.fontSize = 20;
                headerArrow.normal.textColor = Color.gray;

                lineSeparator.fixedHeight = 1;
                lineSeparator.margin.bottom = 2;
                lineSeparator.margin.top = 2;

                checkMark.alignment = TextAnchor.MiddleCenter;
                checkMark.padding = new RectOffset (0, 0, 0, 0);
                checkMark.margin = new RectOffset (0, 0, 0, 0);
                checkMark.fixedHeight += 1;
            }
        }
        #endregion
    }
}

#endif // UNITY_EDITOR