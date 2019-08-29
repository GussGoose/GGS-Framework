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
            ExtendedGUI.TimeObject (rect, label, property);
        }
        #endregion
    }
}