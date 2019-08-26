using UnityEngine;

namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class Group : Element
		{
			#region Class Accesors
			public Orientation Orientation
			{
				get; private set;
			}

			public Element[] Elements
			{
				get; private set;
			}
			#endregion

			#region Class Implementation
			internal Group (string key, Orientation orientation, SizeType sizeType, float size, bool use, Element[] elements) : base (key, sizeType, size, use)
			{
				Orientation = orientation;
				Elements = elements;
			}

			internal void ComputeElements ()
			{
				ComputeElementsRect (Rect, Orientation, Elements);
			}

			internal static void ComputeElementsRect (Rect rect, Orientation orientation, Element[] elements)
			{
				int elementCount = elements.Length;
				Vector2 currentSize = Vector2.zero;
				Vector2 leftSize = Vector2.zero;

				Rect[] rects = new Rect[elementCount];

				// Set rect size for every element with fixed size type
				for (int i = 0; i < elementCount; i++)
				{
					Element element = elements[i];

					if (!element.Use)
						continue;

					if (element.SizeType == SizeType.Fixed)
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

				// Set rect size for every element with proportional size type
				for (int i = 0; i < elementCount; i++)
				{
					Element element = elements[i];

					if (!element.Use)
						continue;

					if (element.SizeType == SizeType.Proportional)
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
					else if (element.SizeType == SizeType.Expanded)
						expandedElementCount++;
				}

				// Update the left size
				leftSize = rect.size - currentSize;

				// Calculate the requiered size for every element with expanded size type
				float expandedSize = ((orientation == Orientation.Horizontal) ? leftSize.x : leftSize.y) / expandedElementCount;

				// Set rect size for every element with expanded size type
				for (int i = 0; i < elementCount; i++)
				{
					Element element = elements[i];

					if (!element.Use)
						continue;

					if (element.SizeType == SizeType.Expanded)
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
			}
			#endregion
		}
	}
}