using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Factories;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Spawners.AmmunitionsSpawners
{
    public class BulletSpawner : Spawner<Bullet>
    {
        private Factory<Bullet> _factory;
        
        public event UnityAction EnemyDestroyed;
        
        public BulletSpawner(Factory<Bullet> factory) : base(factory)
        {
            _factory = factory;
        }
        
        public override void Spawn(Vector2 position, Quaternion rotation)
        {
            Bullet bullet = _factory.GetObject(position);
            bullet.transform.rotation = rotation;
            bullet.EnemieDestroyed += OnEnemieDestroyed;
            bullet.Destroyed += OnDestroyed;
        }

        private void OnEnemieDestroyed()
        {
            EnemyDestroyed?.Invoke();
        }

        private void OnDestroyed(Bullet bullet)
        {
            bullet.Destroyed -= OnDestroyed;
            _factory.Release(bullet);
        }
    }
}
