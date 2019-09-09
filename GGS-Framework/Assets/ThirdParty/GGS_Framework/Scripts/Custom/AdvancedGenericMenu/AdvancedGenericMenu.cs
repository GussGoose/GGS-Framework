#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	public static partial class AdvancedGenericMenu
	{
		#region Class Implementation
		public static void Draw<T> (Item[] items, Action<object> onItemSelected)
		{
			DoMenu (Rect.zero, typeof (T), items, item =>
			{
				onItemSelected (item);
			});
		}

		public static void Draw<T> (Rect rect, Item[] items, Action<object> onItemSelected)
		{
			DoMenu (rect, typeof (T), items, item =>
			{
				onItemSelected (item);
			});
		}

		public static void Draw (Item[] items, Action<string> onItemSelected)
		{
			DoMenu (Rect.zero, items, item =>
			{
				onItemSelected (item);
			});
		}

		public static void Draw (Rect rect, Item[] items, Action<string> onItemSelected)
		{
			DoMenu (rect, items, item =>
			{
				onItemSelected (item);
			});
		}

		private static void DoMenu (Rect rect, Type enumValue, Item[] items, Action<object> onItemSelected)
		{
			GenericMenu menu = new GenericMenu ();

			for (int i = 0; i < items.Length; i++)
			{
				Item item = items[i];

				if (!item.Use)
					continue;

				if (item as Separator != null)
					menu.AddSeparator (item.Path);
				else
				{
					menu.AddItem (new GUIContent (item.Path), item.Selected, delegate
					{
						object enumObject = Enum.Parse (enumValue, item.GetItemValue (), true);
						onItemSelected (enumObject);
					});
				}
			}

			if (rect != Rect.zero)
				menu.DropDown (rect);
			else
				menu.ShowAsContext ();
		}

		private static void DoMenu (Rect rect, Item[] items, Action<string> onItemSelected)
		{
			GenericMenu menu = new GenericMenu ();

			for (int i = 0; i < items.Length; i++)
			{
				Item item = items[i];

				if (!item.Use)
					continue;

				if (item as Separator != null)
					menu.AddSeparator (item.Path);
				else
				{
					menu.AddItem (new GUIContent (item.Path), item.Selected, delegate
					{
						onItemSelected (item.GetItemValue ());
					});
				}
			}

			if (rect != Rect.zero)
				menu.DropDown (rect);
			else
				menu.ShowAsContext ();
		}
		#endregion
	}
}
#endif