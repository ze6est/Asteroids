using System.Collections.Generic;
using System.Linq;
using Asteroids.CodeBase.Ammunitions;
using Asteroids.CodeBase.Controllers;
using Asteroids.CodeBase.Factories.AmmunitionsFactories;
using Asteroids.CodeBase.Guns;
using Asteroids.CodeBase.Input;
using UnityEngine;
using Asteroids.CodeBase.Ships;
using Asteroids.CodeBase.Spawners.AmmunitionsSpawners;

namespace Asteroids.CodeBase
{
    public class EntryPoint : MonoBehaviour
    {
        private Ship _shipPrefab;
        private Bullet _bulletPrefab;
        private Laser _laserPrefab;

        private Ship _ship;
        private InputActions _input;
        private ShipInput _shipInput;
        
        private void Awake()
        {
            LoadPrefabs();
            InstantiateWorld();

            _input = new InputActions();
            _shipInput = new ShipInput(_input);
            _shipInput.Enable();
            
            ConstructShip(_ship,_shipInput);
        }

        private void ConstructShip(Ship ship, ShipInput shipInput)
        {
            BulletsFactory bulletsFactory = new BulletsFactory(_bulletPrefab, 10, 10, ship.transform);
            BulletSpawner bulletSpawner = new BulletSpawner(bulletsFactory);

            LasersFactory lasersFactory = new LasersFactory(_laserPrefab, 10, 10, ship.transform);
            LaserSpawner laserSpawner = new LaserSpawner(lasersFactory);
            
            List<BulletGun> bulletGuns = ship.GetComponentsInChildren<BulletGun>().ToList();
            List<LaserGun> laserGuns = ship.GetComponentsInChildren<LaserGun>().ToList();
            
            if(ship.TryGetComponent(out ShipMover shipMover))
                if(ship.TryGetComponent(out ShipRotator shipRotator))
                    if(ship.TryGetComponent(out ShipShooter shipShooter))
                    {
                        new ShipController(shipInput, shipMover, shipRotator, shipShooter, bulletGuns, laserGuns, bulletSpawner, laserSpawner);
                    }
        }

        private void InstantiateWorld()
        {
            _ship = Instantiate(_shipPrefab);
        }
        
        private void LoadPrefabs()
        {
            _shipPrefab = Resources.Load<Ship>(AssetsPath.SHIP_PATH);
            _bulletPrefab = Resources.Load<Bullet>(AssetsPath.BULLET_PATH);
            _laserPrefab = Resources.Load<Laser>(AssetsPath.LASER_PATH);

            //_enemiesSpawnerPrefab = Resources.Load<EnemiesSpawner>(AssetsPath.ENEMIES_SPAWNER_PATH);
            //_hudPrefab = Resources.Load<Canvas>(AssetsPath.HUD_PATH);
            //_restartWindowPrefab = Resources.Load<RestartWindow>(AssetsPath.RESTART_WINDOW_PATH);
            //_shipUIPrefab = Resources.Load<ShipUI>(AssetsPath.SHIP_UI_PATH);
        }
    }
}