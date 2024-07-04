using UnityEngine;

namespace Asteroids.CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/ShipConfig")]
    public class ShipConfig : ScriptableObject
    {
        [Header("Ship Mover")] 
        [SerializeField] private float _acceleration = 0.5f;
        [SerializeField] private float _deceleration = 0.5f;
        [SerializeField] private float _maxSpeed = 4f;

        [Header("Ship Rotator")] 
        [SerializeField] private float _rotateSpeed = 2f;

        [Header("Bullets Factory")] 
        [SerializeField] private int _capacityBullets;
        [SerializeField] private int _maxCountBullets;
        
        [Header("Laser Factory")] 
        [SerializeField] private int _capacityLasers;
        [SerializeField] private int _maxCountLasers;

        [Header("Bullet")] 
        [SerializeField] private float _startBulletSpeed;
        [SerializeField] private float _maxBulletSpeed;
        
        [Header("Laser")] 
        [SerializeField] private float _startLaserSpeed;
        [SerializeField] private float _maxLaserSpeed;

        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float MaxSpeed => _maxSpeed;

        public float RotateSpeed => _rotateSpeed;

        public int CapacityBullets => _capacityBullets;
        public int MaxCountBullets => _maxCountBullets;

        public int CapacityLasers => _capacityLasers;
        public int MaxCountLasers => _maxCountLasers;

        public float StartBulletSpeed => _startBulletSpeed;
        public float MaxBulletSpeed => _maxBulletSpeed;
        
        public float StartLaserSpeed => _startLaserSpeed;
        public float MaxLaserSpeed => _maxLaserSpeed;
    }
}