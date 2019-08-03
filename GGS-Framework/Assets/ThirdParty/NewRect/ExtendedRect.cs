namespace GGS_Framework.Development
{
	using System.Collections.Generic;
	using UnityEngine;

	public class ExtendedRect : MonoBehaviour
	{
		#region Class members
		#endregion

		#region Class accesors
		#endregion

		#region Class overrides
		#endregion

		#region Class implementation
		public static List<Rect> GroupRect (Rect rect, params RectGroup[] groups)
		{
			SetRectForGroups (rect, groups);

			List<Rect> rects = new List<Rect> ();
			foreach (RectGroup group in groups)
				rects.Add (group.settings.rect);

			return rects;
		}

		private static void SetRectForGroups (Rect totalRect, params RectGroup[] groups)
		{
			if (groups.Length == 0)
				return;

			RectGroupOrientation orientation = groups[0].orientation;

			for (int i = 1; i < groups.Length; i++)
			{
				if (groups[i].orientation != orientation)
				{
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

		private static void SetSettingsRect (Rect totalRect, RectGroupOrientation orientation, RectSettings[] settings)
		{
			float unexpandedSize = 0;
			int expandedElements = 0;

			foreach (RectSettings setting in settings)
			{
				if (!setting.use)
					continue;

				if (setting.Expanded)
					expandedElements += 1;
				else
					unexpandedSize += setting.size;
			}

			float expandedElementSize = (((orientation == RectGroupOrientation.Horizontal) ? totalRect.width : totalRect.height) - unexpandedSize) / expandedElements;
			float currentComputePosition = (orientation == RectGroupOrientation.Horizontal) ? totalRect.x : totalRect.y;
			foreach (RectSettings setting in settings)
			{
				if (!setting.use)
					continue;

				if (setting.Expanded)
					setting.size = expandedElementSize;

				Vector2 position = Vector2.zero;
				Vector2 size = Vector2.zero;

				if (orientation == RectGroupOrientation.Horizontal)
				{
					position = new Vector2 (currentComputePosition, totalRect.y);
					size = new Vector2 (setting.size, totalRect.height);
				}
				else
				{
					position = new Vector2 (totalRect.x, currentComputePosition);
					size = new Vector2 (totalRect.width, setting.size);
				}

				currentComputePosition += setting.size;

				setting.rect = new Rect (position, size);
			}
		}

		private static List<RectGroup> GetGroups (RectGroup root)
		{
			List<RectGroup> groups = new List<RectGroup> ();

			foreach (RectGroup child in root.groups)
			{
				groups.Add (child);
				groups.AddRange (GetGroups (child));
			}

			return groups;
		}
		#endregion

		#region Interface implementation
		#endregion
	}
}