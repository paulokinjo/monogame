using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rockrain.Core
{
  public class Score : DrawableGameComponent
  {
    private SpriteBatch SpriteBatch { get; }
    public Vector2 Position { get; set; } = new Vector2();

    public int Value { get; set; }
    public int Power { get; set; }

    private SpriteFont Font { get; }
    private Color FontColor { get; }

    public Score(Game game, SpriteFont font, Color fontColor)
      : base(game)
    {
      Font = font;
      FontColor = fontColor;

      SpriteBatch = Game.Services.GetService<SpriteBatch>();
    }

    public override void Draw(GameTime gameTime)
    {
      string textToDraw = $"Score: {Value}";

      SpriteBatch.DrawString(Font, textToDraw, new Vector2(Position.X + 1, Position.Y + 1), Color.Black);
      SpriteBatch.DrawString(Font, textToDraw, new Vector2(Position.X, Position.Y), FontColor);

      float height = Font.MeasureString(textToDraw).Y;
      textToDraw = $"Power: {Power}";

      SpriteBatch.DrawString(Font, textToDraw, new Vector2(Position.X + 1, Position.Y + 1 + height), Color.Black);
      SpriteBatch.DrawString(Font, textToDraw, new Vector2(Position.X, Position.Y + 1 + height), Color.Black);

      base.Draw(gameTime);
    }
  }
}