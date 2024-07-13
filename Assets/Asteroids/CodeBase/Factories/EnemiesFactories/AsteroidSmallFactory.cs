using Asteroids.CodeBase.Enemies;
using UnityEngine;

namespace Asteroids.CodeBase.Factories.EnemiesFactories
{
    public class AsteroidSmallFactory : Factory<AsteroidSmall>
    {
        public AsteroidSmallFactory(AsteroidSmall prefab, int capacity, int maxSize, Transform container) : base(prefab, capacity, maxSize, container)
        {
        }
    }
}
