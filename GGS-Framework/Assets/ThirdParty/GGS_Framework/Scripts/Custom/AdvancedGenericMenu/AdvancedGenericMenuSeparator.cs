namespace GGS_Framework
{
	public static partial class AdvancedGenericMenu
	{
		public class Separator : Item
		{
			#region Class implementation
			public Separator (string path) : base (ModifyPath (path), false)
			{
			}

			public Separator () : base (string.Empty, false)
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