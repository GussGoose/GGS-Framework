using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GGS_Framework
{
    public class AdvancedRectExampleEditorWindow : EditorWindow
    {
        #region Class Accesors
        private Rect Rect
        {
            get { return new Rect (Vector2.zero, position.size); }
        }
        #endregion

        #region Class Overrides
        private void OnGUI ()
        {
            Dictionary<string, Rect> rects = AdvancedRect.GetRects (Rect, AdvancedRect.Orientation.Vertical,
                new AdvancedRect.FixedItem ("Header", 18),
                new AdvancedRect.Space (2),
                new AdvancedRect.ExpandedGroup (AdvancedRect.Orientation.Horizontal,
                    new AdvancedRect.FixedGroup (AdvancedRect.Orientation.Vertical, 150,
                        new AdvancedRect.FixedItem ("ListHeader", 18),
                        new AdvancedRect.Space (2),
                        new AdvancedRect.ExpandedItem ("List"),
                        new AdvancedRect.Space (2),
                        new AdvancedRect.FixedItem ("AddButton", 16)
                    ),
                    new AdvancedRect.Space (2),
                    new AdvancedRect.ExpandedGroup (AdvancedRect.Orientation.Vertical,
                        new AdvancedRect.ExpandedItem ("PropertiesA"),
                        new AdvancedRect.Space (2),
                        new AdvancedRect.ExpandedItem ("PropertiesB")
                    )
                )
            );

            foreach (KeyValuePair<string, Rect> rect in rects)
            {
                EditorGUI.DrawRect (rect.Value, Color.black);
                AdvancedGUILabel.Draw (rect.Value, new AdvancedGUILabelConfig (rect.Key));
            }
        }
        #endregion

        #region Class Implementation
        [MenuItem ("Window/GGS Framework/Advanced Rect Example")]
        private static void Open ()
        {
            AdvancedRectExampleEditorWindow window = GetWindow<AdvancedRectExampleEditorWindow> ();
            window.titleContent = new GUIContent ("ARect Example");
        }
        #endregion
    }
}