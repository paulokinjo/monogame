using Microsoft.Xna.Framework;

namespace AsteroidBelt
{
    internal class CollisionManager
    {
        private AsteroidManager asteroidManager;
        private PlayerManager playerManager;
        private EnemyManager enemyManager;
        private ExplosionManager explosionManager;
        private Vector2 offScreen = new Vector2(-500, -500);
        private Vector2 shotToAsteroidImpact = new Vector2(0, -20);
        private int enemyPointValue = 100;

        public CollisionManager(
            AsteroidManager asteroidManager,
            PlayerManager playerManager,
            EnemyManager enemyManager,
            ExplosionManager explosionManager)
        {
            this.asteroidManager = asteroidManager;
            this.playerManager = playerManager;
            this.enemyManager = enemyManager;
            this.explosionManager = explosionManager;
        }

        public void CheckCollisions()
        {
            CheckShotToEnemyCollisions();
            CheckShotToAsteroidCollisions();
            if (!playerManager.Destroyed)
            {
                CheckShotToPlayerCollisions();
                CheckEnemyToPlayerCollisions();
                CheckAsteroidToPlayerCollisions();
            }
        }

        private void CheckShotToEnemyCollisions()
        {
            foreach (Sprite shot in playerManager.PlayerShotManager.Shots)
            {
                foreach (Enemy enemy in enemyManager.Enemies)
                {
                    if (shot.IsCircleColliding(
                        enemy.EnemySprite.Center,
                        enemy.EnemySprite.CollisionRadius))
                    {
                        shot.Location = offScreen;
                        enemy.Destroyed = true;
                        playerManager.PlayerScore += enemyPointValue;
                        explosionManager.AddExplosion(
                            enemy.EnemySprite.Center,
                            enemy.EnemySprite.Velocity / 10);
                    }
                }
            }
        }

        private void CheckShotToAsteroidCollisions()
        {
            foreach (Sprite shot in playerManager.PlayerShotManager.Shots)
            {
                foreach (Sprite asteroid in asteroidManager.Asteroids)
                {
                    if (shot.IsCircleColliding(
                        asteroid.Center,
                        asteroid.CollisionRadius))
                    {
                        shot.Location = offScreen;
                        asteroid.Velocity += shotToAsteroidImpact;
                    }
                }
            }
        }

        private void CheckShotToPlayerCollisions()
        {
            foreach (Sprite shot in enemyManager.EnemyShotManager.Shots)
            {
                if (shot.IsCircleColliding(
                    playerManager.PlayerSprite.Center,
                    playerManager.PlayerSprite.CollisionRadius))
                {
                    shot.Location += offScreen;
                    playerManager.Destroyed = true;
                    explosionManager.AddExplosion(
                        playerManager.PlayerSprite.Center,
                        Vector2.Zero);
                }
            }
        }

        private void CheckEnemyToPlayerCollisions()
        {
            foreach (Enemy enemy in enemyManager.Enemies)
            {
                if (enemy.EnemySprite.IsCircleColliding(
                    playerManager.PlayerSprite.Center,
                    playerManager.PlayerSprite.CollisionRadius))
                {
                    enemy.Destroyed = true;
                    explosionManager.AddExplosion(
                        enemy.EnemySprite.Center,
                        enemy.EnemySprite.Velocity / 10);

                    playerManager.Destroyed = true;

                    explosionManager.AddExplosion(
                        playerManager.PlayerSprite.Center,
                        Vector2.Zero);
                }
            }
        }

        private void CheckAsteroidToPlayerCollisions()
        {
            foreach (Sprite asteroid in asteroidManager.Asteroids)
            {
                if (asteroid.IsCircleColliding(
                    playerManager.PlayerSprite.Center,
                    playerManager.PlayerSprite.CollisionRadius))
                {
                    explosionManager.AddExplosion(
                        asteroid.Center,
                        asteroid.Velocity / 10);

                    asteroid.Location = offScreen;

                    playerManager.Destroyed = true;
                    explosionManager.AddExplosion(
                        playerManager.PlayerSprite.Center,
                        Vector2.Zero);
                }
            }
        }
    }
}
