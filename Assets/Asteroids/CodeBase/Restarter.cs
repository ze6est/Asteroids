using Asteroids.CodeBase.Ship;
using Asteroids.CodeBase.UI;
using UnityEngine;

namespace Asteroids.CodeBase
{
    public class Restarter : MonoBehaviour
    {
        [SerializeField] private RestartWindow _restartWindow;
        [SerializeField] private ShipTriggerObserver _shipTriggerObserver;

        private void Start()
        {
            Time.timeScale = 1;
            _restartWindow.gameObject.SetActive(false);
            _shipTriggerObserver.Died += OnDied;
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
