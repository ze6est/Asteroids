using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Asteroids.CodeBase.Input
{
    public class ShipInput
    {
        private InputActions _input;
        
        public float MoveInput {get; private set;}
        public Vector2 LookInput { get; private set; }

        public event UnityAction BulletShooted;
        public event UnityAction LaserShooted;

        public ShipInput(InputActions input) => 
            _input = input;

        public void Enable()
        {
            _input.ShipInput.Enable();

            _input.ShipInput.Moved.performed += OnMoved;
            _input.ShipInput.Moved.canceled += OnMoved;
            
            _input.ShipInput.LookTo.performed += OnLookTo;
            _input.ShipInput.LookTo.canceled += OnLookTo;

            _input.ShipInput.BulletShooted.performed += OnBulletShooted;
            _input.ShipInput.LaserShooted.performed += OnLaserShooted;
        }

        public void Disable()
        {
            _input.ShipInput.Moved.performed -= OnMoved;
            _input.ShipInput.Moved.canceled -= OnMoved;
            
            _input.ShipInput.LookTo.performed -= OnLookTo;
            _input.ShipInput.LookTo.canceled -= OnLookTo;
            
            _input.ShipInput.BulletShooted.performed -= OnBulletShooted;
            _input.ShipInput.LaserShooted.performed -= OnLaserShooted;
            
            _input.ShipInput.Disable();
        }
        
        private void OnMoved(InputAction.CallbackContext obj) => 
            MoveInput = obj.ReadValue<float>();

        private void OnLookTo(InputAction.CallbackContext obj) => 
            LookInput = obj.ReadValue<Vector2>();

        private void OnBulletShooted(InputAction.CallbackContext obj) =>
            BulletShooted?.Invoke();

        private void OnLaserShooted(InputAction.CallbackContext obj) =>
            LaserShooted?.Invoke();
    }
}
