using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase.Ammunitions
{
    public class Laser : Ammunition
    {
        public event UnityAction<Laser> Destroyed;
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            
            if(other.TryGetComponent(out Destroyer _))
                Destroyed?.Invoke(this);
        }
    }
}