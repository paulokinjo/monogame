using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLib.Core;
using MonoPong.Utils;

namespace MonoPong.Core
{
  public class Ball : Sprite
  {
    /// <summary>
    /// Current ball position, from 0 to 1, 0 is left and top,
    /// 1 is bottom and right.
    /// </summary>
    private Vector2 ballPosition = new Vector2(0.5f, 0.5f);
    public Ball(Game game, ref Texture2D texture)
    : base(game, ref texture)
    {
      Frames.Add(Constants.GameBallRect);

      Position = new Vector2(
        (int)((0.05f + 0.9f * ballPosition.X) * MonoLib.Utils.Constants.ResolutionWidth) - Constants.GameBallRect.Width / 2,
        (int)((0.02f + 0.96f * ballPosition.Y) * MonoLib.Utils.Constants.ResolutionHeight) - Constants.GameBallRect.Height / 2);
    }
  }
}