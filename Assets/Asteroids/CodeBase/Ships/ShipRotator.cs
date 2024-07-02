using Asteroids.CodeBase.Input;
using UnityEngine;

namespace Asteroids.CodeBase.Ships
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShipRotator : MonoBehaviour
    {
        [SerializeField] private float _speed = 2;
        
        private ShipInput _input;
        private Rigidbody2D _rigidbody;
        private Camera _camera;

        private Coroutine _rotateJob;

        public void Construct(ShipInput shipInput, Camera camera)
        {
            _input = shipInput;
            _camera = camera;
        }
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update() => 
            Rotate();

        private void Rotate()
        {
            Vector2 direction = _camera.ScreenToWorldPoint(_input.LookInput) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            _rigidbody.rotation = Mathf.LerpAngle(_rigidbody.rotation, angle, _speed * Time.deltaTime);
        }
    }
}