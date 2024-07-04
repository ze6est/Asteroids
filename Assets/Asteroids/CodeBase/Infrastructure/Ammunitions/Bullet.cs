using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Ammunitions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : Ammunition
    {
        public event UnityAction<Bullet> Destroyed;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            
            Destroyed?.Invoke(this);
        }
    }
}