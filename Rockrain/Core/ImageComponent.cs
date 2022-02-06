using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rockrain.Core
{
  public class ImageComponent : DrawableGameComponent
  {
    public enum DrawMode
    {
      Center = 1,
      Stretch
    };

    protected readonly Texture2D texture;
    protected readonly DrawMode drawMode;

    protected SpriteBatch spriteBatch;
    protected Rectangle imageRect;

    public ImageComponent(Game game, Texture2D texture, DrawMode drawMode)
      : base(game)
    {
      this.texture = texture;
      this.drawMode = drawMode;

      spriteBatch = (SpriteBatch)game.Services.GetService<SpriteBatch>();

      switch (drawMode)
      {
        case DrawMode.Center:
          imageRect = new Rectangle((game.Window.ClientBounds.Width - texture.Width) / 2,
                  (game.Window.ClientBounds.Height - texture.Height) / 2,
                  texture.Width, texture.Height);
          break;
        case DrawMode.Stretch:
          imageRect = new Rectangle(0, 0, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
          break;
      }
    }

    public override void Draw(GameTime gameTime)
    {
      spriteBatch.Draw(texture, imageRect, Color.White);
      base.Draw(gameTime);
    }
  }
}