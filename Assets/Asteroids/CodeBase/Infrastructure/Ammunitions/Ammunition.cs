using UnityEngine;

namespace Asteroids.CodeBase.Ammunitions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Ammunition : MonoBehaviour
    {
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _maxSpeed;
        
        private Rigidbody2D _rigidbody;

        public void Construct(float startSpeed, float maxSpeed)
        {
            _startSpeed = startSpeed;
            _maxSpeed = maxSpeed;
        }
        
        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody2D>();

        private void Update() => 
            Move();

        private void Move()
        {
            _rigidbody.AddRelativeForce(_startSpeed * Time.deltaTime * Vector2.up);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
        }
    }
}