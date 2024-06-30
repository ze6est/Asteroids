using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.CodeBase
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Enemie[] _enemies;
        [SerializeField] private AsteroidSmall _asteroidSmall;
        [SerializeField] private float _spawnRadius = 5f;
        [SerializeField] private float _spawnTime = 3f;
        [SerializeField] private int _countAsteroidsSmall = 3;

        [SerializeField] private ShipMover _target;

        private Coroutine _spawnEnemieJob;
        
        private void OnEnable()
        {
            _spawnEnemieJob = StartCoroutine(SpawnEnemie());
        }

        private void OnDisable()
        {
            StopCoroutine(_spawnEnemieJob);
        }

        private IEnumerator SpawnEnemie()
        {
            WaitForSeconds wait = new WaitForSeconds(_spawnTime);

            while (true)
            {
                float angle = Random.Range(0, Mathf.PI * 2);
                Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _spawnRadius;
                position += transform.position;

                int enemieIndex = Random.Range(0, _enemies.Length);
            
                Enemie enemie = Instantiate(_enemies[enemieIndex], position, Quaternion.identity);
                
                if(enemie is UFO ufo)
                    ufo.Construct(_target.transform);
                else if (enemie is Asteroid)
                    enemie.Destroyed += OnAsteroidDestroyed;

                yield return wait;
            }
        }

        private void OnAsteroidDestroyed(Vector2 position)
        {
            for (int i = 0; i < _countAsteroidsSmall; i++)
            {
                Instantiate(_asteroidSmall, position, Quaternion.identity);
            }
        }
    }
}