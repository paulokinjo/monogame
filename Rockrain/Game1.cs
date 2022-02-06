using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rockrain.Audio;
using Rockrain.Core;
using Rockrain.Scenes;

namespace Rockrain
{
  public class Game1 : Game
  {
    private const int DESIGNED_RESOLUTION_WIDTH = 1280;
    private const int DESIGNED_RESOLUTION_HEIGHT = 720;
    private const float DESIGNED_RESOLUTION_ASPECT_RATIO =
            DESIGNED_RESOLUTION_WIDTH / (float)DESIGNED_RESOLUTION_HEIGHT;

    private GameScene ActiveScene { get; set; }
    private StartScene StartScene { get; set; }
    private HelpScene HelpScene { get; set; }
    private ActionScene ActionScene { get; set; }
    private AudioLibrary AudioLibrary { get; set; }
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private RenderTarget2D _renderTarget;
    private Rectangle _renderScaleRectangle;
    private KeyboardState OldKeyBoardState { get; set; }

    public Game1()
    {
      _graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;

      OldKeyBoardState = Keyboard.GetState();
    }

    protected override void Initialize()
    {

      _graphics.PreferredBackBufferWidth = DESIGNED_RESOLUTION_WIDTH;
      _graphics.PreferredBackBufferHeight = DESIGNED_RESOLUTION_HEIGHT;
      _graphics.IsFullScreen = false;
      _graphics.ApplyChanges();

      _renderTarget = new RenderTarget2D(_graphics.GraphicsDevice, DESIGNED_RESOLUTION_WIDTH, DESIGNED_RESOLUTION_HEIGHT, false,
             SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents);

      _renderScaleRectangle = GetScaleRectangle();

      base.Initialize();
    }

    protected override void LoadContent()
    {
      _spriteBatch = new SpriteBatch(GraphicsDevice);
      Services.AddService<SpriteBatch>(_spriteBatch);

      AudioLibrary = new AudioLibrary();
      AudioLibrary.LoadContent(Content);
      Services.AddService<AudioLibrary>(AudioLibrary);

      var helpBackgroundTexture = Content.Load<Texture2D>("helpbackground");
      var helpForegroundTexture = Content.Load<Texture2D>("helpforeground");
      HelpScene = new HelpScene(this, helpBackgroundTexture, helpForegroundTexture);
      Components.Add(HelpScene);

      var smallFont = Content.Load<SpriteFont>("menuSmall");
      var largeFont = Content.Load<SpriteFont>("menuLarge");
      var startBackgroundTexture = Content.Load<Texture2D>("startbackground");
      var startElementsTexture = Content.Load<Texture2D>("startSceneElements");
      StartScene = new StartScene(this, smallFont, largeFont, startBackgroundTexture, startElementsTexture);
      Components.Add(StartScene);

      var actionElementsTexture = Content.Load<Texture2D>("rockrainenhanced");
      var actionBackgroundTexture = Content.Load<Texture2D>("SpaceBackground");
      var scoreFont = Content.Load<SpriteFont>("score");
      ActionScene = new ActionScene(this, actionElementsTexture, actionBackgroundTexture, scoreFont);
      Components.Add(ActionScene);

      StartScene.Show();
      ActiveScene = StartScene;
    }

    protected override void Update(GameTime gameTime)
    {
      HandleScenesInput();
      base.Update(gameTime);
    }

    private void HandleScenesInput()
    {
      if (ActiveScene == StartScene)
      {
        HandleStartSceneInput();
      }
      else if (ActiveScene == HelpScene)
      {
        if (CheckEnterA())
        {
          ShowScene(StartScene);
        }
      }
      else if (ActiveScene == ActionScene)
      {
        HandleActionInput();
      }
    }

    private void HandleStartSceneInput()
    {
      if (CheckEnterA())
      {
        AudioLibrary.MenuSelect.Play();
        switch (StartScene.SelectedMenuIndex)
        {
          case 0:
            ActionScene.TwoPlayers = false;
            ShowScene(ActionScene);
            break;
          case 1:
            ActionScene.TwoPlayers = true;
            ShowScene(ActionScene);
            break;
          case 2:
            ShowScene(HelpScene);
            break;
          case 3:
            Exit();
            break;
        }
      }
    }

    private void HandleActionInput()
    {
      KeyboardState keyboardState = Keyboard.GetState();

      bool backKey = (OldKeyBoardState.IsKeyDown(Keys.Escape) &&
        keyboardState.IsKeyUp(Keys.Escape));

      bool enterKey = (OldKeyBoardState.IsKeyDown(Keys.Enter) &&
      keyboardState.IsKeyUp(Keys.Enter));

      OldKeyBoardState = keyboardState;

      if (enterKey)
      {
        if (ActionScene.GameOver)
        {
          ShowScene(StartScene);
        }
        else
        {
          AudioLibrary.MenuBack.Play();
          ActionScene.Paused = !ActionScene.Paused;
        }
      }

      if (backKey)
      {
        ShowScene(StartScene);
      }
    }

    private void ShowScene(GameScene scene)
    {
      ActiveScene.Hide();      
      ActiveScene = scene;
      scene.Show();
    }
    private bool CheckEnterA()
    {
      KeyboardState keyboardState = Keyboard.GetState();

      bool result = (OldKeyBoardState.IsKeyDown(Keys.Enter) &&
        keyboardState.IsKeyUp(Keys.Enter));

      OldKeyBoardState = keyboardState;
      return result;
    }
    protected override void Draw(GameTime gameTime)
    {
      _graphics.GraphicsDevice.SetRenderTarget(_renderTarget);
      _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
      _graphics.GraphicsDevice.SetRenderTarget(null);
      _graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Black, 1.0f, 0);

      _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque);
      _spriteBatch.Draw(_renderTarget, _renderScaleRectangle, Color.White);
      _spriteBatch.End();

      _spriteBatch.Begin();
      base.Draw(gameTime);
      _spriteBatch.End();
    }

    private Rectangle GetScaleRectangle()
    {
      var variance = 0.5;
      var actualAspectRatio = Window.ClientBounds.Width / (float)Window.ClientBounds.Height;

      Rectangle scaleRectangle;

      if (actualAspectRatio <= DESIGNED_RESOLUTION_ASPECT_RATIO)
      {
        var presentHeight = (int)(Window.ClientBounds.Width / DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
        var barHeight = (Window.ClientBounds.Height - presentHeight) / 2;

        scaleRectangle = new Rectangle(0, barHeight, Window.ClientBounds.Width, presentHeight);
      }
      else
      {
        var presentWidth = (int)(Window.ClientBounds.Height * DESIGNED_RESOLUTION_ASPECT_RATIO + variance);
        var barWidth = (Window.ClientBounds.Width - presentWidth) / 2;

        scaleRectangle = new Rectangle(barWidth, 0, presentWidth, Window.ClientBounds.Height);
      }

      return scaleRectangle;
    }
  }
}
