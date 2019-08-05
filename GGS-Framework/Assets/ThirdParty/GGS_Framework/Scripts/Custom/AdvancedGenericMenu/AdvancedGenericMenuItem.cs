using System.Text.RegularExpressions;

namespace GGS_Framework
{
	public static partial class AdvancedGenericMenu
	{
		public class Item
		{
			#region Class accesors
			public string Path
			{
				get; private set;
			}

			public bool Selected
			{
				get; private set;
			}
			#endregion

			#region Class implementation
			public Item (string path, bool selected)
			{
				Path = path;
				Selected = selected;
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