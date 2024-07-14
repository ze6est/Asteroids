using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Configs;
using Asteroids.CodeBase.Enemies;
using Asteroids.CodeBase.Ships;
using Asteroids.CodeBase.Spawners.EnemiesSpawners;
using UnityEngine;

namespace Asteroids.CodeBase
{
    public class Prefabs : MonoBehaviour
    {
        [Header("Ship")]
        public Ship ShipPrefab;
        public Bullet BulletPrefab;
        public Laser LaserPrefab;

        [Header("Spawner")] 
        public EnemiesSpawner EnemiesSpawnerPrefab;
        
        [Header("Enemies")] 
        public Asteroid AsteroidPrefab;
        public Ufo UfoPrefab;
        public AsteroidSmall AsteroidSmallPrefab;
        
        [Header("Configs")] 
        public ShipConfig ShipConfig;
        public EnemiesConfig EnemiesConfig;
        
        [Header("Other")] 
        public Canvas HudPrefab;
        public Destroyer DestroyerPrefab;
    }
}
