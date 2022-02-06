using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rockrain.Core
{
  public class PowerSource : Sprite
  {
    private Texture2D Texture { get; }
    private Random _random;
    public PowerSource(Game game, ref Texture2D texture)
      : base(game, ref texture)
    {
      Texture = texture;

      Rectangle frame = new Rectangle();
      frame.X = 291;
      frame.Y = 17;
      frame.Width = 14;
      frame.Height = 12;
      Frames.Add(frame);

      frame.Y = 30;
      Frames.Add(frame);

      frame.Y = 43;
      Frames.Add(frame);

      frame.Y = 57;
      Frames.Add(frame);

      frame.Y = 70;
      Frames.Add(frame);

      frame.Y = 82;
      Frames.Add(frame);

      FrameDelay = 200;

      _random = new Random(GetHashCode());
      PutInStartPosition();
    }

    public override void Update(GameTime gameTime)
    {
      if (Position.Y >= Game.Window.ClientBounds.Height)
      {
        Position.Y = 0;
        Enabled = false;
      }

      Position.Y += 1;

      base.Update(gameTime);
    }

    public void PutInStartPosition()
    {
      Position.X = _random.Next(Game.Window.ClientBounds.Width - CurrentFrame.Width);
      Position.Y = -10;
      Enabled = false;
    }
  }
}