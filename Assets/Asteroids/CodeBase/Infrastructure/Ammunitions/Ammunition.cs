using Asteroids.CodeBase.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Ammunitions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Ammunition : MonoBehaviour
    {
        [SerializeField] private float _startSpeed;
        [SerializeField] private float _maxSpeed;
        
        private Rigidbody2D _rigidbody;
        
        public event UnityAction EnemieDestroyed;
        
        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody2D>();

        private void Update() => 
            Move();

        private void Move()
        {
            _rigidbody.AddRelativeForce(_startSpeed * Time.deltaTime * Vector2.up);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemie _))
            {
                EnemieDestroyed?.Invoke();
            }
        }
    }
}