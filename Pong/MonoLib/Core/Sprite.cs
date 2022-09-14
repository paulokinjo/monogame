using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoLib.Core
{
  public abstract class Sprite : DrawableGameComponent
  {
    private int activeFrame;
    private readonly Texture2D texture;
    protected List<Rectangle> Frames { get; set; } = new List<Rectangle>();

    protected Vector2 Position;
    private TimeSpan elapsedTime = TimeSpan.Zero;
    protected Rectangle CurrentFrame { get; set; }
    protected long FrameDelay { get; set; }
    private readonly SpriteBatch spriteBatch;

    public Sprite(Game game, ref Texture2D texture)
      : base(game)
    {
      this.texture = texture;
      activeFrame = 0;

      spriteBatch = Game.Services.GetService<SpriteBatch>();
    }

    public override void Update(GameTime gameTime)
    {
      elapsedTime += gameTime.ElapsedGameTime;
      if (elapsedTime > TimeSpan.FromMilliseconds(FrameDelay))
      {
        elapsedTime -= TimeSpan.FromMilliseconds(FrameDelay);
        activeFrame++;
        if (activeFrame == Frames.Count)
        {
          activeFrame = 0;
        }

        CurrentFrame = Frames[activeFrame];
      }

      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      spriteBatch.Draw(texture, Position, CurrentFrame, Color.White);
      base.Draw(gameTime);
    }

    public virtual bool CheckCollision(Rectangle rectangle)
    {
      Rectangle spriteRectangle = new Rectangle((int)Position.X, (int)Position.Y,
        CurrentFrame.Width, CurrentFrame.Height);

      return spriteRectangle.Intersects(rectangle);
    }
  }
}