using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDPP2.UjiKom.Input {
    public class InputManager : MonoBehaviour, IInitializable, IDisposable {
        private InputSystem_Actions _inputActions;

        private void ReadMoveInput() {
            Vector2 moveInput = _inputActions.Player.Move.ReadValue<Vector2>();

            GameContext.SceneEvents.Publish(new PlayerMoveInputEvent(moveInput));
        }

        private void OnPlayerAttack(InputAction.CallbackContext ctx) {
            GameContext.SceneEvents.Publish(new AttackInputEvent());
        }

        public void Initialize() {
            GameContext.SceneEvents.Subscribe<GameOverEvent>(OnGameOver);
        }

        public void Dispose() {
            GameContext.SceneEvents.Unsubscribe<GameOverEvent>(OnGameOver);
        }

        private void OnGameOver(GameOverEvent evt) {
            _inputActions.Player.Disable();
        }

        private void Awake() {
            _inputActions = new InputSystem_Actions();

            _inputActions.Player.Attack.performed += OnPlayerAttack;
        }

        private void Start() {
            GameContext.SceneServices.Register(this);

            _inputActions.Player.Enable();
        }

        private void OnDestroy() {
            _inputActions.Player.Disable();
            _inputActions.Dispose();
        }

        private void FixedUpdate() {
            ReadMoveInput();
        }
    }
}