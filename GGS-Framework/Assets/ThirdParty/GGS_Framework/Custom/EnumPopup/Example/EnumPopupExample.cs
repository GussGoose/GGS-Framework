using UnityEngine;

namespace GGS_Framework
{
	public class EnumPopupExample : MonoBehaviour
	{
		#region Class members
		[EnumPopup]
		public KeyCode drawerExample;

		[HideInInspector] public KeyCode customInspectorExample;
		#endregion
	}
}