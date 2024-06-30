using Asteroids.CodeBase.Ammunitions;
using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.CodeBase
{
    public abstract class Spawner<T> where T : Ammunition
    {
        protected ObjectPool<T> Pool;
        
        private T _prefab;
        private int _capacity = 20;
        private int _maxSize = 50;

        protected Spawner(T prefab, int capacity, int maxSize)
        {
            _prefab = prefab;
            _capacity = capacity;
            _maxSize = maxSize;
            
            Pool = new ObjectPool<T>(CreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, false, _capacity, _maxSize);
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
