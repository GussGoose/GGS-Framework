using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ExtendedRect {

	#region Class members
	#endregion

	#region Class implementation
	public static List<Rect> GroupRect (Rect rect, params RectGroup[] groups) {
		SetRectForGroups (rect, groups);

		List<Rect> rects = new List<Rect> ();
		foreach (RectGroup group in groups)
			rects.Add (group.settings.rect);

		return rects;
	}

	private static void SetRectForGroups (Rect totalRect, params RectGroup[] groups) {
		if (groups.Length == 0)
			return;

		RectGroupOrientation orientation = groups[0].orientation;

		for (int i = 1; i < groups.Length; i++) {
			if (groups[i].orientation != orientation) {
				Debug.LogError ("Rect Group Orientation cannot be different between groups.");
				return;
			}
		}

		RectSettings[] settings = new RectSettings[groups.Length];

		for (int i = 0; i < settings.Length; i++)
			settings[i] = groups[i].settings;

		SetSettingsRect (totalRect, orientation, settings);

		for (int i = 0; i < groups.Length; i++)
			groups[i].settings = settings[i];
	}

	private static void SetSettingsRect (Rect totalRect, RectGroupOrientation orientation, RectSettings[] settings) {
		float unexpandedSize = 0;
		int expandedElements = 0;

		foreach (RectSettings setting in settings) {
			if (!setting.use)
				continue;

			if (setting.Expanded)
				expandedElements += 1;
			else
				unexpandedSize += setting.size;
		}

		float expandedElementSize = (((orientation == RectGroupOrientation.Horizontal) ? totalRect.width : totalRect.height) - unexpandedSize) / expandedElements;
		float currentComputePosition = (orientation == RectGroupOrientation.Horizontal) ? totalRect.x : totalRect.y;
		foreach (RectSettings setting in settings) {
			if (!setting.use)
				continue;

			if (setting.Expanded)
				setting.size = expandedElementSize;

			Vector2 position = Vector2.zero;
			Vector2 size = Vector2.zero;

			if (orientation == RectGroupOrientation.Horizontal) {
				position = new Vector2 (currentComputePosition, totalRect.y);
				size = new Vector2 (setting.size, totalRect.height);
			}
			else {
				position = new Vector2 (totalRect.x, currentComputePosition);
				size = new Vector2 (totalRect.width, setting.size);
			}

			currentComputePosition += setting.size;

			setting.rect = new Rect (position, size);
		}
	}

	private static List<RectGroup> GetGroups (RectGroup root) {
		List<RectGroup> groups = new List<RectGroup> ();

		foreach (RectGroup child in root.groups) {
			groups.Add (child);
			groups.AddRange (GetGroups (child));
		}

		return groups;
	}

	public static Dictionary<string, Rect> HorizontalRects (Rect rect, params RectLayoutElement[] layoutElements) {
		float unexpandedSize = 0;
		int expandedElements = 0;

		foreach (RectLayoutElement element in layoutElements) {
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
		foreach (RectLayoutElement element in layoutElements) {
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

	public static Dictionary<string, Rect> VerticalRects (Rect rect, params RectLayoutElement[] layoutElements) {
		float unexpandedSize = 0;
		int expandedElements = 0;

		foreach (RectLayoutElement element in layoutElements) {
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
		foreach (RectLayoutElement element in layoutElements) {
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