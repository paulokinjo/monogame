using Microsoft.Xna.Framework;
using SharpDX.Direct2D1;

namespace RobotRampage
{
    internal static class Camera
    {
        private static Vector2 position = Vector2.Zero;
        private static Vector2 viewPortSize = Vector2.Zero;
        private static Rectangle worldRectangle = new Rectangle(0, 0, 0, 0);

        public static int ViewPortWidth
        {
            get { return (int)viewPortSize.X; }
            set { viewPortSize.X = value; }
        }

        public static int ViewPortHeight
        {
            get { return (int)viewPortSize.Y; }
            set { viewPortSize.Y = value; }
        }

        public static Vector2 Position
        {
            get { return position; }
            set
            {
                position = new Vector2(
                    MathHelper.Clamp(
                        value.X,
                        worldRectangle.X,
                        worldRectangle.Width - ViewPortWidth),
                    MathHelper.Clamp(
                        value.Y,
                        worldRectangle.Y,
                        worldRectangle.Height - ViewPortHeight));
            }
        }

        public static Rectangle WorldRectangle
        {
            get { return worldRectangle; }
            set { worldRectangle = value; }
        }

        public static Rectangle ViewPort => new Rectangle(
            (int)Position.X, (int)Position.Y,
            ViewPortWidth, ViewPortHeight);

        public static void Move(Vector2 offset) => Position += offset;
        public static bool ObjectIsVisible(Rectangle bounds) => ViewPort.Intersects(bounds);

        public static Vector2 Transform(Vector2 point) => point - position;

        public static Rectangle Transform(Rectangle rectangle) => new Rectangle(
            rectangle.Left - (int) position.X,
            rectangle.Top - (int) position.Y,
            rectangle.Width,
            rectangle.Height);
    }
}
