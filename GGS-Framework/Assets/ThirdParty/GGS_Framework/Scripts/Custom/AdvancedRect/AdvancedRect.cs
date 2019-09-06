using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
    public static partial class AdvancedRect
    {
        #region Nested Classes
        public enum Aligment
        {
            TopLeft,
            TopCenter,
            TopRight,

            CenterLeft,
            Center,
            CenterRight,

            BottomLeft,
            BottomCenter,
            BottomRight
        }
        #endregion

        #region Class Implementation
        /// <summary>
        /// Compute rects in especified orientation
        /// </summary>
        /// <param name="rect"> Container of all elements and groups  </param>
        /// <param name="elements"> Elements and groups </param>
        public static Dictionary<string, Rect> GetRects (Rect rect, Orientation orientation, params Element[] elements)
        {
            Group.ComputeElementsRect (rect, orientation, elements);

            List<Element> allElements = new List<Element> ();

            foreach (Element element in elements)
                allElements.AddRange (GetElementsRecursively (element));

            Dictionary<string, Rect> rects = new Dictionary<string, Rect> ();

            foreach (Element element in allElements)
            {
                if (element is Group)
                    (element as Group).ComputeElements ();
            }

            foreach (Element element in allElements)
            {
                if (element.Use & element.Key != string.Empty)
                {
                    element.ApplyPadding ();
                    rects.Add (element.Key, element.Rect);
                }
            }

            return rects;
        }

        private static List<Element> GetElementsRecursively (Element element)
        {
            List<Element> elements = new List<Element> { element };

            Group group = element as Group;
            if (group != null)
            {
                foreach (Element elementInGroup in group.Elements)
                    elements.AddRange (GetElementsRecursively (elementInGroup));
            }

            return elements;
        }

        /// <summary>
        /// Aligns rect inside container
        /// </summary>
        public static Rect AlignRect (Vector2 rectSize, Rect container, Aligment aligment)
        {
            Vector2 position = Vector2.zero;
            Vector2 middleRectSize = rectSize / 2f;

            switch (aligment)
            {
                case Aligment.TopLeft:
                    position = container.position;
                    break;
                case Aligment.TopCenter:
                    position = new Vector2 (container.center.x - middleRectSize.x, container.yMin);
                    break;
                case Aligment.TopRight:
                    position = new Vector2 (container.xMax - rectSize.x, container.yMin);
                    break;
                case Aligment.CenterLeft:
                    position = new Vector2 (container.xMin, container.center.y - middleRectSize.y);
                    break;
                case Aligment.Center:
                    position = container.center - middleRectSize;
                    break;
                case Aligment.CenterRight:
                    position = new Vector2 (container.xMax - rectSize.x, container.center.y - middleRectSize.y);
                    break;
                case Aligment.BottomLeft:
                    position = new Vector2 (container.xMin, container.yMax - rectSize.y);
                    break;
                case Aligment.BottomCenter:
                    position = new Vector2 (container.center.x - middleRectSize.x, container.yMax - rectSize.y);
                    break;
                case Aligment.BottomRight:
                    position = new Vector2 (container.xMax - rectSize.x, container.yMax - rectSize.y);
                    break;
            }

            return new Rect (position, rectSize);
        }

        /// <summary>
        /// Applies padding to rect
        /// </summary>
        public static Rect ExpandRect (Rect rect, Padding padding)
        {
            return padding.Apply (rect);
        }
        #endregion
    }
}