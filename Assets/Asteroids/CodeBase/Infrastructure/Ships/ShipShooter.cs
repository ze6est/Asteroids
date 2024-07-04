using System.Collections.Generic;
using Asteroids.CodeBase.Guns;
using UnityEngine;

namespace Asteroids.CodeBase.Ships
{
    public class ShipShooter : MonoBehaviour
    {
        private List<BulletGun> _bulletsGun;
        private LaserGun _laserGun;

        public void Construct(List<BulletGun> bulletGuns, LaserGun laserGun)
        {
            _bulletsGun = bulletGuns;
            _laserGun = laserGun;
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
            _laserGun.Shoot();
        }
    }
}