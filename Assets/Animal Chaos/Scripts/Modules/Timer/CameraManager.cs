using System;
using UnityEngine;

public class CameraManager : MonoBehaviour, IInitializable, IDisposable {
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Camera _gameoverCamera;

    private void Start() {
        GameContext.SceneServices.Register(this);
    }

    public void Initialize() {
        GameContext.SceneEvents.Subscribe<GameOverEvent>(OnGameOver);
    }

    public void Dispose() {
        GameContext.SceneEvents.Unsubscribe<GameOverEvent>(OnGameOver);
    }

    private void OnGameOver(GameOverEvent evt) {
        _mainCamera.gameObject.SetActive(false);
        _gameoverCamera.gameObject.SetActive(true);
    }
}
