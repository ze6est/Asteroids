using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Factories;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Spawners.AmmunitionsSpawners
{
    public class LaserSpawner : Spawner<Laser>
    {
        private Factory<Laser> _factory;
        
        public event UnityAction EnemyDestroyed;
        
        public LaserSpawner(Factory<Laser> factory) : base(factory)
        {
            _factory = factory;
        }
        
        public override void Spawn(Vector2 position, Quaternion rotation)
        {
            Laser laser = _factory.GetObject(position);
            laser.transform.rotation = rotation;
            laser.EnemieDestroyed += OnEnemieDestroyed;
            laser.Destroyed += OnDestroyed;
        }
        
        private void OnEnemieDestroyed()
        {
            EnemyDestroyed?.Invoke();
        }

        private void OnDestroyed(Laser laser)
        {
            laser.Destroyed -= OnDestroyed;
            _factory.Release(laser);
        }
    }
}
