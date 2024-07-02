using Asteroids.CodeBase.Input;
using UnityEngine;

namespace Asteroids.CodeBase.Ships
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipMover : MonoBehaviour
    {
        [SerializeField] private float _acceleration = 5f;
        [SerializeField] private float _deceleration = 0.5f;
        [SerializeField] private float _maxSpeed = 10f;

        private ShipInput _input;
        private Rigidbody2D _rigidbody;
        
        public float Velocity { get; private set; }

        public void Construct(ShipInput shipInput)
        {
            _input = shipInput;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() => 
            Move();

        private void Move()
        {
            _rigidbody.AddRelativeForce(_input.MoveInput * _acceleration * Time.deltaTime * Vector2.up, ForceMode2D.Force);
            
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);

            if (_input.MoveInput <= 0)
            {
                float velocityX = Mathf.Lerp(_rigidbody.velocity.x, 0, _deceleration * Time.deltaTime);
                float velocityY = Mathf.Lerp(_rigidbody.velocity.y, 0, _deceleration * Time.deltaTime);

                _rigidbody.velocity = new Vector2(velocityX, velocityY);
            }

            Velocity = transform.InverseTransformDirection(_rigidbody.velocity).y;
        }
    }
}
