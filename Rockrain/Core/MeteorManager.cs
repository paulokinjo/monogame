using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rockrain.Audio;

namespace Rockrain.Core
{
  public class MeteorManager : DrawableGameComponent
  {
    public List<Meteor> Meteors { get; private set; } = new List<Meteor>();
    private const int START_METEOR_COUNT = 10;
    private const int ADD_METEOR_TIME = 5000;

    private Texture2D MeteorTexture;
    private TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;
    private AudioLibrary AudioLibrary { get; }

    public MeteorManager(Game game, ref Texture2D texture)
      : base(game)
    {
      MeteorTexture = texture;
      AudioLibrary = Game.Services.GetService<AudioLibrary>();
    }

    public override void Initialize()
    {
      Meteors.Clear();
      Start();

      foreach (var meteor in Meteors)
      {
        meteor.Initialize();
      }

      base.Initialize();
    }

    public void Start()
    {
      ElapsedTime = TimeSpan.Zero;

      for (int i = 0; i < START_METEOR_COUNT; i++)
      {
        AddNewMeteor();
      }
    }

    public override void Update(GameTime gameTime)
    {
      CheckForNewMeteor(gameTime);

      foreach (var meteor in Meteors)
      {
        meteor.Update(gameTime);
      }

      base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
      foreach (var meteor in Meteors)
      {
        meteor.Draw(gameTime);
      }

      base.Draw(gameTime);
    }

    public bool CheckForCollisions(Rectangle rectangle)
    {
      foreach (var meteor in Meteors)
      {
        if (meteor.CheckCollision(rectangle))
        {
          AudioLibrary.Explosion.Play();
          meteor.PutInStartPosition();

          return true;
        }
      }

      return false;
    }

    private void AddNewMeteor()
    {
      Meteor newMeteor = new Meteor(Game, ref MeteorTexture);
      newMeteor.Initialize();
      Meteors.Add(newMeteor);

      newMeteor.Index = Meteors.Count - 1;
    }

    private void CheckForNewMeteor(GameTime gameTime)
    {
      ElapsedTime += gameTime.ElapsedGameTime;

      if (ElapsedTime > TimeSpan.FromMilliseconds(ADD_METEOR_TIME))
      {
        ElapsedTime -= TimeSpan.FromMilliseconds(ADD_METEOR_TIME);
        AddNewMeteor();
        AudioLibrary.NewMeteor.Play();
      }
    }
  }
}