using Microsoft.Xna.Framework;

namespace MonoPong.Utils
{
  public static class Constants
  {
    public static readonly Rectangle
        GameLivesRect = new Rectangle(0, 222, 100, 34),
        GameRedWonRect = new Rectangle(151, 222, 155, 34),
        GameBlueWonRect = new Rectangle(338, 222, 165, 34),
        GameRedPaddleRect = new Rectangle(23, 0, 22, 92),
        GameBluePaddleRect = new Rectangle(0, 0, 22, 92),
        GameBallRect = new Rectangle(1, 94, 33, 33),
        GameSmallBallRect = new Rectangle(37, 108, 19, 19);

    public static readonly int GameLivesPaddingTop = 9;
  }
}