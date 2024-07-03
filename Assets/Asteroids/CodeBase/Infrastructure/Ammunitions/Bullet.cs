using Asteroids.CodeBase.Enemies;
using UnityEngine;

namespace Asteroids.CodeBase.Ammunitions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : Ammunition
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            
            if (other.TryGetComponent(out Enemie enemie))
                OnDisabled(this);
        }
    }
}