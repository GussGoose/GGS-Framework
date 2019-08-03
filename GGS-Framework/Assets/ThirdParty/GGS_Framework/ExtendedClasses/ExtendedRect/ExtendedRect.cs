using System.Collections.Generic;
using UnityEngine;

namespace GGS_Framework
{
	public class ExtendedRect
	{
		#region Class implementation
		public static Dictionary<string, Rect> HorizontalRects (Rect rect, params RectLayoutElement[] layoutElements)
		{
			float unexpandedSize = 0;
			int expandedElements = 0;

			foreach (RectLayoutElement element in layoutElements)
			{
				if (!element.use)
					continue;

				if (element.expand)
					expandedElements += 1;
				else
					unexpandedSize += element.size;
			}

			float expandedElementSize = (rect.width - unexpandedSize) / expandedElements;

			Dictionary<string, Rect> rects = new Dictionary<string, Rect> ();
			float currentComputePosition = rect.x;
			foreach (RectLayoutElement element in layoutElements)
			{
				if (!element.use)
					continue;

				if (element.expand)
					element.size = expandedElementSize;

				Vector2 position = new Vector2 (currentComputePosition, rect.y);
				Vector2 size = new Vector2 (element.size, rect.height);

				currentComputePosition += element.size;

				if (!string.IsNullOrEmpty (element.key))
					rects.Add (element.key, new Rect (position, size));
			}

			return rects;
		}

		public static Dictionary<string, Rect> VerticalRects (Rect rect, params RectLayoutElement[] layoutElements)
		{
			float unexpandedSize = 0;
			int expandedElements = 0;

			foreach (RectLayoutElement element in layoutElements)
			{
				if (!element.use)
					continue;

				if (element.expand)
					expandedElements += 1;
				else
					unexpandedSize += element.size;
			}

			float expandedElementSize = (rect.height - unexpandedSize) / expandedElements;

			Dictionary<string, Rect> rects = new Dictionary<string, Rect> ();
			float currentComputePosition = rect.y;
			foreach (RectLayoutElement element in layoutElements)
			{
				if (!element.use)
					continue;

				if (element.expand)
					element.size = expandedElementSize;

				Vector2 position = new Vector2 (rect.x, currentComputePosition);
				Vector2 size = new Vector2 (rect.width, element.size);

				currentComputePosition += element.size;

				if (!string.IsNullOrEmpty (element.key))
					rects.Add (element.key, new Rect (position, size));
			}

			return rects;
		}
		#endregion
	}
}