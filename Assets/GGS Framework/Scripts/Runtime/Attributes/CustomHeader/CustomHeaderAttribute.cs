// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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