using System.Collections.Generic;
using System.Linq;
using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Enemies;
using Asteroids.CodeBase.Factories.AmmunitionsFactories;
using Asteroids.CodeBase.Factories.EnemiesFactories;
using Asteroids.CodeBase.Guns;
using Asteroids.CodeBase.Input;
using UnityEngine;
using Asteroids.CodeBase.Ships;
using Asteroids.CodeBase.Spawners.AmmunitionsSpawners;
using Asteroids.CodeBase.Spawners.EnemiesSpawners;
using Asteroids.CodeBase.UI;

namespace Asteroids.CodeBase
{
    public class EntryPoint : MonoBehaviour
    {
        private Ship _shipPrefab;
        private Bullet _bulletPrefab;
        private Laser _laserPrefab;

        private EnemiesSpawner _enemiesSpawnerPrefab;
        private Asteroid _asteroidPrefab;
        private Ufo _ufoPrefab;
        private AsteroidSmall _asteroidSmallPrefab;

        private Destroyer _destroyerPrefab;
        private Canvas _hudPrefab;

        private ScoreCounter _scoreCounter;

        private Ship _ship;
        private ShipTriggerObserver _shipTriggerObserver;
        private ShipMover _shipMover;
        private ShipShooter _shipShooter;
        private ShipRotator _shipRotator;
        private BulletSpawner _bulletSpawner;
        private LaserSpawner _laserSpawner;
        private LaserGun _laserGun;
        
        private InputActions _input;
        private ShipInput _shipInput;

        private EnemiesSpawner _enemiesSpawner;

        private Canvas _hud;
        
        private void Awake()
        {
            LoadPrefabs();
            InstantiateWorld();

            EnableInput();

            ConstructShip();
            ConstructEnemiesSpawner();
            
            _scoreCounter = new ScoreCounter(_bulletSpawner, _laserSpawner);
            
            ConstructHud();
            
        }


        private void LoadPrefabs()
        {
            _shipPrefab = Resources.Load<Ship>(AssetsPath.SHIP_PATH);
            _bulletPrefab = Resources.Load<Bullet>(AssetsPath.BULLET_PATH);
            _laserPrefab = Resources.Load<Laser>(AssetsPath.LASER_PATH);

            _enemiesSpawnerPrefab = Resources.Load<EnemiesSpawner>(AssetsPath.ENEMIES_SPAWNER_PATH);
            _asteroidPrefab = Resources.Load<Asteroid>(AssetsPath.ASTEROID_PATH);
            _ufoPrefab = Resources.Load<Ufo>(AssetsPath.UFO_PATH);
            _asteroidSmallPrefab = Resources.Load<AsteroidSmall>(AssetsPath.ASTEROID_SMALL_PREFAB);

            _destroyerPrefab = Resources.Load<Destroyer>(AssetsPath.DESTROYER);

            _hudPrefab = Resources.Load<Canvas>(AssetsPath.HUD_PATH);
        }

        private void InstantiateWorld()
        {
            _ship = Instantiate(_shipPrefab);
            _enemiesSpawner = Instantiate(_enemiesSpawnerPrefab);
            Instantiate(_destroyerPrefab);
            _hud = Instantiate(_hudPrefab);
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
            _bulletSpawner = new BulletSpawner(bulletsFactory);

            LasersFactory lasersFactory = new LasersFactory(_laserPrefab, 10, 10, _ship.transform);
            _laserSpawner = new LaserSpawner(lasersFactory);
            
            List<BulletGun> bulletGuns = _ship.GetComponentsInChildren<BulletGun>().ToList();
            _laserGun = _ship.GetComponentInChildren<LaserGun>();

            if (_ship.TryGetComponent(out ShipMover shipMover))
                _shipMover = shipMover;
                
            if(_ship.TryGetComponent(out ShipShooter shipShooter))
                _shipShooter = shipShooter;

            if (_ship.TryGetComponent(out ShipRotator shipRotator))
                _shipRotator = shipRotator;

            if (_ship.TryGetComponent(out ShipTriggerObserver shipTriggerObserver))
                _shipTriggerObserver = shipTriggerObserver;
            
            _shipInput.Moved += shipMover.OnMoved;
            _shipInput.Rotated += shipRotator.OnRotated;
            _shipInput.BulletShooted += shipShooter.OnBulletShooted;
            _shipInput.LaserShooted += shipShooter.OnLaserShooted;
            
            shipShooter.Construct(bulletGuns, _laserGun);

            foreach (BulletGun bulletGun in bulletGuns)
                bulletGun.Construct(_bulletSpawner);
            
            _laserGun.Construct(_laserSpawner);
        }

        private void ConstructEnemiesSpawner()
        {
            Transform container = _enemiesSpawner.transform;
            
            AsteroidsFactory asteroidsFactory = new AsteroidsFactory(_asteroidPrefab, 10, 10, container);
            UFOFactory ufoFactory = new UFOFactory(_ufoPrefab, 10, 10, container);
            AsteroidSmallFactory asteroidSmallFactory = new AsteroidSmallFactory(_asteroidSmallPrefab, 10, 10, container);
            
            _enemiesSpawner.Construct(asteroidsFactory, ufoFactory, asteroidSmallFactory, _ship);
        }

        private void ConstructHud()
        {
            RestartWindow restartWindow = _hud.GetComponentInChildren<RestartWindow>();
            
            if(_hud.TryGetComponent(out Restarter restarter))
                restarter.Construct(_shipTriggerObserver, restartWindow);

            ShipUI shipUI = _hud.GetComponentInChildren<ShipUI>();
            shipUI.Construct(_shipMover, _laserGun);

            ScoreCounterView scoreCounterView = restartWindow.GetComponentInChildren<ScoreCounterView>();
            scoreCounterView.Construct(_scoreCounter);
        }

        private void OnDestroy()
        {
            _shipInput.Moved -= _shipMover.OnMoved;
            _shipInput.Rotated -= _shipRotator.OnRotated;
            _shipInput.BulletShooted -= _shipShooter.OnBulletShooted;
            _shipInput.LaserShooted -= _shipShooter.OnLaserShooted;
        }
    }
}