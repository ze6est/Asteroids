using Asteroids.CodeBase.Configs;
using UnityEngine;

namespace Asteroids.CodeBase.Ships
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipMover : MonoBehaviour
    {
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        
        private float _acceleration;
        private float _deceleration;
        private float _maxSpeed;
        
        private Rigidbody2D _rigidbody;

        private float _moveInput;
        
        public float Velocity { get; private set; }

        public void Construct(ShipConfig shipConfig)
        {
            _acceleration = shipConfig.Acceleration;
            _deceleration = shipConfig.Deceleration;
            _maxSpeed = shipConfig.MaxSpeed;
        }
        
        private void Awake()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
            
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() => 
            Move();

        public void OnMoved(float speed) => 
            _moveInput = speed;

        public void Restart()
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
            _rigidbody.velocity = Vector2.zero;
        }

        private void Move()
        {
            _rigidbody.AddRelativeForce(_moveInput * _acceleration * Time.deltaTime * Vector2.up, ForceMode2D.Force);
            
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
            
            if (_moveInput <= 0)
            {
                float velocityX = Mathf.Lerp(_rigidbody.velocity.x, 0, _deceleration * Time.deltaTime);
                float velocityY = Mathf.Lerp(_rigidbody.velocity.y, 0, _deceleration * Time.deltaTime);

                _rigidbody.velocity = new Vector2(velocityX, velocityY);
            }

            Velocity = transform.InverseTransformDirection(_rigidbody.velocity).y;
        }
    }
}
