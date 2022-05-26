using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Frogger.Fsm
{
  public class StateMachine
  {
    private readonly Dictionary<string, IState> states = new Dictionary<string, IState>();
    private IState currentState;

    public Game Game { get; }

    public StateMachine(Game game) => Game = game;

    public void Add(string stateName, IState state)
    {
      states[stateName] = state;
    }

    public void Change(string stateName)
    {
      if (!states.ContainsKey(stateName))
      {
        throw new KeyNotFoundException($"{stateName} is not valid state!");
      }

      if (currentState != null)
      {
        currentState.Exit();
      }

      currentState = states[stateName];
      currentState.Enter();
    }

    public void Draw()
    {
      if(currentState != null)
      {
        currentState.Draw();
      }
    }

    public void Update(float deltaTime)
    {
      if(currentState != null)
      {
        currentState.Update(deltaTime);
      }
    }

  }
}