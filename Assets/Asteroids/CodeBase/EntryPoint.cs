using Asteroids.CodeBase.Input;
using UnityEngine;
using Asteroids.CodeBase.Ships;
using Asteroids.CodeBase.UI;

namespace Asteroids.CodeBase
{
    public class EntryPoint : MonoBehaviour
    {
        private Ship _shipPrefab;
        private EnemiesSpawner _enemiesSpawnerPrefab;
        private Canvas _hudPrefab;
        private RestartWindow _restartWindowPrefab;
        private ShipUI _shipUIPrefab;

        private Ship _ship;
        private Canvas _hud;
        private RestartWindow _restartWindow;
        private ShipUI _shipUI;
        
        private void Awake()
        {
            LoadPrefabs();
            
            Camera cameraMain = Camera.main;
            
            ScoreCounter scoreCounter = new ScoreCounter();
            InputActions input = new InputActions();
            ShipInput shipInput = new ShipInput(input);

            if (_shipPrefab.TryGetComponent(out ShipMover shipMover))
                shipMover.Construct(shipInput);

            if (_shipPrefab.TryGetComponent(out ShipRotator shipRotator))
                shipRotator.Construct(shipInput, cameraMain);
            
            if(_shipPrefab.TryGetComponent(out ShipShooter shipShooter))
                shipShooter.Construct(shipInput);
            
            _enemiesSpawnerPrefab.Construct(_shipPrefab);

            _ship = Instantiate(_shipPrefab);
            _hud = Instantiate(_hudPrefab);
            
            _restartWindow = Instantiate(_restartWindowPrefab);
            _restartWindow.transform.parent = _hud.transform;
            
            _shipUI = Instantiate(_shipUIPrefab);
            _shipUI.transform.parent = _hud.transform;
            
            _shipUI.Construct(shipMover, shipShooter);

            if (_ship.TryGetComponent(out ShipTriggerObserver shipTriggerObserver))
            {
            }

            if(_hud.TryGetComponent(out Restarter restarter))
                restarter.Construct(shipTriggerObserver, _restartWindow);
        }

        private void LoadPrefabs()
        {
            _shipPrefab = Resources.Load<Ship>(AssetsPath.SHIP_PATH);
            _enemiesSpawnerPrefab = Resources.Load<EnemiesSpawner>(AssetsPath.ENEMIES_SPAWNER_PATH);
            _hudPrefab = Resources.Load<Canvas>(AssetsPath.HUD_PATH);
            _restartWindowPrefab = Resources.Load<RestartWindow>(AssetsPath.RESTART_WINDOW_PATH);
            _shipUIPrefab = Resources.Load<ShipUI>(AssetsPath.SHIP_UI_PATH);
        }
    }
}