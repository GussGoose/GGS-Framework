using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework
{
	[CustomPropertyDrawer (typeof (FloatRange))]
	public class FloatRangeDrawer : PropertyDrawer
	{
		#region Class overrides
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty (position, label, property);

			position = EditorGUI.PrefixLabel (position, GUIUtility.GetControlID (FocusType.Passive), label);

			int indentLevel = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;

			Dictionary<string, Rect> rects = ExtendedRect.HorizontalRects (position,
				new RectLayoutElement ("Start"),
				new RectLayoutElement (5),
				new RectLayoutElement ("End"));

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