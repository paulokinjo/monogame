using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLib.Core;
using MonoPong.Utils;

namespace MonoPong.Player
{
  public class Paddle : Sprite
  {
    public Paddle(Game game, ref Texture2D texture, Vector2 position, Rectangle rectangle) 
        : base(game, ref texture)
    {
        Frames.Add(rectangle);
        Position = position;
    }
  }
}