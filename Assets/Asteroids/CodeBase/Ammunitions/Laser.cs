using Asteroids.CodeBase.Enemies;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Ammunitions
{
    public class Laser : Ammunition
    {
        public event UnityAction EnemieDestroyed;
        public event UnityAction<Laser> Destroyed;
        
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemie _))
            {
                EnemieDestroyed?.Invoke();
            }
            
            if(other.TryGetComponent(out Destroyer _))
                Destroyed?.Invoke(this);
        }
    }
}