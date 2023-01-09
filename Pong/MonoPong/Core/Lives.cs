using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoLib.Core;
using MonoPong.Player;
using MonoPong.Utils;

namespace MonoPong.Core
{
  public class Lives : Sprite
  {
    private readonly PlayerType playerType;
    private readonly SpriteBatch spriteBatch;
    private readonly Texture2D menuTexture;

    public int LivesRemaining { get; private set; }

    public Lives(Game game, ref Texture2D texture, PlayerType playerType)
        : base(game, ref texture)
    {
      Frames.Add(Constants.GameSmallBallRect);
      this.playerType = playerType;

      LivesRemaining = 3;
      for (int life = 0; life < LivesRemaining; life++)
      {
        SetPosition(life);
      }

      spriteBatch = game.Services.GetService<SpriteBatch>();

      menuTexture = game.Content.Load<Texture2D>(@"Textures/PongMenu");
    }

    private int rightPosition => (int)(MonoLib.Utils.Constants.ResolutionWidth -
                            Constants.GameLivesRect.Width * 1.6);

    public override void Draw(GameTime gameTime)
    {
      if (playerType == PlayerType.One)
      {
        spriteBatch.Draw(menuTexture, new Vector2(2, 2), Constants.GameLivesRect, Color.White);
      }
      else
      {
        spriteBatch.Draw(menuTexture, new Vector2(rightPosition, 2), Constants.GameLivesRect, Color.White);
      }
      base.Draw(gameTime);
    }

    public void SetPosition(int life)
    {
      if (playerType == PlayerType.One)
      {
        Position = new Vector2(
            2 + Constants.GameLivesRect.Width + Constants.GameSmallBallRect.Width *
            life - 2,
            Constants.GameLivesPaddingTop
        );
      }
      else
      {
        Position = new Vector2(
            rightPosition +
            Constants.GameLivesRect.Width +
            Constants.GameSmallBallRect.Width * life - 2,
            Constants.GameLivesPaddingTop
        );
      }
    }
  }
}