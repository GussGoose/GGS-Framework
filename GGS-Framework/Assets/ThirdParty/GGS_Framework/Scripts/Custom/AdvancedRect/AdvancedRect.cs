using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
    public static partial class AdvancedRect
    {
        #region Class Implementation
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
                    //if (element.Key == "I1")
                    //    Debug.Log (element.padding);
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
        #endregion
    }
}