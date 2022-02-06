using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Rockrain.Audio;
using Rockrain.Core;

namespace Rockrain.Scenes
{
  public class StartScene : GameScene
  {
    private readonly TextMenuComponent _menu;
    private readonly Texture2D _elements;

    private readonly AudioLibrary _audioLibrary;
    private readonly SpriteBatch _spriteBatch;

    private readonly Rectangle _rockRect = new Rectangle(0, 0, 536, 131);
    private Vector2 _rockPosition;
    private readonly Rectangle _rainRect = new Rectangle(120, 165, 517, 130);
    private Vector2 _rainPosition;
    private readonly Rectangle _enhancedRect = new Rectangle(8, 304, 375, 144);
    private Vector2 _enhancedPosition;
    private bool _showEnhanced;
    private TimeSpan _elapsedTime = TimeSpan.Zero;
    public StartScene(Game game, SpriteFont smallFont, SpriteFont largeFont,
      Texture2D background, Texture2D elements) : base(game)
    {
      this._elements = elements;
      Components.Add(new ImageComponent(game, background, ImageComponent.DrawMode.Center));

      string[] items = { "One Player", "Two Players", "Help", "Quit" };
      _menu = new TextMenuComponent(game, smallFont, largeFont);
      _menu.SetMenuItems(items);
      Components.Add(_menu);

      _spriteBatch = game.Services.GetService<SpriteBatch>();
      _audioLibrary = game.Services.GetService<AudioLibrary>();
    }

    public override void Update(GameTime gameTime)
    {
      if (!_menu.Visible)
      {
        if (_rainPosition.X >= (Game.Window.ClientBounds.Width - 595) / 2)
        {
          _rainPosition.X -= 15;
        }

        if (_rockPosition.X <= (Game.Window.ClientBounds.Width - 715) / 2)
        {
          _rockPosition.X += 15;
        }
        else
        {
          _menu.Visible = true;
          _menu.Enabled = true;

          MediaPlayer.Play(_audioLibrary.StartMusic);
          _enhancedPosition = new Vector2(
            (_rainPosition.X + _rainRect.Width - _enhancedRect.Width / 2) - 80,
            _rainPosition.Y);

          _showEnhanced = true;
        }
      }
      else
      {
        _elapsedTime += gameTime.ElapsedGameTime;
        if (_elapsedTime > TimeSpan.FromSeconds(1))
        {
          _elapsedTime -= TimeSpan.FromSeconds(1);
          _showEnhanced = !_showEnhanced;
        }
      }
      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      base.Draw(gameTime);

      _spriteBatch.Draw(_elements, _rockPosition, _rockRect, Color.White);
      _spriteBatch.Draw(_elements, _rainPosition, _rainRect, Color.White);

      if (_showEnhanced)
      {
        _spriteBatch.Draw(_elements, _enhancedPosition, _enhancedRect, Color.White);
      }
    }

    public override void Show()
    {
      _audioLibrary.NewMeteor.Play();

      _rockPosition.X = -1 * _rockRect.Width;
      _rockPosition.Y = 40;

      _rainPosition.X = Game.Window.ClientBounds.Width;
      _rainPosition.Y = 180;

      _menu.Position = new Vector2((Game.Window.ClientBounds.Width - _menu.Width) / 2, 330);

      _menu.Visible = false;
      _menu.Enabled = false;
      _showEnhanced = false;

      base.Show();
    }

    public override void Hide()
    {
      MediaPlayer.Stop();
      base.Hide();
    }
    public int SelectedMenuIndex
    {
      get { return _menu.SelectedIndex; }
    }
  }
}