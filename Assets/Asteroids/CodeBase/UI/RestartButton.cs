using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Asteroids.CodeBase.UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        public event UnityAction Click;

        private void OnEnable() => 
            _restartButton.onClick.AddListener(Restart);

        private void OnDisable() => 
            _restartButton.onClick.RemoveListener(Restart);

        private void Restart() => 
            Click?.Invoke();
    }
}