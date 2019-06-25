namespace UtilityFramework
{
	using UnityEngine;
	using UnityEditor;

	[CustomPropertyDrawer (typeof (CustomHeaderAttribute))]
	public class CustomHeaderDrawer : DecoratorDrawer
	{

		#region Class overrides
		public override void OnGUI (Rect position)
		{
			CustomHeaderAttribute targetScript = attribute as CustomHeaderAttribute;

			position.y += targetScript.spacing;
			position.size -= Vector2.up * (targetScript.spacing + 2);
			GUI.Box (position, "", EditorStyles.helpBox);
			GUIStyle style = new GUIStyle (GUI.skin.label);
			style.fontStyle = targetScript.style;
			style.alignment = TextAnchor.MiddleCenter;
			GUI.Label (position, new GUIContent (targetScript.text), style);
		}

		public override float GetHeight ()
		{
			CustomHeaderAttribute targetScript = attribute as CustomHeaderAttribute;
			float defaultHeight = base.GetHeight () + 4;

			if (targetScript.spacing == 0)
				return defaultHeight;

			return defaultHeight + targetScript.spacing;
		}
		#endregion
	}
}