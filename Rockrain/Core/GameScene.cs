using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Rockrain.Core
{
  public class GameScene : DrawableGameComponent
  {
    public List<GameComponent> Components { get; }

    public GameScene(Game game)
      : base(game)
    {
      Components = new List<GameComponent>();
      Hide();
    }

    public virtual void Show()
    {
      Visible = true;
      Enabled = true;
    }

    public virtual void Hide()
    {
      Visible = false;
      Enabled = false;
    }

    public override void Update(GameTime gameTime)
    {
      for (int i = 0; i < Components.Count; i++)
      {
        if (Components[i].Enabled)
        {
          Components[i].Update(gameTime);
        }
      }

      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      for (int i = 0; i < Components.Count; i++)
      {
        var gc = Components[i] as DrawableGameComponent;
        if (gc is DrawableGameComponent && gc.Visible)
        {
          gc.Draw(gameTime);
        }
      }

      base.Draw(gameTime);
    }
  }
}