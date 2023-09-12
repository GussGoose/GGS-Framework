// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

namespace GGS_Framework.Editor
{
    public static partial class AdvancedGenericDropdown
    {
        public class Separator : Option
        {
            #region Class Implementation
            public Separator (string path, bool use = true) : base (ModifyPath (path), false, use)
            {
            }

            public Separator (bool use = true) : base (string.Empty, false, use)
            {
            }

            private static string ModifyPath (string path)
            {
                return path;
            }
            #endregion
        }
    }
}