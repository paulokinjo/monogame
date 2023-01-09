using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AsteroidBelt
{
    public class PlayerManager
    {
        public Sprite PlayerSprite;
        private float playerSpeed = 160.0f;
        private Rectangle playerAreaLimit;

        public long PlayerScore = 0;
        public int LivesRemaining = 3;
        public bool Destroyed = false;

        private Vector2 gunOffset = new Vector2(23, -2);
        private float shotTimer = 0.0f;
        private float minShotTimer = 0.2f;
        private int playerRadius = 15;
        public ShotManager PlayerShotManager;

        public PlayerManager(
            Texture2D texture,
            Rectangle initialFrame,
            int frameCount,
            Rectangle screenBounds)
        {
            PlayerSprite = new Sprite(
                new Vector2(500, 500),
                texture,
                initialFrame,
                Vector2.Zero);

            PlayerShotManager = new ShotManager(
                texture,
                new Rectangle(0, 300, 5, 5),
                4,
                2,
                250f,
                screenBounds);

            playerAreaLimit =
                new Rectangle(
                    0,
                    screenBounds.Height / 2,
                    screenBounds.Width,
                    screenBounds.Height / 2);

            for (int x = 1; x < frameCount; x++)
            {
                PlayerSprite.AddFrame(
                    new Rectangle(
                        initialFrame.X + (initialFrame.Width * x),
                        initialFrame.Y,
                        initialFrame.Width,
                        initialFrame.Height));
            }

            PlayerSprite.CollisionRadius = playerRadius;
        }

        public void Update(GameTime gameTime)
        {
            PlayerShotManager.Update(gameTime);
            if (!Destroyed)
            {
                PlayerSprite.Velocity = Vector2.Zero;

                shotTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                HandleKeyboardInput(Keyboard.GetState());

                PlayerSprite.Velocity.Normalize();
                PlayerSprite.Velocity *= playerSpeed;

                PlayerSprite.Update(gameTime);
                ImposeMovementLimits();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerShotManager.Draw(spriteBatch);
            if (!Destroyed)
            {
                PlayerSprite.Draw(spriteBatch);
            }
        }

        private void FireShot()
        {
            if (shotTimer >= minShotTimer)
            {
                PlayerShotManager.FireShot(
                    PlayerSprite.Location + gunOffset,
                    new Vector2(0, -1),
                    true);
                shotTimer = 0.0f;
            }
        }

        private void HandleKeyboardInput(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.Up))
            {
                PlayerSprite.Velocity += new Vector2(0, -1);
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                PlayerSprite.Velocity += new Vector2(0, 1);
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                PlayerSprite.Velocity += new Vector2(-1, 0);
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                PlayerSprite.Velocity += new Vector2(1, 0);
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                FireShot();
            }
        }

        private void ImposeMovementLimits()
        {
            Vector2 location = PlayerSprite.Location;

            if (location.X < playerAreaLimit.X)
            {
                location.X = playerAreaLimit.X;
            }

            if (location.X > (playerAreaLimit.Right - PlayerSprite.Source.Width))
            {
                location.X = playerAreaLimit.Right - PlayerSprite.Source.Width;
            }

            if (location.Y < playerAreaLimit.Y)
            {
                location.Y = playerAreaLimit.Y;
            }

            if (location.Y > (playerAreaLimit.Bottom - PlayerSprite.Source.Height))
            {
                location.Y = playerAreaLimit.Bottom - PlayerSprite.Source.Height;
            }

            PlayerSprite.Location = location;
        }
    }
}
