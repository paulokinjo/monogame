using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rpg
{
  public class Game1 : Game
  {
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ScreenManager screenManager;

    public Game1()
    {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize()
    {
      base.Initialize();

      screenManager = ScreenManager.GetInstance(this);
      screenManager.Initialize();

      _graphics.PreferredBackBufferWidth = (int)screenManager.Dimensions.X;
      _graphics.PreferredBackBufferHeight = (int)screenManager.Dimensions.Y;
      _graphics.ApplyChanges();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
      Services.AddService<SpriteBatch>(_spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      // TODO: Add your update logic here

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Black);

      _spriteBatch.Begin();
      screenManager.Draw(gameTime);
      _spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
