using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rpg
{
  public class SplashScreen : GameScreen
  {
    private readonly SpriteBatch spriteBatch;

    private Texture2D image;

    public SplashScreen(Game game)
        : base(game)
    {
      spriteBatch = game.Services.GetService<SpriteBatch>();
    }

    protected override void LoadContent()
    {
      base.LoadContent();
      image = Game.Content.Load<Texture2D>(@"SplashScreen");
    }

    public override void Draw(GameTime gameTime)
    {
        spriteBatch.Draw(image, Vector2.Zero, Color.White);
        base.Draw(gameTime);
    }
  }
}