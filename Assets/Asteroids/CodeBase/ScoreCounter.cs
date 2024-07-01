using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Asteroids.CodeBase
{
    public class ScoreCounter : MonoBehaviour
    {
        private List<AmmunitionSpawner> _ammunitionSpawners = new();

        private int _score;

        public event UnityAction<int> ScoreChanged;

        private void OnDestroy()
        {
            foreach (AmmunitionSpawner ammunitionSpawner in _ammunitionSpawners)
            {
                ammunitionSpawner.EnemieDestroyed -= OnEnemieDestroyed;
            }
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