// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

namespace GGS_Framework.Editor
{
    [System.Flags]
    public enum RectPaddingType
    {
        Left = 1 << 0,
        Right = 1 << 1,
        Top = 1 << 2,
        Bottom = 1 << 3,

        Horizontal = Left | Right,
        Vertical = Top | Bottom,

        TopLeft = Top | Left,
        TopRight = Top | Right,

        BottomLeft = Bottom | Left,
        BottomRight = Bottom | Right,

        All = Horizontal | Vertical
    }
}