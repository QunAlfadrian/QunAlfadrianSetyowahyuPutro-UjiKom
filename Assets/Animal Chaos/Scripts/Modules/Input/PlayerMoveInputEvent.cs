using UnityEngine;

namespace GDPP2.UjiKom.Input {
    public struct PlayerMoveInputEvent {
        public Vector2 InputVector { get; private set; }

        public PlayerMoveInputEvent(Vector2 inputVector) {
            InputVector = inputVector;
        }
    }
}