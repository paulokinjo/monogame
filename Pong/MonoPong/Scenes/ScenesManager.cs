using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoLib.Audio;
using MonoLib.Scenes;

namespace MonoPong.Scenes
{
  public class ScenesManager
  {
    private readonly Game game;
    private readonly StartScene startScene;
    private readonly ActionScene actionScene;
    private GameScene activeScene { get; set; }

    private readonly AudioLibrary audioLibrary;
    private KeyboardState prevKeyboardState;
    private GameScene currentGameScene;

    public ScenesManager(
        Game game,
        StartScene startScene,
        ActionScene actionScene)
    {
      this.game = game;
      this.startScene = startScene;
      this.actionScene = actionScene;

      prevKeyboardState = Keyboard.GetState();
      audioLibrary = game.Services.GetService<AudioLibrary>();
    }

    public void SetActiveScene(GameScene scene)
    {
      activeScene = scene;
      activeScene.Show();
    }

    public void HandleInputs()
    {
      if (activeScene == startScene)
      {
        HandleStartSceneInputs();
      }
    }

    private void HandleStartSceneInputs()
    {
      if (IsEnterPressed())
      {
        audioLibrary.MenuSelect.Play();
        switch (startScene.SelectedMenuItem)
        {
          case 0:
            ShowScene(actionScene);
            break;
          case 1:
          default:
            game.Exit();
            break;
        }
      }
    }

    private bool IsEnterPressed()
    {
      var keyboardState = Keyboard.GetState();
      var isEnterPressed = prevKeyboardState.IsKeyDown(Keys.Enter) &&
                           keyboardState.IsKeyUp(Keys.Enter);

      prevKeyboardState = keyboardState;

      return isEnterPressed;
    }

    private void ShowScene(GameScene scene)
    {
      activeScene.Hide();      
      activeScene = scene;
      scene.Initialize();
      scene.Show();
    }
  }
}