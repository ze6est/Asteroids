using System;
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

        public event UnityAction<Ammunition> Disabled;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
        }
        
        protected virtual void OnDisabled(Ammunition ammunition)
        {
            Disabled?.Invoke(ammunition);
        }

        private void Move()
        {
            _rigidbody.AddRelativeForce(_startSpeed * Time.deltaTime * Vector2.up);
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Destroyer>(out _))
            {
                OnDisabled(this);
            }
            
            if (other.TryGetComponent(out Enemie enemie))
            {
                enemie.Crash();
            }
        }
    }
}