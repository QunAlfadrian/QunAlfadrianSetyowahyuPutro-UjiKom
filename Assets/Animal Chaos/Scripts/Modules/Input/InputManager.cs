using UnityEngine;
using UnityEngine.InputSystem;

namespace GDPP2.UjiKom.Input {
    public class InputManager : MonoBehaviour {
        private InputSystem_Actions _inputActions;

        private void ReadMoveInput() {
            Vector2 moveInput = _inputActions.Player.Move.ReadValue<Vector2>();

            GameContext.SceneEvents.Publish(new PlayerMoveInputEvent(moveInput));
        }

        private void Awake() {
            _inputActions = new InputSystem_Actions();
        }

        private void Start() {
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