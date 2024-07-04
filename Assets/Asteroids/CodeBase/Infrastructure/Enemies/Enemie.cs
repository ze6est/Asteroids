using UnityEngine;

namespace Asteroids.CodeBase.Enemies
{
    public abstract class Enemie : MonoBehaviour
    {
        [SerializeField] protected float Speed;
        [SerializeField] protected Rigidbody2D Rigidbody;
        
        protected abstract void Move();

        protected abstract void OnTriggerEnter2D(Collider2D other);
    }
}
