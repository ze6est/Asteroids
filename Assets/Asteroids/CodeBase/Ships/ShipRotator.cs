using Asteroids.CodeBase.Configs;
using UnityEngine;

namespace Asteroids.CodeBase.Ships
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipRotator : MonoBehaviour
    {
        private float _speed;
        
        private Rigidbody2D _rigidbody;
        private Camera _camera;

        private Coroutine _rotateJob;

        private Vector2 _lookTo;

        public void Construct(ShipConfig shipConfig)
        {
            _speed = shipConfig.RotateSpeed;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
        }

        private void Update() => 
            Rotate();
        
        public void OnRotated(Vector2 lookTo) => 
            _lookTo = lookTo;

        private void Rotate()
        {
            Vector2 direction = _camera.ScreenToWorldPoint(_lookTo) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.rotation = Mathf.LerpAngle(_rigidbody.rotation, angle, _speed * Time.deltaTime);
        }
    }
}