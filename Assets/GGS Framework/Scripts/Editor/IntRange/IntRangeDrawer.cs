﻿// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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