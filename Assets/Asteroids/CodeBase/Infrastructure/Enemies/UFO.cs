using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Enemies
{
    public class Ufo : Enemie
    {
        private Transform _target;
        
        public event UnityAction<Ufo, Vector2> Destroyed;

        public void Construct(Transform target) => 
            _target = target;

        void Update() => 
            Move();

        protected override void Move()
        {
            Vector2 direction = (_target.position - transform.position).normalized;
            Rigidbody.MovePosition(Rigidbody.position + Speed * Time.fixedDeltaTime * direction);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Destroyed?.Invoke(this, transform.position);
        }
    }
}
