using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	[CustomPropertyDrawer (typeof (IntRange))]
	public class IntRangeDrawer : PropertyDrawer
	{
		#region Class overrides
		public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty (rect, label, property);

			rect = EditorGUI.PrefixLabel (rect, GUIUtility.GetControlID (FocusType.Passive), label);

			int indentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			Dictionary<string, Rect> rects = AdvancedRect.GetRects (rect, AdvancedRect.Orientation.Horizontal,
				new AdvancedRect.ExpandedItem ("Start"),
				new AdvancedRect.Space (5),
				new AdvancedRect.ExpandedItem ("End")
			);

			EditorGUIUtility.labelWidth = 35;
			EditorGUI.PropertyField (rects["Start"], property.FindPropertyRelative ("start"));
			EditorGUI.PropertyField (rects["End"], property.FindPropertyRelative ("end"));
			EditorGUIUtility.labelWidth = 0;

			EditorGUI.indentLevel = indentLevel;

			EditorGUI.EndProperty ();
		}
		#endregion
	}
}