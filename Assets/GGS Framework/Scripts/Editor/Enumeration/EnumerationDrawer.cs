// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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