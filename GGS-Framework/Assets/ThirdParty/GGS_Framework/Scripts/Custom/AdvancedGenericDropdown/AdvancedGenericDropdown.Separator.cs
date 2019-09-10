namespace GGS_Framework
{
	public static partial class AdvancedGenericDropdown
	{
		public class Separator : Option
		{
			#region Class Implementation
			public Separator (string path, bool use = true) : base (ModifyPath (path), false, use)
			{
			}

			public Separator (bool use = true) : base (string.Empty, false, use)
			{
			}

			private static string ModifyPath (string path)
			{
				return path += "/";
			}
			#endregion
		}
	}
}