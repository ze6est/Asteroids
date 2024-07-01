using Asteroids.CodeBase.Enemies;
using UnityEngine;

namespace Asteroids.CodeBase
{
    public class UFO : Enemie
    {
        private Transform _target;

        public void Construct(Transform target)
        {
            _target = target;
        }
        
        void Update()
        {
            Move();
        }
        
        protected override void Move()
        {
            Vector2 direction = (_target.position - transform.position).normalized;
            Rigidbody.MovePosition(Rigidbody.position + Speed * Time.fixedDeltaTime * direction);
        }
    }
}
