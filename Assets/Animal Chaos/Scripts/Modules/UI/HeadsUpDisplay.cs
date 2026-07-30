using GDPP2.UjiKom.Scoring;
using System;
using TMPro;
using UnityEngine;

namespace GDPP2.UjiKom.UI {
    [RequireComponent(typeof(CanvasGroup))]
    public class HeadsUpDisplay : MonoBehaviour, IInitializable, IDisposable{
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _tmp_score;
        [SerializeField] private TextMeshProUGUI _tmp_timer;
        private ScoreService _scoreService;
        private GameplayTimer _gameTimer;

        private void Start() {
            GameContext.SceneServices.Register(this);

            _scoreService = GameContext.SceneServices.Get<ScoreService>();
            _gameTimer = GameContext.SceneServices.Get<GameplayTimer>();

            _tmp_timer.text = $"Timer : {_gameTimer.TimeRemaining.ToString("00")}";
            _tmp_score.text = $"Score : {_scoreService.Score.ToString("000")}";
        }

        public void Initialize() {
            GameContext.SceneEvents.Subscribe<ScoreChangedEvent>(OnScoreChanged);
            GameContext.SceneEvents.Subscribe<GameplayTimerEvent>(OnGameTimeChanged);
            GameContext.SceneEvents.Subscribe<GameOverEvent>(OnGameOver);
        }

        public void Dispose() {
            GameContext.SceneEvents.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
            GameContext.SceneEvents.Unsubscribe<GameplayTimerEvent>(OnGameTimeChanged);
            GameContext.SceneEvents.Unsubscribe<GameOverEvent>(OnGameOver);
        }

        private void OnGameOver(GameOverEvent evt) {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void OnGameTimeChanged(GameplayTimerEvent evt) {
            _tmp_timer.text = $"Timer : {evt.TimeRemaining.ToString("00")}";
        }

        private void OnScoreChanged(ScoreChangedEvent evt) {
            _tmp_score.text = $"Score : {evt.Score.ToString("000")}";
        }
    }
}
