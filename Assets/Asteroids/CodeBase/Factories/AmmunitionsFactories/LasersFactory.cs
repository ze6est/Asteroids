using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Spawners;
using UnityEngine;

namespace Asteroids.CodeBase.Factories.AmmunitionsFactories
{
    public class LasersFactory : Factory<Laser>
    {
        public LasersFactory(Laser prefab, int capacity, int maxSize, Transform container) : base(prefab, capacity, maxSize, container)
        {
        }
    }
}
