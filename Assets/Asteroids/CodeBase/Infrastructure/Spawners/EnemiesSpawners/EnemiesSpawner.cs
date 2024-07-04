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
        private UfoFactory _ufoFactory;
        private AsteroidSmallFactory _asteroidSmallFactory;
        
        private Ship _target;

        private Coroutine _spawnEnemieJob;

        public void Construct(AsteroidsFactory asteroidsFactory, UfoFactory ufoFactory, AsteroidSmallFactory asteroidSmallFactory, Ship ship)
        {
            _asteroidsFactory = asteroidsFactory;
            _ufoFactory = ufoFactory;
            _asteroidSmallFactory = asteroidSmallFactory;
            _target = ship;
            
            _spawnEnemieJob = StartCoroutine(SpawnEnemies());
        }

        private void OnDisable() => 
            StopCoroutine(_spawnEnemieJob);

        private IEnumerator SpawnEnemies()
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
                    Asteroid asteroid = _asteroidsFactory.GetObject(position);
                    asteroid.CalculateDirectionNormalized();
                    asteroid.Destroyed += OnEnemieDestroyed;
                }
                else
                {
                    Ufo ufo = _ufoFactory.GetObject(position);
                    ufo.Construct(_target.transform);
                    ufo.Destroyed += OnEnemieDestroyed;
                }

                yield return wait;
            }
        }

        private void OnEnemieDestroyed(Enemie enemie, Vector2 position)
        {
            switch (enemie)
            {
                case Asteroid asteroid:
                    CrashAsteroid(asteroid, position);
                    break;
                case Ufo ufo:
                    CrashUfo(ufo);
                    break;
                case AsteroidSmall asteroidSmall:
                    CrachAsteroidSmall(asteroidSmall);
                    break;
            }
        }

        private void CrashAsteroid(Asteroid asteroid, Vector2 position)
        {
            for (int i = 0; i < _countAsteroidsSmall; i++)
            {
                AsteroidSmall asteroidSmall = _asteroidSmallFactory.GetObject(position);
                asteroidSmall.CalculateDirectionNormalized();
                asteroidSmall.Destroyed += OnEnemieDestroyed;
            }
            
            asteroid.Destroyed -= OnEnemieDestroyed;
            _asteroidsFactory.Release(asteroid);
        }

        private void CrashUfo(Ufo ufo)
        {
            ufo.Destroyed -= OnEnemieDestroyed;
            _ufoFactory.Release(ufo);
        }

        private void CrachAsteroidSmall(AsteroidSmall asteroidSmall)
        {
            asteroidSmall.Destroyed -= OnEnemieDestroyed;
            _asteroidSmallFactory.Release(asteroidSmall);
        }
    }
}