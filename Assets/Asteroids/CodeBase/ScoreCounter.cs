using System.Collections.Generic;
using Asteroids.CodeBase.Spawners;
using UnityEngine.Events;

namespace Asteroids.CodeBase
{
    public class ScoreCounter
    {
        /*
        private List<AmmunitionFactory> _ammunitionSpawners;

        private int _score;

        public event UnityAction<int> ScoreChanged;

        public ScoreCounter()
        {
            _ammunitionSpawners = new List<AmmunitionFactory>();
            
            _score = 0;
            ScoreChanged?.Invoke(_score);
        }

        private void OnDestroy()
        {
            foreach (AmmunitionFactory ammunitionSpawner in _ammunitionSpawners)
                ammunitionSpawner.EnemieDestroyed -= OnEnemieDestroyed;
        }

        public void Subscribe(AmmunitionFactory ammunitionFactory)
        {
            _ammunitionSpawners.Add(ammunitionFactory);

            ammunitionFactory.EnemieDestroyed += OnEnemieDestroyed;
        }

        private void OnEnemieDestroyed()
        {
            _score++;
            
            ScoreChanged?.Invoke(_score);
        }
        */
    }
}