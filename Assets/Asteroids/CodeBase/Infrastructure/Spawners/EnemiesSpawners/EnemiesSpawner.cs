using System.Collections;
using Asteroids.CodeBase.Enemies;
using Asteroids.CodeBase.Factories.EnemiesFactories;
using Asteroids.CodeBase.Ships;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.CodeBase.Spawners.EnemiesSpawners
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRadius = 5f;
        [SerializeField] private float _spawnTime = 3f;
        [SerializeField] private int _countAsteroidsSmall = 3;

        private AsteroidsFactory _asteroidsFactory;
        private UFOFactory _ufoFactory;
        private AsteroidSmallFactory _asteroidSmallFactory;
        
        private Ship _target;

        private Coroutine _spawnEnemieJob;

        public void Construct(AsteroidsFactory asteroidsFactory, UFOFactory ufoFactory, AsteroidSmallFactory asteroidSmallFactory, Ship ship)
        {
            _asteroidsFactory = asteroidsFactory;
            _ufoFactory = ufoFactory;
            _asteroidSmallFactory = asteroidSmallFactory;
            _target = ship;
            
            _spawnEnemieJob = StartCoroutine(SpawnEnemie());
        }

        private void OnDisable() => 
            StopCoroutine(_spawnEnemieJob);

        private IEnumerator SpawnEnemie()
        {
            WaitForSeconds wait = new WaitForSeconds(_spawnTime);

            while (true)
            {
                float angle = Random.Range(0, Mathf.PI * 2);
                Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * _spawnRadius;
                position += transform.position;

                int random = Random.Range(0, 101);

                if (random > 50)
                {
                    Asteroid asteroid = _asteroidsFactory.GetObject();
                    asteroid.Destroyed += OnAsteroidDestroyed;
                    asteroid.transform.position = position;
                }
                else
                {
                    UFO ufo = _ufoFactory.GetObject();
                    ufo.Construct(_target.transform);
                    ufo.transform.position = position;
                }

                yield return wait;
            }
        }

        private void OnAsteroidDestroyed(Vector2 position)
        {
            for (int i = 0; i < _countAsteroidsSmall; i++)
            {
                AsteroidSmall asteroidSmall = _asteroidSmallFactory.GetObject();
                asteroidSmall.transform.position = position;
            }
        }
    }
}