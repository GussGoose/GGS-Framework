// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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