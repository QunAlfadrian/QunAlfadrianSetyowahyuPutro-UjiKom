using System;
using UnityEngine;

namespace GDPP2.UjiKom.Scoring {
    public class ScoreService : MonoBehaviour, IInitializable, IDisposable {
        [SerializeField] private int _score;

        public int Score => _score;

        public void Initialize() {
            GameContext.SceneEvents.Subscribe<ScoreEvent>(OnScore);
        }

        public void Dispose() {
            GameContext.SceneEvents.Unsubscribe<ScoreEvent>(OnScore);
        }

        private void OnScore(ScoreEvent evt) {
            _score += evt.Score;
            _score = Mathf.Max(0, _score);

            GameContext.SceneEvents.Publish(new ScoreChangedEvent(Score));

            Debug.Log($"Current Score: <color=white>{_score.ToString("000")}</color>");
        }
    }
}