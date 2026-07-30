using UnityEngine;

public class GameplayTimer : MonoBehaviour {
    [SerializeField] private float _timeLimit;
    [SerializeField] private float _timeRemaining;
    [SerializeField] private float _timeElapsed;
    private bool _paused;

    private void Start() {
        _timeElapsed = 0f;
        _timeRemaining = _timeLimit;
    }

    private void Update() {
        if (_paused) {
            return;
        }

        _timeElapsed += Time.deltaTime;
        _timeRemaining -= Time.deltaTime;

        GameContext.SceneEvents.Publish(new GameplayTimerEvent(_timeElapsed, _timeRemaining));

        Debug.Log($"Time Elapsed: <color=yellow>{_timeElapsed.ToString("00")} s</color>");

        if (_timeRemaining <= 0) {
            _paused = true;
            // publish gameover event
        }
    }
}
