using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLib.Audio;
using MonoLib.Scenes;
using MonoLib.Utils;
using MonoPong.Scenes;

namespace MonoPong
{
  public class MonoPong : Game
  {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private RenderTarget2D renderTarget;
    private Rectangle renderScaleRectangle;
    private GameScene activeScene;
    private StartScene startScene;
    private ActionScene actionScene;
    private ScenesManager scenesManager;

    public MonoPong()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize()
    {
      graphics.PreferredBackBufferWidth = Constants.ResolutionWidth;
      graphics.PreferredBackBufferHeight = Constants.ResolutionHeight;
      graphics.IsFullScreen = false;

      graphics.ApplyChanges();

      renderTarget = new RenderTarget2D(
        graphics.GraphicsDevice,
        Constants.ResolutionWidth,
        Constants.ResolutionHeight,
        false,
        SurfaceFormat.Color,
        DepthFormat.None,
        0,
        RenderTargetUsage.DiscardContents);

      renderScaleRectangle = GetScaleRectangle();

      base.Initialize();
    }

    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);
      Services.AddService<SpriteBatch>(spriteBatch);

      var audioLibrary = new AudioLibrary();
      audioLibrary.LoadContent(Content);
      Services.AddService<AudioLibrary>(audioLibrary);

      startScene = new StartScene(this);
      Components.Add(startScene);

      actionScene = new ActionScene(this);
      Components.Add(actionScene);

      scenesManager = new ScenesManager(this, startScene, actionScene);
      scenesManager.SetActiveScene(startScene);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      scenesManager.HandleInputs();

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      graphics.GraphicsDevice.SetRenderTarget(renderTarget);
      graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
      graphics.GraphicsDevice.SetRenderTarget(null);
      graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

      spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
      spriteBatch.Draw(renderTarget, renderScaleRectangle, Color.White);
      spriteBatch.End();

      spriteBatch.Begin();
      base.Draw(gameTime);
      spriteBatch.End();
    }

    private Rectangle GetScaleRectangle()
    {
      var variance = 0.5;
      var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

      Rectangle scaleRectangle;

      if (actualAspectRatio <= Constants.ResolutionAspectRatio)
      {
        var presentHeight = (int)(Window.ClientBounds.Width / Constants.ResolutionAspectRatio + variance);
        var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

        scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
      }
      else
      {
        var presentWidth = (int)(Window.ClientBounds.Height * Constants.ResolutionAspectRatio + variance);
        var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

        scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
      }

      return scaleRectangle;
    }
  }
}
