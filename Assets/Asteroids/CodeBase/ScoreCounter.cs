using System.Collections.Generic;
using UnityEngine.Events;

namespace Asteroids.CodeBase
{
    public class ScoreCounter
    {
        private List<AmmunitionSpawner> _ammunitionSpawners;

        private int _score;

        public event UnityAction<int> ScoreChanged;

        public ScoreCounter()
        {
            _ammunitionSpawners = new List<AmmunitionSpawner>();
            
            _score = 0;
            ScoreChanged?.Invoke(_score);
        }

        private void OnDestroy()
        {
            foreach (AmmunitionSpawner ammunitionSpawner in _ammunitionSpawners)
                ammunitionSpawner.EnemieDestroyed -= OnEnemieDestroyed;
        }

        public void Subscribe(AmmunitionSpawner ammunitionSpawner)
        {
            _ammunitionSpawners.Add(ammunitionSpawner);

            ammunitionSpawner.EnemieDestroyed += OnEnemieDestroyed;
        }

        private void OnEnemieDestroyed()
        {
            _score++;
            
            ScoreChanged?.Invoke(_score);
        }
    }
}