using Asteroids.CodeBase.Factories;
using UnityEngine;

namespace Asteroids.CodeBase.Spawners
{
    public abstract class Spawner<T> where T : MonoBehaviour
    {
        private Factory<T> _factory;

        protected Spawner(Factory<T> factory)
        {
            _factory = factory;
        }
        
        public virtual void Spawn(Vector2 position, Quaternion rotation)
        {
            T obj = _factory.GetObject(position);
            obj.transform.rotation = rotation;
        }
    }
}