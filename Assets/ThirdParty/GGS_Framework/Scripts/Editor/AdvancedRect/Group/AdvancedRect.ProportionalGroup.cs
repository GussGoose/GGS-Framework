namespace GGS_Framework.Editor
{
	public static partial class AdvancedRect
	{
		public class ProportionalGroup : Group
		{
			#region Class Implementation
			public ProportionalGroup (string key, Orientation orientation, float percent, params Element[] elements) : base (Type.Proportional, key, orientation, percent, null, elements, true)
			{
			}

			public ProportionalGroup (string key, Orientation orientation, float percent, RectPadding padding, params Element[] elements) : base (Type.Proportional, key, orientation, percent, padding, elements, true)
			{
			}

			public ProportionalGroup (string key, Orientation orientation, float percent, bool use, params Element[] elements) : base (Type.Proportional, key, orientation, percent, null, elements, use)
			{
			}

			public ProportionalGroup (string key, Orientation orientation, float percent, RectPadding padding, bool use, params Element[] elements) : base (Type.Proportional, key, orientation, percent, padding, elements, use)
			{
			}

			public ProportionalGroup (Orientation orientation, float percent, params Element[] elements) : base (Type.Proportional, string.Empty, orientation, percent, null, elements, true)
			{
			}

			public ProportionalGroup (Orientation orientation, float percent, RectPadding padding, params Element[] elements) : base (Type.Proportional, string.Empty, orientation, percent, padding, elements, true)
			{
			}

			public ProportionalGroup (Orientation orientation, float percent, bool use, params Element[] elements) : base (Type.Proportional, string.Empty, orientation, percent, null, elements, use)
			{
			}

			public ProportionalGroup (Orientation orientation, float percent, RectPadding padding, bool use, params Element[] elements) : base (Type.Proportional, string.Empty, orientation, percent, padding, elements, use)
			{
			}
			#endregion
		}
	}
}