using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AsteroidBelt
{
    public class StarField
    {
        private List<Sprite> stars = new List<Sprite>();
        private int screenWidth = 800;
        private int screenHeight = 600;

        private Random rand = new Random();
        private Color[] colors =
        {
            Color.White,
            Color.Yellow,
            Color.Wheat,
            Color.WhiteSmoke,
            Color.SlateGray
        };

        public StarField(
            int screenWidth,
            int screenHeight,
            int starCount,
            Texture2D texture,
            Rectangle frameRectangle,
            Vector2 starVelocity)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            for (int x = 0; x < starCount; x++)
            {
                var starLocation = new Vector2(
                    rand.Next(0, screenWidth),
                    rand.Next(0, screenHeight));

                var star = new Sprite(
                    starLocation,
                    texture,
                    frameRectangle,
                    starVelocity);

                var starColor = colors[rand.Next(0, colors.Count())];
                starColor *= (float)(rand.Next(30, 80) / 100f);

                star.TintColor = starColor;

                stars.Add(star);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var star in stars)
            {
                star.Update(gameTime);
                if (star.Location.Y > screenHeight)
                {
                    star.Location = new Vector2(rand.Next(0, screenWidth), 0);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var star in stars)
            {
                star.Draw(spriteBatch);
            }
        }
    }
}
