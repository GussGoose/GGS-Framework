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
            ExtendedGUI.IntRange (rect, label, property);
        }
        #endregion
    }
}