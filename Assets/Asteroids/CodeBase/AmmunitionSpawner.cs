using Asteroids.CodeBase.Ammunitions;

namespace Asteroids.CodeBase
{
    public class AmmunitionSpawner : Spawner<Ammunition>
    {
        public AmmunitionSpawner(Ammunition prefab, int capacity, int maxSize) : base(prefab, capacity, maxSize)
        {
        }
        
        protected override Ammunition CreateObject()
        {
            Ammunition ammunition = base.CreateObject();

            ammunition.Disabled += ReturnObjectToPool;

            return ammunition;
        }

        private void ReturnObjectToPool(Ammunition ammunition)
        { 
            Pool.Release(ammunition);
        }

    }
}
