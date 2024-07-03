using System.Collections.Generic;
using Asteroids.CodeBase.Guns;
using UnityEngine;

namespace Asteroids.CodeBase.Ships
{
    public class ShipShooter : MonoBehaviour
    {
        private List<BulletGun> _bulletsGun;
        private List<LaserGun> _laserGuns;

        public void Construct(List<BulletGun> bulletGuns, List<LaserGun> laserGuns)
        {
            _bulletsGun = bulletGuns;
            _laserGuns = laserGuns;
        }
        
        public void OnBulletShooted()
        {
            foreach (BulletGun bulletGun in _bulletsGun)
            {
                bulletGun.Shoot();
            }
        }
        
        public void OnLaserShooted()
        {
            foreach (var laserGun in _laserGuns)
            {
                laserGun.Shoot();
            }
        }
    }
}