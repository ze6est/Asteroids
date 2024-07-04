using Asteroids.CodeBase.Spawners.AmmunitionsSpawners;
using UnityEngine.Events;

namespace Asteroids.CodeBase
{
    public class ScoreCounter
    {
        private LaserSpawner _laserSpawner;
        private BulletSpawner _bulletSpawner;

        private int _score;

        public event UnityAction<int> ScoreChanged;

        public ScoreCounter(BulletSpawner bulletSpawner, LaserSpawner laserSpawner)
        {
            _bulletSpawner = bulletSpawner;
            _laserSpawner = laserSpawner;
            
            _laserSpawner.EnemyDestroyed += OnEnemieDestroyed;
            _bulletSpawner.EnemyDestroyed += OnEnemieDestroyed;
            
            _score = 0;
            ScoreChanged?.Invoke(_score);
        }

        private void OnDestroy()
        {
            _laserSpawner.EnemyDestroyed -= OnEnemieDestroyed;
            _bulletSpawner.EnemyDestroyed -= OnEnemieDestroyed;
        }

        private void OnEnemieDestroyed()
        {
            _score++;
            
            ScoreChanged?.Invoke(_score);
        }
    }
}