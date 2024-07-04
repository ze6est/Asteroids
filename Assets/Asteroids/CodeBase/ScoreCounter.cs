using System;
using Asteroids.CodeBase.Spawners.AmmunitionsSpawners;
using UnityEngine.Events;

namespace Asteroids.CodeBase
{
    public class ScoreCounter : IDisposable
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
        }

        public void Dispose()
        {
            _laserSpawner.EnemyDestroyed -= OnEnemieDestroyed;
            _bulletSpawner.EnemyDestroyed -= OnEnemieDestroyed;
        }

        public void Restart()
        {
            _score = 0;
            ScoreChanged?.Invoke(_score);
        }

        private void OnEnemieDestroyed()
        {
            _score++;
            
            ScoreChanged?.Invoke(_score);
        }
    }
}