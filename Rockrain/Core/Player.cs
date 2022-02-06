using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rockrain.Core
{
  public class Player : DrawableGameComponent
  {
    private SpriteBatch SpriteBatch { get; }
    private Texture2D Texture { get; }
    private Rectangle SpriteRectangle { get; }
    private Vector2 Position;
    private TimeSpan ElapsedTime { get; set; }
    private PlayerIndex PlayerIndex { get; set; }
    private Rectangle ScreenBounds { get; set; }

    private int _score;
    public int Score
    {
      get { return _score; }
      set
      {
        if (value < 0)
        {
          _score = 0;
        }
        else
        {
          _score = value;
        }
      }
    }
    public int Power { get; set; }
    private const int INITIAL_POWER = 100;

    public Player(Game game, ref Texture2D texture, PlayerIndex playerIndex, Rectangle rectangle)
      : base(game)
    {
      Texture = texture;
      Position = new Vector2();
      PlayerIndex = playerIndex;

      SpriteRectangle = rectangle;

      ScreenBounds = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);

      SpriteBatch = Game.Services.GetService<SpriteBatch>();
    }

    public void Reset()
    {
      if (PlayerIndex == PlayerIndex.One)
      {
        Position.X = ScreenBounds.Width / 3;
      }
      else
      {
        Position.X = (int)(ScreenBounds.Width / 1.5);
      }

      Position.Y = ScreenBounds.Height - SpriteRectangle.Height;
      Score = 0;
      Power = INITIAL_POWER;
    }

    public override void Update(GameTime gameTime)
    {
      if (PlayerIndex == PlayerIndex.One)
      {
        HandlePlayer1KeyBoard();
      }
      else
      {
        HandlePlayer2KeyBoard();
      }

      KeepInBound();

      ElapsedTime += gameTime.ElapsedGameTime;
      if (ElapsedTime > TimeSpan.FromSeconds(1))
      {
        ElapsedTime -= TimeSpan.FromSeconds(1);
        Score++;
        Power--;
      }

      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      SpriteBatch.Draw(Texture, Position, SpriteRectangle, Color.White);
      base.Draw(gameTime);
    }

    public Rectangle GetBounds()
    {
      return new Rectangle((int)Position.X, (int)Position.Y,
        SpriteRectangle.Width, SpriteRectangle.Height);
    }

    private void KeepInBound()
    {
      if (Position.X < ScreenBounds.Left)
      {
        Position.X = ScreenBounds.Left;
      }

      if (Position.X > ScreenBounds.Width - SpriteRectangle.Width)
      {
        Position.X = ScreenBounds.Width - SpriteRectangle.Width;
      }

      if (Position.Y < ScreenBounds.Top)
      {
        Position.Y = ScreenBounds.Top;
      }

      if (Position.Y > ScreenBounds.Height - SpriteRectangle.Height)
      {
        Position.Y = ScreenBounds.Height - SpriteRectangle.Height;
      }
    }

    private void HandlePlayer1KeyBoard()
    {
      KeyboardState keyboard = Keyboard.GetState(); if (keyboard.IsKeyDown(Keys.Up))
      {
        Position.Y -= 3;
      }
      if (keyboard.IsKeyDown(Keys.Down))
      {
        Position.Y += 3;
      }
      if (keyboard.IsKeyDown(Keys.Left))
      {
        Position.X -= 3;
      }
      if (keyboard.IsKeyDown(Keys.Right))
      {
        Position.X += 3;
      }
    }

    private void HandlePlayer2KeyBoard()
    {
      KeyboardState keyboard = Keyboard.GetState(); if (keyboard.IsKeyDown(Keys.W))
      {
        Position.Y -= 3;
      }
      if (keyboard.IsKeyDown(Keys.S))
      {
        Position.Y += 3;
      }
      if (keyboard.IsKeyDown(Keys.A))
      {
        Position.X -= 3;
      }
      if (keyboard.IsKeyDown(Keys.D))
      {
        Position.X += 3;
      }
    }

  }
}