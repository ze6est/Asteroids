using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Input;
using UnityEngine;

namespace Asteroids.CodeBase.Ship
{
    [RequireComponent(typeof(ShipInput))]
    public class ShipShooter : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private Bullet _bullet;
        [SerializeField] private Transform[] _bulletPoints;

        [Header("Laser Settings")]
        [SerializeField] private Laser _laser;
        [SerializeField] private Transform _laserPoint;
        
        private ShipInput _input;

        private void Awake()
        {
            _input = GetComponent<ShipInput>();
        }

        private void OnEnable()
        {
            _input.BulletShooted += OnBulletShooted;
            _input.LaserShooted += OnLaserShooted;
        }

        private void OnDisable()
        {
            _input.BulletShooted -= OnBulletShooted;
            _input.LaserShooted -= OnLaserShooted;
        }

        private void OnBulletShooted()
        {
            foreach (Transform bulletPoint in _bulletPoints)
            {
                Instantiate(_bullet, bulletPoint);
            }
        }
        
        private void OnLaserShooted()
        {
            Instantiate(_laser, _laserPoint);
        }
    }
}
