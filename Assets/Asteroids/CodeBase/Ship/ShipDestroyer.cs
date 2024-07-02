using Asteroids.CodeBase.Enemies;
using Asteroids.CodeBase.UI;
using UnityEngine;

namespace Asteroids.CodeBase.Ship
{
    public class ShipDestroyer : MonoBehaviour
    {
        [SerializeField] private RestartWindow _restartWindow;

        private void Start()
        {
            Time.timeScale = 1;
            _restartWindow.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Enemie>(out _))
            {
                Time.timeScale = 0;
                _restartWindow.gameObject.SetActive(true);
            }
        }
    }
}
