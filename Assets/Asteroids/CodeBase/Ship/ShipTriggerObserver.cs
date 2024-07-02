using Asteroids.CodeBase.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Ship
{
    public class ShipTriggerObserver : MonoBehaviour
    {
        public event UnityAction Died;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemie>(out _))
                Died?.Invoke();
        }
    }
}
