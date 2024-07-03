using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Factories;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Spawners
{
    public class AmmunitionFactory : Factory<Ammunition>
    {
        public AmmunitionFactory(Ammunition prefab, int capacity, int maxSize, Transform container) : base(prefab, capacity, maxSize, container){}
        
        public event UnityAction EnemieDestroyed;

        protected override Ammunition CreateObject()
        {
            Ammunition ammunition = base.CreateObject();

            ammunition.Disabled += ReturnObjectToPool;
            ammunition.EnemieDestroyed += OnEnemieDestroyed;

            return ammunition;
        }

        private void ReturnObjectToPool(Ammunition ammunition) => 
            Pool.Release(ammunition);

        private void OnEnemieDestroyed() => 
            EnemieDestroyed?.Invoke();
    }
}
