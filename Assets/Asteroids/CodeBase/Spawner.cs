using Asteroids.CodeBase.Ammunitions;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.CodeBase
{
    public abstract class Spawner<T> where T : Ammunition
    {
        protected ObjectPool<T> Pool;
        
        private readonly T _prefab;
        private readonly Transform _container;

        protected Spawner(T prefab, int capacity, int maxSize, Transform container)
        {
            _prefab = prefab;
            _container = container;
            
            Pool = new ObjectPool<T>(CreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, false, capacity, maxSize);
        }
        
        public void Spawn(Vector2 position, Vector3 direction)
        {
            T obj = Pool.Get();
            
            obj.transform.position = position;
            obj.transform.rotation = Quaternion.Euler(direction);
        }

        protected virtual T CreateObject()
        {
            T obj = Object.Instantiate(_prefab, Vector2.zero, Quaternion.identity);
            obj.gameObject.SetActive(false);
            obj.transform.parent = _container;
            
            return obj;
        }

        protected virtual void OnGetObject(T obj) => 
            obj.gameObject.SetActive(true);

        protected virtual void OnReleaseObject(T obj) => 
            obj.gameObject.SetActive(false);

        protected virtual void OnDestroyObject(T obj) => 
            Object.Destroy(obj.gameObject);
    }
}
