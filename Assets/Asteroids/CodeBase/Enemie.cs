using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase
{
    public abstract class Enemie : MonoBehaviour
    {
        [SerializeField] protected float Speed;
        [SerializeField] protected Rigidbody2D Rigidbody;

        public event UnityAction<Vector2> Destroyed;
        
        public void Crash()
        {
            Destroyed?.Invoke(transform.position);
            Destroy(gameObject);
        }
        
        protected abstract void Move();
    }
}
