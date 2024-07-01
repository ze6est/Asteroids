using Asteroids.CodeBase.Enemies;
using UnityEngine;

namespace Asteroids.CodeBase
{
    public class AsteroidSmall : Enemie
    {
        private Vector2 _randomDirection;
        
        private void Start()
        {
            _randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        private void Update()
        {
            Move();
        }

        protected override void Move()
        {
            Rigidbody.AddForce(Speed * Time.deltaTime * _randomDirection);
            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, Speed);
        }
    }
}
