using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rpg
{
  public class ScreenManager : DrawableGameComponent
  {
    private static ScreenManager instance;
    private GameScreen currentScreen;

    public Vector2 Dimensions { get; private set; }


    public static ScreenManager GetInstance(Game game)
    {
      if (instance == null)
      {
        instance = new ScreenManager(game);
      }
      return instance;
    }

    private ScreenManager(Game game)
        : base(game)
    {
      Dimensions = new Vector2(640, 480);
      currentScreen = new SplashScreen(game);
    }

    public override void Initialize()
    {
      currentScreen.Initialize();
      base.Initialize();
    }

    public override void Draw(GameTime gameTime)
    {
      currentScreen.Draw(gameTime);
      base.Draw(gameTime);
    }
  }
}