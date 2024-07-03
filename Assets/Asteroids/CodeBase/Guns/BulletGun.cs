using Asteroids.CodeBase.Spawners.AmmunitionsSpawners;

namespace Asteroids.CodeBase.Guns
{
    public class BulletGun : Gun
    {
        private BulletSpawner _bulletSpawner;

        public void Construct(BulletSpawner bulletSpawner)
        {
            _bulletSpawner = bulletSpawner;
        }
        
        public override void Shoot()
        {
            _bulletSpawner.Spawn(transform.position, transform.rotation);
        }
    }
}
