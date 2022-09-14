using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLib.Scenes;
using MonoLib.Scenes.Menu;

namespace MonoPong.Scenes
{
  public class StartScene : GameScene
  {
    private readonly TextMenuComponent textMenuComponent;
    private readonly SpriteBatch spriteBatch;

    public int SelectedMenuItem => textMenuComponent.SelectedMenuItem;

    public StartScene(Game game) : base(game)
    {
      var smallFont = game.Content.Load<SpriteFont>("menu");
      var largeFont = game.Content.Load<SpriteFont>("menu");

      textMenuComponent = new TextMenuComponent(game, smallFont, largeFont);
      textMenuComponent.SetMenuItems("Start", "Exit");

      Components.Add(textMenuComponent);

      spriteBatch = game.Services.GetService<SpriteBatch>();
    }

    public override void Draw(GameTime gameTime)
    {
      base.Draw(gameTime);
    }
  }
}