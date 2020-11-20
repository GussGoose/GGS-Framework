using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	[CustomPropertyDrawer (typeof (TimeObject))]
    public class TimeObjectDrawer : PropertyDrawer
    {
        #region Class Overrides
        public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label)
        {
            ExtendedGUI.TimeObject (rect, label, property);
        }
        #endregion
    }
}