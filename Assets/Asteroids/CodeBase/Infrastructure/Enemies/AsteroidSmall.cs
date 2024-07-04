using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Enemies
{
    public class AsteroidSmall : Enemie
    {
        private Vector2 _randomDirection;
        
        public event UnityAction<AsteroidSmall, Vector2> Destroyed;

        private void Update() => 
            Move();

        public void CalculateDirectionNormalized()
        {
            _randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        protected override void Move()
        {
            Rigidbody.AddForce(Speed * Time.deltaTime * _randomDirection);
            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, Speed);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Destroyed?.Invoke(this, transform.position);
        }
    }
}
