using UnityEngine;

namespace Asteroids.CodeBase
{
    public abstract class Enemie : MonoBehaviour
    {
        [SerializeField] protected float Speed;
        [SerializeField] protected Rigidbody2D Rigidbody;

        protected abstract void Move();
    }
}
