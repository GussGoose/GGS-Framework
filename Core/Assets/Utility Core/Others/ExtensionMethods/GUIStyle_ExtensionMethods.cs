using UnityEngine;
using System.Collections;

public static partial class ExtensionMethods {

	#region Class members
	public static void Draw (this GUIStyle guiStyle, Rect rect) {
		if (Event.current.type == EventType.Repaint)
			guiStyle.Draw (rect, false, false, false, false);
	}
	#endregion
}