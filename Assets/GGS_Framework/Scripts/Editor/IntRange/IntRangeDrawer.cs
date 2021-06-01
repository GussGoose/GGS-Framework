using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	[CustomPropertyDrawer (typeof (IntRange))]
	public class IntRangeDrawer : PropertyDrawer
	{
		#region Class Overrides
		public override void OnGUI (Rect rect, SerializedProperty property, GUIContent label)
		{
			ExtendedGUI.IntRange (rect, label, property);
		}
		#endregion
	}
}