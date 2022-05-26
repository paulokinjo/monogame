using Frogger.Fsm;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frogger.States
{
  public class GameState : BaseState
  {
    private SpriteBatch spriteBatch;
    private Texture2D blocks;

    public GameState(StateMachine stateMachine)
    {
      spriteBatch = new SpriteBatch(stateMachine.Game.GraphicsDevice);
      blocks = stateMachine.Game.Content.Load<Texture2D>("blocks");
    }

    public override void Draw()
    {
        spriteBatch.Begin();
        spriteBatch.Draw(blocks, Vector2.Zero, Color.White);
        spriteBatch.End();
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Update(float deltaTime)
    {
    }
  }
}