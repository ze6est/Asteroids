using Asteroids.CodeBase.Ships;
using Asteroids.CodeBase.UI;
using UnityEngine;

namespace Asteroids.CodeBase
{
    public class Restarter : MonoBehaviour
    {
        private RestartWindow _restartWindow;
        private ShipTriggerObserver _shipTriggerObserver;

        public void Construct(ShipTriggerObserver shipTriggerObserver, RestartWindow restartWindow)
        {
            _shipTriggerObserver = shipTriggerObserver;
            _restartWindow = restartWindow;
            
            _restartWindow.gameObject.SetActive(false);
            _shipTriggerObserver.Died += OnDied;
        }
        
        private void Start()
        {
            Time.timeScale = 1;
        }

        private void OnDestroy() => 
            _shipTriggerObserver.Died -= OnDied;

        private void OnDied()
        {
            Time.timeScale = 0;
            _restartWindow.gameObject.SetActive(true);
        }
    }
}
