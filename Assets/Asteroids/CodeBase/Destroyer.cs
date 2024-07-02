using Asteroids.CodeBase.Enemies;
using UnityEngine;

namespace Asteroids.CodeBase
{
    public class Destroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemie enemie))
                enemie.Crash();
        }
    }
}
