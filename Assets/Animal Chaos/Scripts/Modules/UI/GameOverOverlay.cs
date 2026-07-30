using GDPP2.UjiKom.Scoring;
using System;
using TMPro;
using UnityEngine;

namespace GDPP2.UjiKom.UI {
    [RequireComponent(typeof(CanvasGroup))]
    public class GameOverOverlay : MonoBehaviour, IInitializable, IDisposable {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _tmp_score;

        private void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start() {
            GameContext.SceneServices.Register(this);
        }

        public void Initialize() {
            GameContext.SceneEvents.Subscribe<ScoreChangedEvent>(OnScoreChanged);
            GameContext.SceneEvents.Subscribe<GameOverEvent>(OnGameOver);
        }

        public void Dispose() {
            GameContext.SceneEvents.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
            GameContext.SceneEvents.Unsubscribe<GameOverEvent>(OnGameOver);
        }

        private void OnScoreChanged(ScoreChangedEvent evt) {
            _tmp_score.text = $"Score : {evt.Score.ToString("000")}";
        }

        private void OnGameOver(GameOverEvent evt) {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }
    }
}
