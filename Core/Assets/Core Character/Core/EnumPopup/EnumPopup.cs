using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Make a popup menu with search bar
/// </summary>
public class EnumPopup {

	#region Class implementation
	public static void Popup (SerializedProperty serializedProperty, string label) {
		Rect rect = GUILayoutUtility.GetRect (new GUIContent (label), EditorStyles.popup);
		DoPopup (rect, serializedProperty, label, EditorStyles.popup);
	}

	public static void Popup (SerializedProperty serializedProperty, string label, GUIStyle style) {
		Rect rect = GUILayoutUtility.GetRect (new GUIContent (label), style);
		DoPopup (rect, serializedProperty, label, style);
	}

	public static void Popup (Rect rect, SerializedProperty serializedProperty, string label) {
		DoPopup (rect, serializedProperty, label, EditorStyles.popup);
	}

	public static void Popup (Rect rect, SerializedProperty serializedProperty, string label, GUIStyle style) {
		DoPopup (rect, serializedProperty, label, style);
	}

	private static void DoPopup (Rect rect, SerializedProperty serializedProperty, string label, GUIStyle style) {
		bool labelEmpty = label == string.Empty;
		string selectedEnum = serializedProperty.enumNames[serializedProperty.enumValueIndex];
		float styleHeight = style.CalcHeight (new GUIContent (selectedEnum), 100);

		List<RectLayoutElement> layoutElements = new List<RectLayoutElement> ();
		layoutElements.Add (new RectLayoutElement ("Field"));
		if (!labelEmpty)
			layoutElements.Insert (0, new RectLayoutElement ("Label", EditorGUIUtility.labelWidth));
		Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (rect, layoutElements.ToArray ());

		if (!labelEmpty)
			GUI.Label (rects["Label"], label);

		if (GUI.Button (rects["Field"], selectedEnum.ToTitleCase (), style)) {
			EnumPopupWindow popupWindow = new EnumPopupWindow (serializedProperty);

			Rect windowRect = new Rect (rects["Field"].position, new Vector2 (1, styleHeight));
			PopupWindow.Show (windowRect, popupWindow);
		}
	}
	#endregion
}