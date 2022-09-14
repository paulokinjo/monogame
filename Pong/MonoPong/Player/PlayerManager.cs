using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoPong.Core;
using MonoPong.Utils;

namespace MonoPong.Player
{
  public class PlayerManager : DrawableGameComponent
  {
    private Texture2D gameTexture;
    private Paddle player1;
    private Lives player1Lives;
    private float player1Position = 0.5f;
    private int player1LivesRemaining = 3;

    private Paddle player2;
    float player2Position = 0.5f;
    int player2LivesRemaining = 3;
    private Lives player2Lives;

    public PlayerManager(Game game, ref Texture2D gameTexture)
        : base(game)
    {
      this.gameTexture = gameTexture;
    }

    public override void Initialize()
    {
      player1 = new Paddle(
            Game,
            ref gameTexture,
            new Vector2(
            (0.05f * MonoLib.Utils.Constants.ResolutionWidth) - Constants.GameRedPaddleRect.Width / 2,
            ((0.06f + 0.88f * player1Position) * MonoLib.Utils.Constants.ResolutionHeight) - Constants.GameRedPaddleRect.Height / 2),
            Constants.GameRedPaddleRect);
      Game.Components.Add(player1);

      player1Lives = new Lives(Game, ref gameTexture, PlayerType.One);
      Game.Components.Add(player1Lives);

      player2 = new Paddle(
        Game,
        ref gameTexture,
        new Vector2((int)(0.95f * MonoLib.Utils.Constants.ResolutionWidth) - Constants.GameBluePaddleRect.Width / 2,
        (int)((0.06f + 0.88f * player2Position) * MonoLib.Utils.Constants.ResolutionHeight) - Constants.GameBluePaddleRect.Height / 2),
        Constants.GameBluePaddleRect);
      Game.Components.Add(player2);

      player2Lives = new Lives(Game, ref gameTexture, PlayerType.Two);
      Game.Components.Add(player2Lives);

      base.Initialize();
    }

    public override void Draw(GameTime gameTime)
    {
      player1.Draw(gameTime);
      for (int lives = 0; lives < player1Lives.LivesRemaining; lives++)
      {
        player1Lives.SetPosition(lives);
        player1Lives.Draw(gameTime);
      }

      player2.Draw(gameTime);
      for (int lives = 0; lives < player2Lives.LivesRemaining; lives++)
      {
        player2Lives.SetPosition(lives);
        player2Lives.Draw(gameTime);
      }

      base.Draw(gameTime);
    }
  }
}