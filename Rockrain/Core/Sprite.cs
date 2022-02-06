using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rockrain.Core
{
  public abstract class Sprite : DrawableGameComponent
  {
    private int _activeFrame;
    private readonly Texture2D _texture;
    protected List<Rectangle> Frames { get; set; } = new List<Rectangle>();

    protected Vector2 Position;
    private TimeSpan _elapsedTime = TimeSpan.Zero;
    protected Rectangle CurrentFrame { get; set; }
    protected long FrameDelay { get; set; }
    private readonly SpriteBatch _spriteBatch;

    public Sprite(Game game, ref Texture2D texture)
      : base(game)
    {
      _texture = texture;
      _activeFrame = 0;

      _spriteBatch = Game.Services.GetService<SpriteBatch>();
    }

    public override void Update(GameTime gameTime)
    {
      _elapsedTime += gameTime.ElapsedGameTime;
      if (_elapsedTime > TimeSpan.FromMilliseconds(FrameDelay))
      {
        _elapsedTime -= TimeSpan.FromMilliseconds(FrameDelay);
        _activeFrame++;
        if (_activeFrame == Frames.Count)
        {
          _activeFrame = 0;
        }

        CurrentFrame = Frames[_activeFrame];
      }

      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      _spriteBatch.Draw(_texture, Position, CurrentFrame, Color.White);
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