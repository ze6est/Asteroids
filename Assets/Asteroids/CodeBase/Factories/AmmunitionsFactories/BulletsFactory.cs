using Asteroids.CodeBase.Ammunitions;
using UnityEngine;

namespace Asteroids.CodeBase.Factories.AmmunitionsFactories
{
    public class BulletsFactory : Factory<Bullet>
    {
        public BulletsFactory(Bullet prefab, int capacity, int maxSize, Transform container) : base(prefab, capacity, maxSize, container)
        {
        }
    }
}
