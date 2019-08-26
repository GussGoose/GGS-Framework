namespace GGS_Framework
{
	public static partial class AdvancedRect
	{
		public class ProportionalGroup : Group
		{
			#region Class Implementation
			public ProportionalGroup (string key, Orientation orientation, float percent, params Element[] elements) : base (key, orientation, SizeType.Proportional, percent, true, elements)
			{
			}

			public ProportionalGroup (string key, Orientation orientation, float percent, bool use, params Element[] elements) : base (key, orientation, SizeType.Proportional, percent, use, elements)
			{
			}

			public ProportionalGroup (Orientation orientation, float percent, params Element[] elements) : base (string.Empty, orientation, SizeType.Proportional, percent, true, elements)
			{
			}

			public ProportionalGroup (Orientation orientation, float percent, bool use, params Element[] elements) : base (string.Empty, orientation, SizeType.Proportional, percent, use, elements)
			{
			} 
			#endregion
		}
	}
}