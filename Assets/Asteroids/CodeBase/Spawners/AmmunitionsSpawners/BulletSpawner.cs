using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Factories;

namespace Asteroids.CodeBase.Spawners.AmmunitionsSpawners
{
    public class BulletSpawner : Spawner<Bullet>
    {
        public BulletSpawner(Factory<Bullet> factory) : base(factory)
        {
        }
    }
}
