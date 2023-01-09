using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AsteroidBelt
{
    internal static class SoundManager
    {
        private static List<SoundEffect> explosions = new List<SoundEffect>();
        private static int explosionCount = 4;

        private static SoundEffect playerShot;
        private static SoundEffect enemyShot;

        private static Random rand = new Random();

        public static void Initialize(ContentManager content)
        {
            try
            {
                playerShot = content.Load<SoundEffect>(@"Audios\Shot1");
                enemyShot = content.Load<SoundEffect>(@"Audios\Shot2");

                for (int x = 1; x <= explosionCount; x++)
                {
                    explosions.Add(content.Load<SoundEffect>(@$"Audios\Explosion{x}"));
                }
            }
            catch (Exception)
            {
                Debug.Write("SoundManager Initialization Failed");
            }
        }

        public static void PlayExplosion()
        {
            try
            {
                explosions[rand.Next(0, explosionCount)].Play();
            }
            catch (Exception)
            {
                Debug.Write("PlayExplosion Failed");
            }
        }

        public static void PlayPlayerShot()
        {
            try
            {
                playerShot.Play();
            }
            catch (Exception)
            {
                Debug.Write("PlayPlayerShot Failed");
            }
        }

        public static void PlayEnemyShot()
        {
            try
            {
                enemyShot.Play();
            }
            catch (Exception)
            {
                Debug.Write("PlayEnemyShot Failed");
            }
        }
    }
}
