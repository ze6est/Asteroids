using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.CodeBase
{
    public class Asteroid : Enemie
    {
        [SerializeField] private float _maxMovePositionX = 3;
        [SerializeField] private float _maxMovePositionY = 2f;
        
        private Vector2 _direction;

        private void Start()
        {
            _direction = GetDirectionNormalized();
            Debug.Log(_direction);
        }

        private void Update()
        {
            Move();
        }

        protected override void Move()
        {
            Rigidbody.AddForce(Speed * Time.deltaTime * _direction);
            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, Speed);
        }

        private Vector2 GetDirectionNormalized()
        {
            float positionX = Random.Range(-_maxMovePositionX, _maxMovePositionX);
            float positionY = Random.Range(-_maxMovePositionY, _maxMovePositionY);
            
            return (new Vector3(positionX, positionY, 0) - transform.position).normalized;
        }
    }
}