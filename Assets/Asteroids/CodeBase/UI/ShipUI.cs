using Asteroids.CodeBase.Guns;
using Asteroids.CodeBase.Ships;
using TMPro;
using UnityEngine;

namespace Asteroids.CodeBase.UI
{
    public class ShipUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coordinates;
        [SerializeField] private TextMeshProUGUI _angleRotation;
        [SerializeField] private TextMeshProUGUI _speed;
        [SerializeField] private TextMeshProUGUI _laserCharges;
        [SerializeField] private TextMeshProUGUI _laserFailureTime;

        private ShipMover _shipMover;
        private LaserGun _laserGun;
        private Transform _ship;

        public void Construct(ShipMover shipMover, LaserGun laserGun)
        {
            _shipMover = shipMover;
            _laserGun = laserGun;
            
            _ship = _shipMover.transform;
            _laserGun.LaserChargesChanged += OnLaserChargesChanged;
            _laserGun.LaserFailureTimeChanged += OnLaserFailureTimeChanged;
        }

        private void Update()
        {
            Vector2 coordinates = _ship.position;
            
            _coordinates.text = $"Coordinates: {coordinates}";
            _angleRotation.text = $"Angle rotation: {(int)_ship.localEulerAngles.z}";
            
            string formattedVelocity = $"{_shipMover.Velocity:F2}";
            _speed.text = $"Speed: {formattedVelocity}";
        }
        
        private void OnDestroy()
        {
            _laserGun.LaserChargesChanged -= OnLaserChargesChanged;
            _laserGun.LaserFailureTimeChanged -= OnLaserFailureTimeChanged;
        }
        
        private void OnLaserChargesChanged(int currentLaserCharges) => 
            _laserCharges.text = $"Laser charges : {currentLaserCharges}";
        
        private void OnLaserFailureTimeChanged(float currentLaserFailureTime) => 
            _laserFailureTime.text = $"Laser failure time: {currentLaserFailureTime:F2}";
    }
}
