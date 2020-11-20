namespace GGS_Framework.Editor
{
    public static partial class AdvancedGenericDropdown
    {
        public class Item : Option
        {
            #region Class Implementation
            public Item (string path, bool selected = false, bool use = true) : base (path, selected, use)
            {
            }

            public Item (string path, object data, bool selected = false, bool use = true) : base (path, data, selected, use)
            {
            }
            #endregion
        }
    }
}