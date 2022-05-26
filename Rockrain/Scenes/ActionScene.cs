using System.Threading.Tasks;
using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Rockrain.Audio;
using Rockrain.Core;

namespace Rockrain.Scenes
{
  public class ActionScene : GameScene
  {
    private Texture2D _actionTexture;
    private AudioLibrary AudioLibrary { get; }
    private SpriteBatch SpriteBatch { get; }

    private Player Player1 { get; }
    private Player Player2 { get; }
    private MeteorManager MeteorManager { get; }
    private PowerSource PowerSource { get; }
    private ImageComponent Background { get; }
    private Score ScorePlayer1 { get; set; }
    private Score ScorePlayer2 { get; set; }

    private Vector2 _pausePosition;
    private Vector2 _gameOverPosition;
    private Rectangle PauseRect { get; } = new Rectangle(1, 120, 200, 44);
    private Rectangle GameOverRect { get; } = new Rectangle(1, 170, 350, 48);

    private bool _paused;
    public bool Paused
    {
      get { return _paused; }
      set
      {
        _paused = value;
        if (_paused)
        {
          MediaPlayer.Pause();
        }
        else
        {
          MediaPlayer.Resume();
        }
      }
    }
    public bool GameOver { get; set; }
    private TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;
    public bool TwoPlayers { get; set; }

    public ActionScene(Game game, Texture2D texture,
      Texture2D backgroundTexture, SpriteFont font) : base(game)
    {
      AudioLibrary = Game.Services.GetService<AudioLibrary>();

      Background = new ImageComponent(game, backgroundTexture,
          ImageComponent.DrawMode.Stretch);
      Components.Add(Background);

      _actionTexture = texture;

      SpriteBatch = Game.Services.GetService<SpriteBatch>();

      MeteorManager = new MeteorManager(Game, ref _actionTexture);
      Components.Add(MeteorManager);

      Player1 = new Player(Game, ref _actionTexture, PlayerIndex.One,
        new Rectangle(323, 15, 30, 30));
      Player1.Initialize();
      Components.Add(Player1);

      Player2 = new Player(Game, ref _actionTexture, PlayerIndex.Two,
        new Rectangle(360, 17, 30, 30));
      Player2.Initialize();
      Components.Add(Player2);

      ScorePlayer1 = new Score(game, font, Color.Blue);
      ScorePlayer1.Position = new Vector2(10, 10);
      Components.Add(ScorePlayer1);

      ScorePlayer2 = new Score(game, font, Color.Red);
      ScorePlayer2.Position = new Vector2(Game.Window.ClientBounds.Width - 200, 10);
      Components.Add(ScorePlayer2);

      PowerSource = new PowerSource(game, ref _actionTexture);
      PowerSource.Initialize();
      Components.Add(PowerSource);

    }

    public override void Show()
    {
      MediaPlayer.Play(AudioLibrary.BackMusic);
      MeteorManager.Initialize();
      PowerSource.PutInStartPosition();

      Player1.Reset();
      Player2.Reset();

      Paused = false;

      _pausePosition.X = (Game.Window.ClientBounds.Width - PauseRect.Width) / 2;
      _pausePosition.Y = (Game.Window.ClientBounds.Height - PauseRect.Height) / 2;

      GameOver = false;
      _gameOverPosition.X = (Game.Window.ClientBounds.Width - GameOverRect.Width) / 2;
      _gameOverPosition.Y = (Game.Window.ClientBounds.Height - GameOverRect.Height) / 2;


      Player1.Visible = true;
      Player2.Visible = TwoPlayers;
      Player2.Enabled = TwoPlayers;
      ScorePlayer2.Visible = TwoPlayers;
      ScorePlayer2.Enabled = TwoPlayers;

      base.Show();
    }

    public override void Hide()
    {
      MediaPlayer.Stop();

      base.Hide();
    }

    public override void Update(GameTime gameTime)
    {
      if ((!Paused) && (!GameOver))
      {
        HandleDamages();

        HandlePowerSourceSprite(gameTime);

        ScorePlayer1.Value = Player1.Score;
        ScorePlayer1.Power = Player1.Power;
        if (TwoPlayers)
        {
          ScorePlayer2.Value = Player2.Score;
          ScorePlayer2.Power = Player2.Power;
        }

        GameOver = ((Player1.Power <= 0) || (Player2.Power <= 0));
        if (GameOver)
        {
          Player1.Visible = (Player1.Power > 0);
          Player2.Visible = (Player2.Power > 0) && TwoPlayers;

          MediaPlayer.Stop();
        }

        base.Update(gameTime);
      }

      if (GameOver)
      {
        MeteorManager.Update(gameTime);
      }
    }

    private void HandleDamages()
    {
      if (MeteorManager.CheckForCollisions(Player1.GetBounds()))
      {
        losePoints(Player1);
      }

      if (TwoPlayers)
      {
        if (MeteorManager.CheckForCollisions(Player2.GetBounds()))
        {
          losePoints(Player2);
        }

        if (Player1.GetBounds().Intersects(Player2.GetBounds()))
        {
          losePoints(Player1);
          losePoints(Player2);
        }
      }
    }

    private void HandlePowerSourceSprite(GameTime gameTime)
    {
      if (PowerSource.CheckCollision(Player1.GetBounds()))
      {
        AudioLibrary.PowerGet.Play();
        ElapsedTime = TimeSpan.Zero;
        PowerSource.PutInStartPosition();
        Player1.Power += 50;
      }

      if (TwoPlayers)
      {
        if (PowerSource.CheckCollision(Player2.GetBounds()))
        {
          AudioLibrary.PowerGet.Play();
          PowerSource.PutInStartPosition();
          Player2.Power += 50;
        }
      }

      ElapsedTime += gameTime.ElapsedGameTime;
      if (ElapsedTime > TimeSpan.FromSeconds(15))
      {
        ElapsedTime -= TimeSpan.FromSeconds(15);
        PowerSource.Enabled = true;
        AudioLibrary.PowerShow.Play();
      }
    }

    public override void Draw(GameTime gameTime)
    {
      base.Draw(gameTime);

      if (Paused)
      {
        SpriteBatch.Draw(_actionTexture, _pausePosition, PauseRect, Color.White);
      }

      if (GameOver)
      {
        SpriteBatch.Draw(_actionTexture, _gameOverPosition, GameOverRect, Color.White);

        var data = JsonSerializer.Serialize<dynamic>(new {
          Power = Player1.Power,
          Score = Player1.Score
        });
        
        var path = Environment.CurrentDirectory + "/GameScore.json";  
        System.IO.File.WriteAllText(path, data);
      }
    }

    private void losePoints(Player player)
    {
      player.Power -= 10;
      player.Score -= 10;
    }
  }
}