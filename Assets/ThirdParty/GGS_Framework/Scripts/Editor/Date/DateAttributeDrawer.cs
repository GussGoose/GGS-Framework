using GGS_Framework.Editor;
using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
    [CustomPropertyDrawer (typeof(DateAttribute))]
    public class DateAttributeDrawer : PropertyDrawer
    {
        #region Overrides
        public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label)
        {
            ExtendedGUI.DateField (rect, label, property);
        }
        #endregion
    }
}