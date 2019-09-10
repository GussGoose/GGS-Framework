namespace GGS_Framework
{
	public static partial class AdvancedGenericDropdown
	{
		public class Item : Option
		{
			#region Class Implementation
			public Item (string path, bool selected = false, bool use = true) : base (path, selected, use)
			{
			}
			#endregion
		}
	}
}