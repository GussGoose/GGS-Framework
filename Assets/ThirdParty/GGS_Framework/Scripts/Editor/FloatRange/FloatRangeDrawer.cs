using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	[CustomPropertyDrawer (typeof (FloatRange))]
	public class FloatRangeDrawer : PropertyDrawer
	{
		#region Class Overrides
		public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label)
		{
			ExtendedGUI.FloatRange (rect, label, property);
		}
		#endregion
	}
}