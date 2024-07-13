using System.Collections;
using Asteroids.CodeBase.Spawners.AmmunitionsSpawners;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Guns
{
    public class LaserGun : Gun
    {
        [SerializeField] private int _maxLaserCharges = 10;
        [SerializeField] private float _laserFailureTime = 10;
        
        private bool _isRecharged;
        private int _currentLaserCharges;
        private float _currentLaserFailureTime;
        
        private LaserSpawner _laserSpawner;
        
        public event UnityAction<int> LaserChargesChanged;
        public event UnityAction<float> LaserFailureTimeChanged;

        public void Construct(LaserSpawner laserSpawner)
        {
            _laserSpawner = laserSpawner;
        }
        
        private void Start()
        {
            _isRecharged = true;
            _currentLaserCharges = _maxLaserCharges;
            _currentLaserFailureTime = 0;
            
            LaserChargesChanged?.Invoke(_currentLaserCharges);
            LaserFailureTimeChanged?.Invoke(_currentLaserFailureTime);
        }
        
        public override void Shoot()
        {
            if (_isRecharged)
            {
                _laserSpawner.Spawn(transform.position, transform.rotation);
                _currentLaserCharges--;
                LaserChargesChanged?.Invoke(_currentLaserCharges);
            }
            
            if (_currentLaserCharges <= 0)
            {
                StartCoroutine(Recharge());
                _isRecharged = false;
            }
        }

        public void Restart()
        {
            _isRecharged = true;
            _currentLaserCharges = _maxLaserCharges;
            _currentLaserFailureTime = 0;
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
