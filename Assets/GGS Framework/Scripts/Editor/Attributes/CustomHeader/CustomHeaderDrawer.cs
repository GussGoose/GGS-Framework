// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEditor;
using UnityEngine;

namespace GGS_Framework.Editor
{
	[CustomPropertyDrawer (typeof (CustomHeaderAttribute))]
	public class CustomHeaderDrawer : DecoratorDrawer
	{
		#region Class Overrides
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