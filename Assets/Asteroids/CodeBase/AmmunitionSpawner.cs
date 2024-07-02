using Asteroids.CodeBase.Ammunitions;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase
{
    public class AmmunitionSpawner : Spawner<Ammunition>
    {
        public AmmunitionSpawner(Ammunition prefab, int capacity, int maxSize, Transform container) : base(prefab, capacity, maxSize, container){}
        
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
