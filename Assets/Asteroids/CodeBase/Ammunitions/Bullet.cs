using Asteroids.CodeBase.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Ammunitions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : Ammunition
    {
        public event UnityAction EnemieDestroyed;
        public event UnityAction<Bullet> Destroyed;
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemie _))
            {
                EnemieDestroyed?.Invoke();
            }
            
            Destroyed?.Invoke(this);
        }
    }
}