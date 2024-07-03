using Asteroids.CodeBase.Enemies;
using Asteroids.CodeBase.Spawners;
using UnityEngine;

namespace Asteroids.CodeBase.Factories.EnemiesFactories
{
    public class AsteroidsFactory : Factory<Asteroid>
    {
        public AsteroidsFactory(Asteroid prefab, int capacity, int maxSize, Transform container) : base(prefab, capacity, maxSize, container)
        {
            
        }
    }
}