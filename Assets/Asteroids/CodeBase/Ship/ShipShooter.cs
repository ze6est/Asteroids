using System.Collections;
using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Input;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private int _maxLaserCharges = 10;
        [SerializeField] private float _laserFailureTime = 10;

        [Header("ScoreCounter")] [SerializeField]
        private ScoreCounter _scoreCounter;
        
        private ShipInput _input;
        private AmmunitionSpawner _laserSpawner;
        private AmmunitionSpawner _bulletSpawner;

        private bool _isRecharged;
        private int _currentLaserCharges;
        private float _currentLaserFailureTime;

        public event UnityAction<int> LaserChargesChanged;
        public event UnityAction<float> LaserFailureTimeChanged;

        private void Awake()
        {
            _input = GetComponent<ShipInput>();
            _laserSpawner = new AmmunitionSpawner(_laser, 10, 10, transform);
            _bulletSpawner = new AmmunitionSpawner(_bullet, 20, 50, transform);
        }

        private void Start()
        {
            _isRecharged = true;
            _currentLaserCharges = _maxLaserCharges;
            _currentLaserFailureTime = 0;
            
            LaserChargesChanged?.Invoke(_currentLaserCharges);
            LaserFailureTimeChanged?.Invoke(_currentLaserFailureTime);
            
            _scoreCounter.Subscribe(_laserSpawner);
            _scoreCounter.Subscribe(_bulletSpawner);
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
                _bulletSpawner.Spawn(bulletPoint.position, bulletPoint.rotation.eulerAngles);
        }
        
        private void OnLaserShooted()
        {
            if (_isRecharged)
            {
                _laserSpawner.Spawn(_laserPoint.position, _laserPoint.rotation.eulerAngles);
                _currentLaserCharges--;
                LaserChargesChanged?.Invoke(_currentLaserCharges);
            }

            if (_currentLaserCharges <= 0)
            {
                StartCoroutine(Recharge());
                _isRecharged = false;
            }
        }

        private IEnumerator Recharge()
        {
            _currentLaserFailureTime = _laserFailureTime;
            
            while (_currentLaserFailureTime > 0)
            {
                _currentLaserFailureTime -= Time.deltaTime;
                LaserFailureTimeChanged?.Invoke(_currentLaserFailureTime);
                yield return null;
            }

            _currentLaserCharges = _maxLaserCharges;
            _currentLaserFailureTime = 0;
            _isRecharged = true;
            
            LaserChargesChanged?.Invoke(_currentLaserCharges);
            LaserFailureTimeChanged?.Invoke(_currentLaserFailureTime);
        }
    }
}