using TMPro;
using UnityEngine;

namespace Asteroids.CodeBase.UI
{
    public class ScoreCounterView : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private TextMeshProUGUI _score;

        private void Awake() => 
            _scoreCounter.ScoreChanged += OnScoreChanged;

        private void OnDestroy() => 
            _scoreCounter.ScoreChanged -= OnScoreChanged;

        private void OnScoreChanged(int score) => 
            _score.text = $"Score: {score}";
    }
}