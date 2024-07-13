using System.Collections.Generic;
using System.Linq;
using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Configs;
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
        [Header("Ship")]
        [SerializeField] private Ship _shipPrefab;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Laser _laserPrefab;

        [Header("Spawner")]
        [SerializeField] private EnemiesSpawner _enemiesSpawnerPrefab;
        
        [Header("Enemies")]
        [SerializeField] private Asteroid _asteroidPrefab;
        [SerializeField] private Ufo _ufoPrefab;
        [SerializeField] private AsteroidSmall _asteroidSmallPrefab;
        
        [Header("Configs")]
        [SerializeField] private ShipConfig _shipConfig;
        [SerializeField] private EnemiesConfig _enemiesConfig;
        
        [Header("Other")]
        [SerializeField] private Canvas _hudPrefab;
        [SerializeField] private Destroyer _destroyerPrefab;
        
        private ScoreCounter _scoreCounter;

        private Ship _ship;
        private ShipTriggerObserver _shipTriggerObserver;
        private ShipMover _shipMover;
        private BulletsFactory _bulletsFactory;
        private LasersFactory _lasersFactory;
        private ShipShooter _shipShooter;
        private ShipRotator _shipRotator;
        private BulletSpawner _bulletSpawner;
        private LaserSpawner _laserSpawner;
        private LaserGun _laserGun;
        
        private InputActions _input;
        private ShipInput _shipInput;

        private EnemiesSpawner _enemiesSpawner;
        private AsteroidsFactory _asteroidsFactory;
        private UfoFactory _ufoFactory;
        private AsteroidSmallFactory _asteroidSmallFactory;

        private Canvas _hud;

        private Restarter _restarter;
        
        private void Awake()
        {
            ConstructAmmunitionsPrefabs();
            ConstructEnemiesPrefab();
            
            InstantiateWorld();

            EnableInput();
            
            ConstructShip();
            ConstructEnemiesSpawner();
            
            _scoreCounter = new ScoreCounter(_bulletSpawner, _laserSpawner);
            
            ConstructHud();
            
        }
        
        private void OnDestroy()
        {
            _shipInput.Moved -= _shipMover.OnMoved;
            _shipInput.Rotated -= _shipRotator.OnRotated;
            _shipInput.BulletShooted -= _shipShooter.OnBulletShooted;
            _shipInput.LaserShooted -= _shipShooter.OnLaserShooted;
        }
        
        private void Restart()
        {
            _restarter.Restart();
            _shipMover.Restart();
            _laserGun.Restart();
            _scoreCounter.Restart();
            
            Ammunition[] ammunitions = _ship.GetComponentsInChildren<Ammunition>();

            foreach (Ammunition ammunition in ammunitions)
            {
                switch (ammunition)
                {
                    case Bullet bullet:
                        _bulletsFactory.Release(bullet);
                        break;
                    case Laser laser:
                        _lasersFactory.Release(laser);
                        break;
                }
            }
            
            _bulletsFactory.Clear();
            _lasersFactory.Clear();
            
            Enemie[] enemies = _enemiesSpawner.GetComponentsInChildren<Enemie>();

            foreach (Enemie enemy in enemies)
            {
                switch (enemy)
                {
                    case Asteroid asteroid:
                        _asteroidsFactory.Release(asteroid);
                        break;
                    case Ufo ufo:
                        _ufoFactory.Release(ufo);
                        break;
                    case AsteroidSmall asteroidSmall:
                        _asteroidSmallFactory.Release(asteroidSmall);
                        break;
                }
            }
            
            _asteroidsFactory.Clear();
            _ufoFactory.Clear();
            _asteroidSmallFactory.Clear();
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

        private void ConstructAmmunitionsPrefabs()
        {
            _bulletPrefab.Construct(_shipConfig.StartBulletSpeed, _shipConfig.MaxBulletSpeed);
            _laserPrefab.Construct(_shipConfig.StartLaserSpeed, _shipConfig.MaxBulletSpeed);
        }

        private void ConstructEnemiesPrefab()
        {
            _asteroidPrefab.Construct(_enemiesConfig.AsteroidSpeed, _enemiesConfig.MaxMovePositionX, _enemiesConfig.MaxMovePositionY);
            _ufoPrefab.ConstructPrefab(_enemiesConfig.UfoSpeed);
            _asteroidSmallPrefab.Construct(_enemiesConfig.AsteroidSmallSpeed);
        }
        
        private void ConstructShip()
        {
            _bulletsFactory = new BulletsFactory(_bulletPrefab, _shipConfig.CapacityBullets, _shipConfig.MaxCountBullets, _ship.transform);
            _bulletSpawner = new BulletSpawner(_bulletsFactory);

            _lasersFactory = new LasersFactory(_laserPrefab, _shipConfig.CapacityLasers, _shipConfig.MaxCountLasers, _ship.transform);
            _laserSpawner = new LaserSpawner(_lasersFactory);
            
            List<BulletGun> bulletGuns = _ship.GetComponentsInChildren<BulletGun>().ToList();
            _laserGun = _ship.GetComponentInChildren<LaserGun>();

            if (_ship.TryGetComponent(out ShipMover shipMover))
            {
                _shipMover = shipMover;
                shipMover.Construct(_shipConfig);
            }
                
            if(_ship.TryGetComponent(out ShipShooter shipShooter))
                _shipShooter = shipShooter;

            if (_ship.TryGetComponent(out ShipRotator shipRotator))
            {
                _shipRotator = shipRotator;
                shipRotator.Construct(_shipConfig);
            }

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
            
            _asteroidsFactory = new AsteroidsFactory(_asteroidPrefab, 10, 10, container);
            _ufoFactory = new UfoFactory(_ufoPrefab, 10, 10, container);
            _asteroidSmallFactory = new AsteroidSmallFactory(_asteroidSmallPrefab, 10, 10, container);
            
            _enemiesSpawner.Construct(_asteroidsFactory, _ufoFactory, _asteroidSmallFactory, _ship);
        }

        private void ConstructHud()
        {
            RestartWindow restartWindow = _hud.GetComponentInChildren<RestartWindow>();

            if (_hud.TryGetComponent(out Restarter restarter))
            {
                restarter.Construct(_shipTriggerObserver, restartWindow);
                _restarter = restarter;
            }

            ShipUI shipUI = _hud.GetComponentInChildren<ShipUI>();
            shipUI.Construct(_shipMover, _laserGun);

            ScoreCounterView scoreCounterView = restartWindow.GetComponentInChildren<ScoreCounterView>();
            scoreCounterView.Construct(_scoreCounter);
            
            restartWindow.RestartButton.onClick.AddListener(Restart);
        }
    }
}