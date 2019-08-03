namespace UtilityFramework
{
#if UNITY_EDITOR
	using System.Collections.Generic;
	using UnityEditor;
	using UnityEngine;

	public partial class ExtendedGUILayout
	{
		#region Class implementation
		public static TimeObject TimeObject (string label, TimeObject value)
		{
			Rect rect = EditorGUILayout.GetControlRect (GUILayout.ExpandWidth (true), GUILayout.Height (18));
			rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), new GUIContent (label));

			Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (rect,
				new RectLayoutElement ("Value"),
				new RectLayoutElement (2),
				new RectLayoutElement ("Type", 75));

			value.value = EditorGUI.DoubleField (rects["Value"], value.value);
			value.type = (TimeObjectType) EditorGUI.EnumPopup (rects["Type"], value.type);

			return value;
		}
		#endregion
	}
#endif
}