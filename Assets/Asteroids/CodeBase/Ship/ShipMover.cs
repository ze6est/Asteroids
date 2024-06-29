using System;
using Asteroids.CodeBase.Input;
using UnityEngine;

namespace Asteroids.CodeBase
{
    [RequireComponent(typeof(Rigidbody2D), typeof(ShipInput))]
    public class ShipMover : MonoBehaviour
    {
        [SerializeField] private float _acceleration = 5f;
        [SerializeField] private float _maxSpeed = 10f;

        private ShipInput _input;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _input = GetComponent<ShipInput>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _rigidbody.AddRelativeForce(_input.MoveInput * _acceleration * Time.deltaTime * Vector2.up, ForceMode2D.Force);
            
            _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, _maxSpeed);
        }
    }
}
