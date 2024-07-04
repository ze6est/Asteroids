using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Factories;
using UnityEngine;

namespace Asteroids.CodeBase.Spawners.AmmunitionsSpawners
{
    public class LaserSpawner : Spawner<Laser>
    {
        private Factory<Laser> _factory;
        
        public LaserSpawner(Factory<Laser> factory) : base(factory)
        {
            _factory = factory;
        }
        
        public override void Spawn(Vector2 position, Quaternion rotation)
        {
            Laser laser = _factory.GetObject(position);
            laser.transform.rotation = rotation;
            laser.Destroyed += OnDestroyed;
        }

        private void OnDestroyed(Laser laser)
        {
            laser.Destroyed -= OnDestroyed;
            _factory.Release(laser);
        }
    }
}
