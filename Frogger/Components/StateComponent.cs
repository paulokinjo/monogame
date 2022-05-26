using Frogger.Fsm;
using Microsoft.Xna.Framework;

namespace Frogger.Components
{
  public class StateComponent : DrawableGameComponent
  {
    public StateMachine StateMachine { get; }

    public StateComponent(Game game)
        : base(game) =>
        StateMachine = new StateMachine(game);

    public override void Update(GameTime gameTime) 
    {
        var deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000f;
        StateMachine.Update(deltaTime);
    }
        
    public override void Draw(GameTime gameTime) 
    {
        StateMachine.Draw();
    }
  }
}