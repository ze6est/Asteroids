using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Asteroids.CodeBase.Factories
{
    public class Factory<T> where T : MonoBehaviour
    {
        private ObjectPool<T> _pool;
        
        private readonly T _prefab;
        private readonly Transform _container;

        protected Factory(T prefab, int capacity, int maxSize, Transform container)
        {
            _prefab = prefab;
            _container = container;
            
            _pool = new ObjectPool<T>(CreateObject, OnGetObject, OnReleaseObject, OnDestroyObject, false, capacity, maxSize);
        }
        
        public T GetObject(Vector3 position)
        {
            T obj = _pool.Get();
            obj.transform.position = position;
            return obj;
        }

        public void Release(T obj) =>
            _pool.Release(obj);

        private T CreateObject()
        {
            T obj = Object.Instantiate(_prefab, Vector2.zero, Quaternion.identity);
            obj.gameObject.SetActive(false);
            obj.transform.parent = _container;
            
            return obj;
        }

        private void OnGetObject(T obj) => 
            obj.gameObject.SetActive(true);

        private void OnReleaseObject(T obj) => 
            obj.gameObject.SetActive(false);

        private void OnDestroyObject(T obj) => 
            Object.Destroy(obj.gameObject);
    }
}
