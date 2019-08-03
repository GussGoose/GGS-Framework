using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	[CustomPropertyDrawer (typeof (TimeObject))]
	public class TimeObjectDrawer : PropertyDrawer
	{
		#region Class overrides
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty (position, label, property);

			position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (position,
				new RectLayoutElement ("Value"),
				new RectLayoutElement (2),
				new RectLayoutElement ("Type", 75));

			EditorGUI.PropertyField (rects["Value"], property.FindPropertyRelative ("value"), GUIContent.none);
			EditorGUI.PropertyField (rects["Type"], property.FindPropertyRelative ("type"), GUIContent.none);

			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty ();
		}
		#endregion
	}
}