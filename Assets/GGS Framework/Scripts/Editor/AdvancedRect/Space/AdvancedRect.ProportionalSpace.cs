// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

namespace GGS_Framework.Editor
{
    public static partial class AdvancedRect
    {
        public class ProportionalSpace : Element
        {
            #region Class Implementation
            public ProportionalSpace (float percent, bool use = true) : base (Type.Proportional, string.Empty, percent, (RectPadding) null, use)
            {
            }
            #endregion
        }
    }
}