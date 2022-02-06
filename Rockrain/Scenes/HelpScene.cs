using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rockrain.Core;

namespace Rockrain.Scenes
{
  public class HelpScene : GameScene
  {
    public HelpScene(Game game, Texture2D background, Texture2D foreground) 
      : base(game)
    {
      Components.Add(new ImageComponent(game, background, ImageComponent.DrawMode.Stretch));
      Components.Add(new ImageComponent(game, foreground, ImageComponent.DrawMode.Center));
    }
  }
}