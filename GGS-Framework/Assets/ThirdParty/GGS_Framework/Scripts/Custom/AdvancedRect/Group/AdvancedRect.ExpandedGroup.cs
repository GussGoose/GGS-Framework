namespace GGS_Framework
{
    public static partial class AdvancedRect
    {
        public class ExpandedGroup : Group
        {
            #region Class Implementation
            public ExpandedGroup (string key, Orientation orientation, params Element[] elements) : base (key, orientation, SizeType.Expanded, 0, null, true, elements)
            {
            }

            public ExpandedGroup (string key, Orientation orientation, Padding padding, params Element[] elements) : base (key, orientation, SizeType.Expanded, 0, padding, true, elements)
            {
            }

            public ExpandedGroup (string key, Orientation orientation, bool use, params Element[] elements) : base (key, orientation, SizeType.Expanded, 0, null, use, elements)
            {
            }

            public ExpandedGroup (string key, Orientation orientation, Padding padding, bool use, params Element[] elements) : base (key, orientation, SizeType.Expanded, 0, padding, use, elements)
            {
            }

            public ExpandedGroup (Orientation orientation, params Element[] elements) : base (string.Empty, orientation, SizeType.Expanded, 0, null, true, elements)
            {
            }

            public ExpandedGroup (Orientation orientation, Padding padding, params Element[] elements) : base (string.Empty, orientation, SizeType.Expanded, 0, padding, true, elements)
            {
            }

            public ExpandedGroup (Orientation orientation, bool use, params Element[] elements) : base (string.Empty, orientation, SizeType.Expanded, 0, null, use, elements)
            {
            }

            public ExpandedGroup (Orientation orientation, Padding padding, bool use, params Element[] elements) : base (string.Empty, orientation, SizeType.Expanded, 0, padding, use, elements)
            {
            }
            #endregion
        }
    }
}