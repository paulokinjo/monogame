namespace Frogger.Fsm
{
  public interface IState
  {
    void Update(float deltaTime);
    void Draw();
    void Enter();
    void Exit();
  }
}