namespace GGS_Framework
{
    public static partial class AdvancedRect
    {
        public class FixedItem : Element
        {
            #region Class Implementation
            public FixedItem (string key, float size, Padding padding) : base (key, SizeType.Fixed, size, padding, true)
            {
            }

            public FixedItem (string key, float size, bool use) : base (key, SizeType.Fixed, size, null, use)
            {
            }

            public FixedItem (string key, float size, Padding padding = null, bool use = true) : base (key, SizeType.Fixed, size, padding, use)
            {
            }
            #endregion
        }
    }
}