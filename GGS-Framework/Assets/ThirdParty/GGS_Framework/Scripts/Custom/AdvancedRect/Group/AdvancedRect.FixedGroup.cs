namespace GGS_Framework
{
    public static partial class AdvancedRect
    {
        public class FixedGroup : Group
        {
            #region Class Implementation
            public FixedGroup (string key, Orientation orientation, float size, params Element[] elements) : base (key, orientation, SizeType.Fixed, size, null, true, elements)
            {
            }

            public FixedGroup (string key, Orientation orientation, float size, Padding padding, params Element[] elements) : base (key, orientation, SizeType.Fixed, size, padding, true, elements)
            {
            }

            public FixedGroup (string key, Orientation orientation, float size, bool use, params Element[] elements) : base (key, orientation, SizeType.Fixed, size, null, use, elements)
            {
            }

            public FixedGroup (string key, Orientation orientation, float size, Padding padding, bool use, params Element[] elements) : base (key, orientation, SizeType.Fixed, size, padding, use, elements)
            {
            }

            public FixedGroup (Orientation orientation, float size, Padding padding, params Element[] elements) : base (string.Empty, orientation, SizeType.Fixed, size, padding, true, elements)
            {
            }

            public FixedGroup (Orientation orientation, float size, params Element[] elements) : base (string.Empty, orientation, SizeType.Fixed, size, null, true, elements)
            {
            }

            public FixedGroup (Orientation orientation, float size, bool use, params Element[] elements) : base (string.Empty, orientation, SizeType.Fixed, size, null, use, elements)
            {
            }

            public FixedGroup (Orientation orientation, float size, Padding padding, bool use, params Element[] elements) : base (string.Empty, orientation, SizeType.Fixed, size, padding, use, elements)
            {
            }
            #endregion
        }
    }
}