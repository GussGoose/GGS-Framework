using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	[CustomPropertyDrawer (typeof (TimeObject))]
	public class TimeObjectDrawer : PropertyDrawer
	{
		#region Class overrides
		public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty (rect, label, property);

			rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), label);

			int indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
				new AdvancedRect.ExpandedItem ("Value"),
				new AdvancedRect.Space (2),
				new AdvancedRect.FixedItem ("Type", 75)
			);

			EditorGUI.PropertyField (rects["Value"], property.FindPropertyRelative ("value"), GUIContent.none);
			EditorGUI.PropertyField (rects["Type"], property.FindPropertyRelative ("type"), GUIContent.none);

			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty ();
		}
		#endregion
	}
}