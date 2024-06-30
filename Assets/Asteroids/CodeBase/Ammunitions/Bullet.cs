using UnityEngine;

namespace Asteroids.CodeBase.Ammunitions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : Ammunition
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out Enemie enemie))
            {
                enemie.Crash();
                Destroy(gameObject);
            }
        }
    }
}