namespace GGS_Framework
{
    public static partial class AdvancedRect
    {
        public class ProportionalItem : Element
        {
            #region Class Implementation
            public ProportionalItem (string key, float percent, Padding padding) : base (key, SizeType.Proportional, percent, padding, true)
            {
            }

            public ProportionalItem (string key, float percent, bool use) : base (key, SizeType.Proportional, percent, null, use)
            {
            }

            public ProportionalItem (string key, float percent, Padding padding = null, bool use = true) : base (key, SizeType.Proportional, percent, padding, use)
            {
            }
            #endregion
        }
    }
}