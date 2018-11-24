using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ColorFieldAttribute))]
public class ColorFieldDrawer : PropertyDrawer
{
    #region Class overrides
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ColorFieldAttribute target = attribute as ColorFieldAttribute;
        GUI.backgroundColor = Color.green;
        GUI.Box(position, "", EditorStyles.helpBox);
        GUI.backgroundColor = Color.white;

        EditorGUI.BeginChangeCheck();
        EditorGUI.PropertyField(position, property);
        if (EditorGUI.EndChangeCheck())
            property.serializedObject.ApplyModifiedProperties();
    }
    #endregion
}
