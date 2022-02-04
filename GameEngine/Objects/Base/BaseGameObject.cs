using GameEngine.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Objects.Base
{
  public class BaseGameObject
  {
    protected Texture2D _texture;
    public int zIndex { get; }

    public void Render(SpriteBatch spriteBatch)
      => spriteBatch.Draw(_texture, Vector2.One, Color.White);

    public virtual void OnNotify(Events eventType) 
    { 
    }

  }
}