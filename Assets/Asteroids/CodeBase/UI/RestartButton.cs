using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Asteroids.CodeBase.UI
{
    public class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;

        private void OnEnable() => 
            _restartButton.onClick.AddListener(Restart);

        private void OnDisable() => 
            _restartButton.onClick.RemoveListener(Restart);

        private void Restart() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}