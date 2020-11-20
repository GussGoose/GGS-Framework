using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		#region Class Implementation
		/// <summary>
		/// Compute rects in especified orientation
		/// </summary>
		/// <param name="rect"> Container of all elements and groups  </param>
		/// <param name="elements"> Elements and groups </param>
		public static Dictionary<string, Rect> GetRects (Rect rect, Orientation orientation, params Element[] elements)
		{
			ComputeElementsRect (rect, orientation, elements);

			List<Element> allElements = new List<Element> ();

			foreach (Element element in elements)
				allElements.AddRange (GetElementsRecursively (element));

			Dictionary<string, Rect> rects = new Dictionary<string, Rect> ();

			foreach (Element element in allElements)
			{
				if (element is Group)
				{
					(element as Group).ComputeElements ();
				}
			}

			foreach (Element element in allElements)
			{
				if (element.Use & element.Key != string.Empty)
				{
					if (!(element is Group))
						element.ApplyPadding ();

					rects.Add (element.Key, element.Rect);
				}
			}

			return rects;
		}

		public static Dictionary<string, Rect> GetRectsByPrefix (Dictionary<string, Rect> originalRects, string prefix)
		{
			Dictionary<string, Rect> rects = new Dictionary<string, Rect> ();

			foreach (KeyValuePair<string, Rect> keyValuePair in originalRects)
			{
				string currentKey = keyValuePair.Key;
				string fullPrefix = string.Concat (prefix, ".");

				if (currentKey.Contains (prefix))
					rects.Add (currentKey.Replace (fullPrefix, string.Empty), keyValuePair.Value);
			}

			return rects;
		}

		public static Element AddPrefixToElement (string prefix, Element element)
		{
			List<Element> allElements = GetElementsRecursively (element);

			foreach (Element currentElement in allElements)
			{
				if (!string.IsNullOrEmpty (currentElement.Key))
					currentElement.Key = $"{prefix}.{currentElement.Key}";
			}

			return element;
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

		private static Vector2 ComputeElementsRect (Rect rect, Orientation orientation, Element[] elements)
		{
			int elementCount = elements.Length;
			Vector2 currentSize = Vector2.zero;
			Vector2 leftSize = Vector2.zero;

			Rect[] rects = new Rect[elementCount];

			// Set rect size for every fixed element
			for (int i = 0; i < elementCount; i++)
			{
				Element element = elements[i];

				if (!element.Use)
					continue;

				if (element.ElementType == Element.Type.Fixed)
				{
					Vector2 size = Vector2.zero;
					if (orientation == Orientation.Horizontal)
					{
						size = new Vector2 (element.Size, rect.height);
						currentSize.x += size.x;
					}
					else
					{
						size = new Vector2 (rect.width, element.Size);
						currentSize.y += size.y;
					}

					rects[i].size = size;
				}
			}

			// Update the left size
			leftSize = rect.size - currentSize;

			int expandedElementCount = 0;

			// Set rect size for every proportional element
			for (int i = 0; i < elementCount; i++)
			{
				Element element = elements[i];

				if (!element.Use)
					continue;

				if (element.ElementType == Element.Type.Proportional)
				{
					Vector2 size = Vector2.zero;
					float proportionalElementSize = element.Size / 100f;
					if (orientation == Orientation.Horizontal)
					{
						size = new Vector2 (proportionalElementSize * leftSize.x, rect.height);
						currentSize.x += size.x;
					}
					else
					{
						size = new Vector2 (rect.width, proportionalElementSize * leftSize.y);
						currentSize.y += size.y;
					}

					rects[i].size = size;
				}
				// Increases expanded element count
				else if (element.ElementType == Element.Type.Expanded)
					expandedElementCount++;
			}

			// Update the left size
			leftSize = rect.size - currentSize;

			// Calculate the required size for every expanded element
			float expandedSize = ((orientation == Orientation.Horizontal) ? leftSize.x : leftSize.y) / expandedElementCount;

			// Set rect size for every expanded ellement
			for (int i = 0; i < elementCount; i++)
			{
				Element element = elements[i];

				if (!element.Use)
					continue;

				if (element.ElementType == Element.Type.Expanded)
				{
					Vector2 size = Vector2.zero;
					if (orientation == Orientation.Horizontal)
						size = new Vector2 (expandedSize, rect.height);
					else
						size = new Vector2 (rect.width, expandedSize);

					rects[i].size = size;
				}
			}

			Vector2 currentPosition = rect.position;

			// Calculate rect position for every element
			for (int i = 0; i < elementCount; i++)
			{
				if (!elements[i].Use)
					continue;

				rects[i].position = currentPosition;

				if (orientation == Orientation.Horizontal)
					currentPosition.x += rects[i].width;
				else
					currentPosition.y += rects[i].height;

				elements[i].Rect = rects[i];
			}

			return currentSize;
		}

		/// <summary>
		/// Aligns rect inside container
		/// </summary>
		public static Rect AlignRect (Vector2 rectSize, Rect container, Alignment alignment)
		{
			Vector2 position = Vector2.zero;
			Vector2 middleRectSize = rectSize / 2f;

			switch (alignment)
			{
				case Alignment.TopLeft:
					position = container.position;
					break;
				case Alignment.TopCenter:
					position = new Vector2 (container.center.x - middleRectSize.x, container.yMin);
					break;
				case Alignment.TopRight:
					position = new Vector2 (container.xMax - rectSize.x, container.yMin);
					break;
				case Alignment.CenterLeft:
					position = new Vector2 (container.xMin, container.center.y - middleRectSize.y);
					break;
				case Alignment.Center:
					position = container.center - middleRectSize;
					break;
				case Alignment.CenterRight:
					position = new Vector2 (container.xMax - rectSize.x, container.center.y - middleRectSize.y);
					break;
				case Alignment.BottomLeft:
					position = new Vector2 (container.xMin, container.yMax - rectSize.y);
					break;
				case Alignment.BottomCenter:
					position = new Vector2 (container.center.x - middleRectSize.x, container.yMax - rectSize.y);
					break;
				case Alignment.BottomRight:
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