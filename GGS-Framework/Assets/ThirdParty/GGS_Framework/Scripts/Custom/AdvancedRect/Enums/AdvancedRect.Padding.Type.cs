namespace GGS_Framework
{
    public partial class AdvancedRect
    {
        public partial class Padding
        {
            [System.Flags]
            public enum Type
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
    }
}
