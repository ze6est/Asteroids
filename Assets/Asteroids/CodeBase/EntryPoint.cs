using Asteroids.CodeBase.Controllers;
using Asteroids.CodeBase.Input;
using UnityEngine;
using Asteroids.CodeBase.Ships;
using Asteroids.CodeBase.UI;

namespace Asteroids.CodeBase
{
    public class EntryPoint : MonoBehaviour
    {
        private Ship _shipPrefab;

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
            if(ship.TryGetComponent(out ShipMover shipMover))
                if(ship.TryGetComponent(out ShipRotator shipRotator))
                    if(ship.TryGetComponent(out ShipShooter shipShooter))
                    {
                        ShipController shipController = new ShipController(shipInput, shipMover, shipRotator, shipShooter);
                    }
        }

        private void InstantiateWorld()
        {
            _ship = Instantiate(_shipPrefab);
        }
        
        private void LoadPrefabs()
        {
            _shipPrefab = Resources.Load<Ship>(AssetsPath.SHIP_PATH);
            //_enemiesSpawnerPrefab = Resources.Load<EnemiesSpawner>(AssetsPath.ENEMIES_SPAWNER_PATH);
            //_hudPrefab = Resources.Load<Canvas>(AssetsPath.HUD_PATH);
            //_restartWindowPrefab = Resources.Load<RestartWindow>(AssetsPath.RESTART_WINDOW_PATH);
            //_shipUIPrefab = Resources.Load<ShipUI>(AssetsPath.SHIP_UI_PATH);
        }
    }
}