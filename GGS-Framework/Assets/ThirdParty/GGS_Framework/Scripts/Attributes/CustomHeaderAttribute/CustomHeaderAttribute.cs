using UnityEngine;

namespace GGS_Framework
{
	public class CustomHeaderAttribute : PropertyAttribute
	{
		#region Class Members
		public string text;
		public float spacing;
		public FontStyle style;
		#endregion

		#region Class Implementation
		public CustomHeaderAttribute (string text, float spacing, FontStyle style = FontStyle.Normal)
		{
			this.text = text;
			this.spacing = spacing;
			this.style = style;
		}

		public CustomHeaderAttribute (string text, FontStyle style = FontStyle.Normal)
		{
			this.text = text;
			spacing = 8;
			this.style = style;
		}
		#endregion
	}
}