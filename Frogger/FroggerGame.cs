using Frogger.Components;
using Frogger.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Frogger
{
  public class FroggerGame : Game
  {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;

    public FroggerGame()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";
      IsMouseVisible = true;
    }

    protected override void Initialize()
    {
      var stateComponent = new StateComponent(this);
      stateComponent.StateMachine.Add("game",
          new GameState(stateComponent.StateMachine));
      stateComponent.StateMachine.Change("game");
      Components.Add(stateComponent);

      base.Initialize();
    }

    protected override void LoadContent()
    {
      spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      GraphicsDevice.Clear(Color.Black);

      base.Draw(gameTime);
    }
  }
}
