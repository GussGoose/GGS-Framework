namespace GGS_Framework
{
	public static partial class AdvancedGenericDropdown
	{
		public abstract class Option
		{
			#region Class Accesors
			public string Path
			{
				get; private set;
			}

			public int Index
			{
				get; private set;
			}

			public bool Selected
			{
				get; private set;
			}

			public bool Use
			{
				get; private set;
			}
			#endregion

			#region Class Implementation
			protected Option (string path, bool selected = false, bool use = true)
			{
				Path = path;
				Selected = selected;
				Use = use;
			}

			public string GetValue ()
			{
				string[] splittedPath = Path.Split ('/');
				return splittedPath[splittedPath.Length - 1];
			}
			#endregion
		}
	}
}