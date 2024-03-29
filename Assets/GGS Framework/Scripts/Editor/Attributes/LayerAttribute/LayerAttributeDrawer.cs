﻿// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	[CustomPropertyDrawer (typeof (LayerAttribute))]
	public class LayerAttributeDrawer : PropertyDrawer
	{
		#region Class Overrides
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			if (property.propertyType != SerializedPropertyType.Integer)
			{
				EditorGUI.HelpBox (position, "The type must be a int", MessageType.Error);
				return;
			}

			EditorGUI.BeginChangeCheck ();
			string currentSelected = LayerMask.LayerToName (property.intValue);
			property.intValue = EditorGUI.LayerField (position, label, LayerMask.NameToLayer (currentSelected));
			if (EditorGUI.EndChangeCheck ())
				property.serializedObject.ApplyModifiedProperties ();
		}
		#endregion
	}
}