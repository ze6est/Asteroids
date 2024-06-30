using UnityEngine;

namespace Asteroids.CodeBase.Ammunitions
{
    public class Laser : Ammunition
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out Enemie enemie))
            {
                enemie.Crash();
            }
        }
    }
}