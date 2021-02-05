namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class ExpandedItem : Element
		{
			#region Class Implementation
			public ExpandedItem (string key, RectPadding padding) : base (Type.Expanded, key, 0, padding, true)
			{
			}

			public ExpandedItem (string key, bool use) : base (Type.Expanded, key, 0, (RectPadding) null, use)
			{
			}

			public ExpandedItem (string key, RectPadding padding = null, bool use = true) : base (Type.Expanded, key, 0, padding, use)
			{
			}
			#endregion
		}
	}
}