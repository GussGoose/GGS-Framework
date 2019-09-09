namespace GGS_Framework
{
	public static partial class AdvancedGenericMenu
	{
		public class Item
		{
			#region Class Accesors
			public string Path
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
			public Item (string path, bool selected = false, bool use = true)
			{
				Path = path;
				Selected = selected;
				Use = use;
			}

			public string GetItemValue ()
			{
				string[] splittedPath = Path.Split ('/');

				string value = splittedPath[splittedPath.Length - 1];
				value = value.Replace (" ", string.Empty);

				return value;
			}
			#endregion
		}
	}
}