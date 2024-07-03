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
        
        public void Spawn(Vector2 position, Quaternion rotation)
        {
            T obj = _factory.GetObject();

            Transform objTransform = obj.transform;
            
            objTransform.position = position;
            objTransform.rotation = rotation;
        }
    }
}