using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer (typeof (LayerAttribute))]
public class LayerAttributeDrawer : PropertyDrawer {

	#region Class overrides
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		if (property.propertyType != SerializedPropertyType.Integer) {
			EditorGUI.HelpBox (position, "The type must be a int", MessageType.Error);
			return;
		}

		string currentSelected = LayerMask.LayerToName (property.intValue);

		EditorGUI.BeginChangeCheck ();
		property.intValue = EditorGUI.LayerField (position, label, LayerMask.NameToLayer (currentSelected));
		if (EditorGUI.EndChangeCheck ())
			property.serializedObject.ApplyModifiedProperties ();
	}
	#endregion
}