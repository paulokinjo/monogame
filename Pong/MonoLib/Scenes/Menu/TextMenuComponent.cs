using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoLib.Audio;

namespace MonoLib.Scenes.Menu
{
  public class TextMenuComponent : DrawableGameComponent
  {
    private readonly SpriteBatch spriteBatch;
    private readonly AudioLibrary audioLibrary;
    private readonly SpriteFont regularFont;
    private readonly SpriteFont selectedFont;
    private readonly List<string> menuItems = new List<string>();    
    public int SelectedMenuItem { get; private set; }
    private Color regularColor;
    private Color selectedColor;
    private KeyboardState prevKeyboardState;
    private Vector2 position;
    private int width;
    private int height;

    public TextMenuComponent(
        Game game,
        SpriteFont regularFont,
        SpriteFont selectedFont)
        : base(game)
    {
      this.regularFont = regularFont;
      this.selectedFont = selectedFont;

      regularColor = Color.White;
      selectedColor = Color.Red;

      spriteBatch = game.Services.GetService<SpriteBatch>();
      audioLibrary = game.Services.GetService<AudioLibrary>();

      prevKeyboardState = Keyboard.GetState();
    }

    public void SetMenuItems(params string[] items)
    {
      menuItems.Clear();
      menuItems.AddRange(items);
      CalculateBounds();
    }

    public override void Update(GameTime gameTime)
    {
      var keyboardState = Keyboard.GetState();
      var down = prevKeyboardState.IsKeyDown(Keys.Down) &&
                 keyboardState.IsKeyUp(Keys.Down);
      bool up = prevKeyboardState.IsKeyDown(Keys.Up) &&
              keyboardState.IsKeyUp(Keys.Up);

      if (down || up) 
      {
        audioLibrary.MenuScroll.Play();
      }  

      if (down)
      {
        SelectedMenuItem = SelectedMenuItem + 1;
        if (SelectedMenuItem == menuItems.Count)
        {
          SelectedMenuItem = 0;
        }
      }
      else if (up)
      {
        SelectedMenuItem = SelectedMenuItem - 1;
        if (SelectedMenuItem == -1) 
        {
          SelectedMenuItem = menuItems.Count - 1;
        }
      }

      prevKeyboardState = keyboardState;
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      if (position == null)
      {
        throw new Exception("TextMenuComponent (position) not initialized.");
      }

      var yPos = position.Y;
      for (var i = 0; i < menuItems.Count; i++)
      {
        SpriteFont font;
        Color color;

        if (i == SelectedMenuItem)
        {
          font = selectedFont;
          color = selectedColor;
        }
        else
        {
          font = regularFont;
          color = regularColor;
        }

        spriteBatch.DrawString(font, menuItems[i], new Vector2(position.X + 1, yPos + 1), Color.Black);
        spriteBatch.DrawString(font, menuItems[i], new Vector2(position.X, yPos), color);

        yPos += font.LineSpacing;
      }

      base.Draw(gameTime);
    }

    private void CalculateBounds()
    {
      width = 0;
      height = 0;
      foreach (var item in menuItems)
      {
        var size = selectedFont.MeasureString(item);
        if (size.X > width)
        {
          width = (int)size.X;
        }

        height += selectedFont.LineSpacing;
      }

      position = new Vector2((Game.Window.ClientBounds.Width - width) / 2, 330);
    }
  }
}