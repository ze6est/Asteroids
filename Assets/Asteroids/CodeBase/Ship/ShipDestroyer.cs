using UnityEngine;

namespace Asteroids.CodeBase.Ship
{
    public class ShipDestroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemie>(out _))
            {
                Time.timeScale = 0;
            }
        }
    }
}
