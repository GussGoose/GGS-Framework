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