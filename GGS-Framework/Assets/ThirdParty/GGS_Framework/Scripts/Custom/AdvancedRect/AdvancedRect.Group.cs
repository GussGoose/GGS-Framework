namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public abstract class Group : Element
		{
			#region Class Accesors
			public Orientation Orientation
			{
				get; private set;
			}

			public Element[] Elements
			{
				get; private set;
			}
			#endregion

			#region Class Implementation
			public Group (Type type, string key, Orientation orientation, float size, Padding padding, Element[] elements, bool use) : base (type, key, size, padding, use)
			{
				Orientation = orientation;
				Elements = elements;
			}

			public void ComputeElements ()
			{
				ComputeElementsRect (Rect, Orientation, Elements);
			}
			#endregion
		}
	}
}