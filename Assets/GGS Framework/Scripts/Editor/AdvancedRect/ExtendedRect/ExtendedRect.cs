// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

using UnityEngine;

namespace GGS_Framework.Editor
{
    public static class ExtendedRect
    {
        #region Implementation
        public static Rect Expand (Rect rect, RectPadding padding)
        {
            return padding.Apply (rect);
        }

        public static Rect Align (Vector2 rectSize, Rect container, RectAlignment alignment)
        {
            Vector2 position = Vector2.zero;
            Vector2 middleRectSize = rectSize / 2f;

            switch (alignment)
            {
                case RectAlignment.TopLeft:
                    position = container.position;
                    break;
                case RectAlignment.TopCenter:
                    position = new Vector2 (container.center.x - middleRectSize.x, container.yMin);
                    break;
                case RectAlignment.TopRight:
                    position = new Vector2 (container.xMax - rectSize.x, container.yMin);
                    break;
                case RectAlignment.CenterLeft:
                    position = new Vector2 (container.xMin, container.center.y - middleRectSize.y);
                    break;
                case RectAlignment.Center:
                    position = container.center - middleRectSize;
                    break;
                case RectAlignment.CenterRight:
                    position = new Vector2 (container.xMax - rectSize.x, container.center.y - middleRectSize.y);
                    break;
                case RectAlignment.BottomLeft:
                    position = new Vector2 (container.xMin, container.yMax - rectSize.y);
                    break;
                case RectAlignment.BottomCenter:
                    position = new Vector2 (container.center.x - middleRectSize.x, container.yMax - rectSize.y);
                    break;
                case RectAlignment.BottomRight:
                    position = new Vector2 (container.xMax - rectSize.x, container.yMax - rectSize.y);
                    break;
            }

            return new Rect (position, rectSize);
        }
        #endregion
    }
}