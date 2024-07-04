using UnityEngine;

namespace Asteroids.CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        [Header("Asteroid")] 
        [SerializeField] private float _asteroidSpeed = 0.5f;
        [SerializeField] private float _maxMovePositionX = 2;
        [SerializeField] private float _maxMovePositionY = 1f;

        [Header("Ufo")]
        [SerializeField] private float _ufoSpeed = 2;

        [Header("Asteroid Small")]
        [SerializeField] private float _asteroidSmallSpeed = 0.7f;

        public float AsteroidSpeed => _asteroidSpeed;
        public float MaxMovePositionX => _maxMovePositionX;
        public float MaxMovePositionY => _maxMovePositionY;

        public float UfoSpeed => _ufoSpeed;

        public float AsteroidSmallSpeed => _asteroidSmallSpeed;
    }
}
