using GameEngine.Enum;
using GameEngine.States;
using GameEngine.States.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
  public class MainGame : Game
  {
    private const int DESIGNED_RESOLUTION_WIDTH = 1280;
    private const int DESIGNED_RESOLUTION_HEIGHT = 720;
    private const float DESIGNED_RESOLUTION_ASPECT_RATIO =
        DESIGNED_RESOLUTION_WIDTH / (float)DESIGNED_RESOLUTION_HEIGHT;

    private BaseGameState _currentGameState;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private RenderTarget2D _renderTarget;
    private Rectangle _renderScaleRectangle;

    public MainGame()
    {
      _graphics = new GraphicsDeviceManager(this);



      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize()
    {

      _graphics.PreferredBackBufferWidth = DESIGNED_RESOLUTION_WIDTH;
      _graphics.PreferredBackBufferHeight = DESIGNED_RESOLUTION_HEIGHT;
      _graphics.IsFullScreen = false;
      _graphics.ApplyChanges();

      _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice,
          DESIGNED_RESOLUTION_WIDTH, DESIGNED_RESOLUTION_HEIGHT,
          false, SurfaceFormat.Color, DepthFormat.None, 0,
          RenderTargetUsage.DiscardContents);

      _renderScaleRectangle = GetScaleRectangle();

      SwitchGameState(new GameplayState());

      base.Initialize();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);

      // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
      _currentGameState.HandleInput();

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.SetRenderTarget(_renderTarget);
      GraphicsDevice.Clear(Color.CornflowerBlue);

      GraphicsDevice.SetRenderTarget(null);
      GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

      _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
      _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);
      _spriteBatch.End();


      base.Draw(gameTime);
    }

    private void SwitchGameState(BaseGameState gameState)
    {
      _currentGameState?.UnloadContent(Content);
      _currentGameState = gameState;
      _currentGameState.LoadContent(Content);

      _currentGameState.OnStateSwitched += CurrentGameState_OnStateSwitched;
      _currentGameState.OnEventNotification += CurrentGameState_OnEventNotification;
    }

    private void CurrentGameState_OnEventNotification(object sender, Events e)
    {
      switch (e)
      {
        case Events.GAME_QUIT:
          Exit();
          break;
      }
    }

    private void CurrentGameState_OnStateSwitched(object sender, BaseGameState e) =>
        SwitchGameState(e);

    private Rectangle GetScaleRectangle()
    {
      var variance = 0.5;
      var actualAspectRatio = Window.ClientBounds.Width /
          (float)Window.ClientBounds.Height;

      Rectangle scaleRectangle;


      if (actualAspectRatio <= DESIGNED_RESOLUTION_ASPECT_RATIO)
      {
        var presentHeight = (int)(Window.ClientBounds.Width /
            DESIGNED_RESOLUTION_ASPECT_RATIO + variance);

        var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

        scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
      }
      else
      {
        var presentWidth = (int)(Window.ClientBounds.Height *
            DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
        var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

        scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
      }

      return scaleRectangle;
    }
  }
}
