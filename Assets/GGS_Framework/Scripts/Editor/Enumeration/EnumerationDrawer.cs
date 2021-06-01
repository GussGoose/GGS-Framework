using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    [CustomPropertyDrawer (typeof(EnumerationAttribute))]
    public class EnumerationDrawer : PropertyDrawer
    {
        #region Implementation
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            ExtendedGUI.Enumeration (position, label, property);
        }
        #endregion
    }
}