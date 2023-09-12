// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	/// <summary>
	/// Make a popup menu with search bar
	/// </summary>
	public class PopupMenu
	{
		#region Class implementation
		public static void Popup (System.Action<int, string> onValueSelected, params string[] values)
		{
			DoPopup (new Rect (), true, onValueSelected, values);
		}

		public static void Popup (Rect rect, System.Action<int, string> onValueSelected, params string[] values)
		{
			DoPopup (rect, false, onValueSelected, values);
		}

		private static void DoPopup (Rect rect, bool showUnderMouse, System.Action<int, string> onValueSelected, string[] values)
		{
			PopupMenuWindow popupWindow = new PopupMenuWindow (onValueSelected, values);

			Rect windowRect = (showUnderMouse) ? new Rect (Event.current.mousePosition, Vector2.zero) : rect;
			PopupWindow.Show (windowRect, popupWindow);
		}
		#endregion
	}
}