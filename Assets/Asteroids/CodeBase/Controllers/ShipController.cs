using System.Collections.Generic;
using Asteroids.CodeBase.Guns;
using Asteroids.CodeBase.Input;
using Asteroids.CodeBase.Ships;
using Asteroids.CodeBase.Spawners.AmmunitionsSpawners;

namespace Asteroids.CodeBase.Controllers
{
    public class ShipController
    {
        public ShipController(ShipInput shipInput, ShipMover shipMover, ShipRotator shipRotator,
            ShipShooter shipShooter, List<BulletGun> bulletGuns, List<LaserGun> laserGuns, BulletSpawner bulletSpawner,
            LaserSpawner laserSpawner)
        {
            shipInput.Moved += shipMover.OnMoved;
            shipInput.Rotated += shipRotator.OnRotated;
            shipInput.BulletShooted += shipShooter.OnBulletShooted;
            shipInput.LaserShooted += shipShooter.OnLaserShooted;
            
            shipShooter.Construct(bulletGuns, laserGuns);

            foreach (BulletGun bulletGun in bulletGuns)
                bulletGun.Construct(bulletSpawner);

            foreach (LaserGun laserGun in laserGuns)
                laserGun.Construct(laserSpawner);
        }
    }
}
