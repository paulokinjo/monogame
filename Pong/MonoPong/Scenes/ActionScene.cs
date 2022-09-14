using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLib.Core;
using MonoLib.Scenes;
using MonoPong.Core;
using MonoPong.Player;
using MonoPong.Utils;

namespace MonoPong.Scenes
{
  public class ActionScene : GameScene
  {
    private PlayerManager playerManager;
    Texture2D backgroundTexture, gameTexture;
    private readonly SpriteBatch spriteBatch;
    private Ball ball;

    public ActionScene(Game game)
        : base(game)
    {
    }

    protected override void LoadContent()
    {
      var contentManager = Game.Content;
      backgroundTexture = contentManager.Load<Texture2D>(@"Textures/SpaceBackground");
      gameTexture = contentManager.Load<Texture2D>(@"Textures/PongGame");

      Components.Add(new ImageComponent(Game, backgroundTexture,
          ImageComponent.DrawMode.Stretch));

      playerManager = new PlayerManager(Game, ref gameTexture);
      Components.Add(playerManager);

      ball = new Ball(Game, ref gameTexture);
      Components.Add(ball);

      base.LoadContent();
    }

    public override void Show()
    {
      playerManager.Initialize();
      base.Show();
    }
  }
}