using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rockrain.Audio;

namespace Rockrain.Core
{
  public class TextMenuComponent : DrawableGameComponent
  {
    private readonly SpriteBatch _spriteBatch;
    private readonly SpriteFont _regularFont;
    private readonly SpriteFont _selectedFont;

    public Color RegularColor { get; set; } = Color.White;
    public Color SelectedColor { get; set; } = Color.Red;
    public Vector2 Position { get; set; } = new Vector2();

    public int SelectedIndex { get; set; }
    private readonly List<string> _menuItems;
    public int Width { get; private set; }
    public int Height { get; private set; }
    private KeyboardState _oldKeyboardState;
    private readonly AudioLibrary _audioLibrary;
    public TextMenuComponent(Game game, SpriteFont regularFont, SpriteFont selectedFont)
      : base(game)
    {
      _regularFont = regularFont;
      _selectedFont = selectedFont;
      _menuItems = new List<string>();

      _spriteBatch = game.Services.GetService<SpriteBatch>();
      _audioLibrary = game.Services.GetService<AudioLibrary>();

      _oldKeyboardState = Keyboard.GetState();
    }

    public void SetMenuItems(string[] items)
    {
      _menuItems.Clear();
      _menuItems.AddRange(items);
      CalculateBounds();
    }


    public override void Update(GameTime gameTime)
    {
      KeyboardState keyboardState = Keyboard.GetState();
      bool down;
      bool up;

      down = _oldKeyboardState.IsKeyDown(Keys.Down) && keyboardState.IsKeyUp(Keys.Down);
      up = _oldKeyboardState.IsKeyDown(Keys.Up) && keyboardState.IsKeyUp(Keys.Up);

      if (down || up)
      {
        _audioLibrary.MenuScroll.Play();
      }

      if (down)
      {
        SelectedIndex++;
        if (SelectedIndex == _menuItems.Count)
        {
          SelectedIndex = 0;
        }
      }

      if (up)
      {
        SelectedIndex--;
        if (SelectedIndex == -1)
        {
          SelectedIndex = _menuItems.Count - 1;
        }
      }

      _oldKeyboardState = keyboardState;
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      float y = Position.Y;
      for (int i = 0; i < _menuItems.Count; i++)
      {
        SpriteFont font;
        Color theColor;

        if (i == SelectedIndex)
        {
          font = _selectedFont;
          theColor = SelectedColor;
        }
        else
        {
          font = _regularFont;
          theColor = RegularColor;
        }

        _spriteBatch.DrawString(font, _menuItems[i], new Vector2(Position.X + 1, y + 1), Color.Black);

        _spriteBatch.DrawString(font, _menuItems[i], new Vector2(Position.X, y), theColor);

        y += font.LineSpacing;
      }

      base.Draw(gameTime);
    }

    protected void CalculateBounds()
    {
      Width = 0;
      Height = 0;
      foreach (string item in _menuItems)
      {
        Vector2 size = _selectedFont.MeasureString(item);
        if (size.X > Width)
        {
          Width = (int)size.X;
        }
        Height += _selectedFont.LineSpacing;
      }
    }
  }
}