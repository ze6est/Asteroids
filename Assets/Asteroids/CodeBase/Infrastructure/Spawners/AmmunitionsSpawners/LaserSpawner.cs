using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Factories;

namespace Asteroids.CodeBase.Spawners.AmmunitionsSpawners
{
    public class LaserSpawner : Spawner<Laser>
    {
        public LaserSpawner(Factory<Laser> factory) : base(factory)
        {
        }
    }
}
