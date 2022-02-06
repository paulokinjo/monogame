using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rockrain.Core
{
  public class Meteor : Sprite
  {
    private int _ySpeed;
    public int YSpeed
    {
      get { return _ySpeed; }
      set
      {
        _ySpeed = value;
        FrameDelay = 200 - (_ySpeed * 5);
      }
    }
    public int XSpeed { get; private set; }
    public Random RandomPosition { get; }

    public int Index { get; set; }
    public Meteor(Game game, ref Texture2D texture)
      : base(game, ref texture)
    {
      Rectangle frame = new Rectangle();
      frame.X = 468;
      frame.Y = 0;
      frame.Width = 49;
      frame.Height = 44;
      Frames.Add(frame);

      frame.Y = 50;
      Frames.Add(frame);

      frame.Y = 98;
      frame.Height = 45;
      Frames.Add(frame);

      frame.Y = 146;
      frame.Height = 49;
      Frames.Add(frame);

      frame.Y = 200;
      frame.Height = 44;
      Frames.Add(frame);

      frame.Y = 250;
      Frames.Add(frame);

      frame.Y = 299;
      Frames.Add(frame);

      frame.Y = 350;
      frame.Height = 49;
      Frames.Add(frame);

      RandomPosition = new Random(GetHashCode());

      PutInStartPosition();
    }

    public override void Update(GameTime gameTime)
    {
      if ((Position.Y >= Game.Window.ClientBounds.Height) ||
           (Position.X >= Game.Window.ClientBounds.Width) ||
           Position.X <= 0)
      {
        PutInStartPosition();
      }

      Position.Y += YSpeed;
      Position.X += XSpeed;

      base.Update(gameTime);
    }



    public void PutInStartPosition()
    {
      Position.X = RandomPosition.Next(Game.Window.ClientBounds.Width - CurrentFrame.Width);
      Position.Y = 0;
      YSpeed = 1 + RandomPosition.Next(9);
      XSpeed = RandomPosition.Next(3) - 1;
    }
  }
}