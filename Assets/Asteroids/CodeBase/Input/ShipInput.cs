using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Asteroids.CodeBase.Input
{
    public class ShipInput
    {
        private InputActions _input;

        public event UnityAction<float> Moved;
        public event UnityAction<Vector2> Rotated;
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
        
        private void OnMoved(InputAction.CallbackContext context) => 
            Moved?.Invoke(context.ReadValue<float>());

        private void OnLookTo(InputAction.CallbackContext context) => 
            Rotated?.Invoke(context.ReadValue<Vector2>());

        private void OnBulletShooted(InputAction.CallbackContext context) =>
            BulletShooted?.Invoke();

        private void OnLaserShooted(InputAction.CallbackContext context) =>
            LaserShooted?.Invoke();
    }
}
