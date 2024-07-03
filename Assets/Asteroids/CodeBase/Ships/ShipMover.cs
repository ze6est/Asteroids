using UnityEngine;

namespace Asteroids.CodeBase.Ships
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipMover : MonoBehaviour
    {
        [SerializeField] private float _acceleration = 5f;
        [SerializeField] private float _deceleration = 0.5f;
        [SerializeField] private float _maxSpeed = 10f;
        
        private Rigidbody2D _rigidbody;

        [SerializeField] private float _moveInput;
        
        public float Velocity { get; private set; }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() => 
            Move();

        public void OnMoved(float speed)
        {
            _moveInput = speed;
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
