using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Asteroids.CodeBase.Enemies
{
    public class Asteroid : Enemie
    {
        [SerializeField] private float _maxMovePositionX;
        [SerializeField] private float _maxMovePositionY;
        
        private Vector2 _direction;

        public event UnityAction<Asteroid, Vector2> Destroyed;

        public void Construct(float speed, float maxMovePositionX, float maxMovePositionY)
        {
            Speed = speed;
            _maxMovePositionX = maxMovePositionX;
            _maxMovePositionY = maxMovePositionY;
        }
        
        private void Update() => 
            Move();

        public void CalculateDirectionNormalized()
        {
            float positionX = Random.Range(-_maxMovePositionX, _maxMovePositionX);
            float positionY = Random.Range(-_maxMovePositionY, _maxMovePositionY);
            
            _direction = (new Vector3(positionX, positionY, 0) - transform.position).normalized;
        }

        protected override void Move()
        {
            Rigidbody.AddForce(Speed * Time.deltaTime * _direction);
            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, Speed);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            Destroyed?.Invoke(this, transform.position);
        }
    }
}