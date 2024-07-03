using System.Collections.Generic;
using System.Linq;
using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Controllers;
using Asteroids.CodeBase.Enemies;
using Asteroids.CodeBase.Factories.AmmunitionsFactories;
using Asteroids.CodeBase.Factories.EnemiesFactories;
using Asteroids.CodeBase.Guns;
using Asteroids.CodeBase.Input;
using UnityEngine;
using Asteroids.CodeBase.Ships;
using Asteroids.CodeBase.Spawners.AmmunitionsSpawners;
using Asteroids.CodeBase.Spawners.EnemiesSpawners;

namespace Asteroids.CodeBase
{
    public class EntryPoint : MonoBehaviour
    {
        private Ship _shipPrefab;
        private Bullet _bulletPrefab;
        private Laser _laserPrefab;

        private EnemiesSpawner _enemiesSpawnerPrefab;
        private Asteroid _asteroidPrefab;
        private UFO _ufoPrefab;
        private AsteroidSmall _asteroidSmallPrefab;

        private Ship _ship;
        private InputActions _input;
        private ShipInput _shipInput;

        private EnemiesSpawner _enemiesSpawner;
        
        private void Awake()
        {
            LoadPrefabs();
            InstantiateWorld();

            EnableInput();

            ConstructShip();
            ConstructEnemiesSpawner();
        }


        private void LoadPrefabs()
        {
            _shipPrefab = Resources.Load<Ship>(AssetsPath.SHIP_PATH);
            _bulletPrefab = Resources.Load<Bullet>(AssetsPath.BULLET_PATH);
            _laserPrefab = Resources.Load<Laser>(AssetsPath.LASER_PATH);

            _enemiesSpawnerPrefab = Resources.Load<EnemiesSpawner>(AssetsPath.ENEMIES_SPAWNER_PATH);
            _asteroidPrefab = Resources.Load<Asteroid>(AssetsPath.ASTEROID_PATH);
            _ufoPrefab = Resources.Load<UFO>(AssetsPath.UFO_PATH);
            _asteroidSmallPrefab = Resources.Load<AsteroidSmall>(AssetsPath.ASTEROID_SMALL_PREFAB);

            //_hudPrefab = Resources.Load<Canvas>(AssetsPath.HUD_PATH);
            //_restartWindowPrefab = Resources.Load<RestartWindow>(AssetsPath.RESTART_WINDOW_PATH);
            //_shipUIPrefab = Resources.Load<ShipUI>(AssetsPath.SHIP_UI_PATH);
        }

        private void InstantiateWorld()
        {
            _ship = Instantiate(_shipPrefab);
            _enemiesSpawner = Instantiate(_enemiesSpawnerPrefab);
        }
        
        private void EnableInput()
        {
            _input = new InputActions();
            _shipInput = new ShipInput(_input);
            _shipInput.Enable();
        }
        
        private void ConstructShip()
        {
            BulletsFactory bulletsFactory = new BulletsFactory(_bulletPrefab, 10, 10, _ship.transform);
            BulletSpawner bulletSpawner = new BulletSpawner(bulletsFactory);

            LasersFactory lasersFactory = new LasersFactory(_laserPrefab, 10, 10, _ship.transform);
            LaserSpawner laserSpawner = new LaserSpawner(lasersFactory);
            
            List<BulletGun> bulletGuns = _ship.GetComponentsInChildren<BulletGun>().ToList();
            List<LaserGun> laserGuns = _ship.GetComponentsInChildren<LaserGun>().ToList();
            
            if(_ship.TryGetComponent(out ShipMover shipMover))
                if(_ship.TryGetComponent(out ShipRotator shipRotator))
                    if(_ship.TryGetComponent(out ShipShooter shipShooter))
                    {
                        new ShipController(_shipInput, shipMover, shipRotator, shipShooter, bulletGuns, laserGuns, bulletSpawner, laserSpawner);
                    }
        }

        private void ConstructEnemiesSpawner()
        {
            Transform container = _enemiesSpawner.transform;
            
            AsteroidsFactory asteroidsFactory = new AsteroidsFactory(_asteroidPrefab, 10, 10, container);
            UFOFactory ufoFactory = new UFOFactory(_ufoPrefab, 10, 10, container);
            AsteroidSmallFactory asteroidSmallFactory = new AsteroidSmallFactory(_asteroidSmallPrefab, 10, 10, container);
            
            _enemiesSpawner.Construct(asteroidsFactory, ufoFactory, asteroidSmallFactory, _ship);
        }
    }
}