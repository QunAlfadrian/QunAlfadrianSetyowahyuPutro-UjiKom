using GDPP2.UjiKom.Input;
using System;
using UnityEngine;

namespace GDPP2.UjiKom.Traversal {
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IInitializable, IDisposable, IMovable {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private int _moveSpeed;
        private Vector2 _inputVector;

        public int MoveSpeed => _moveSpeed;
        public bool IsWalking { get; private set; }
        public Vector2 InputVector => _inputVector;

        public void Initialize() {
            GameContext.SceneEvents.Subscribe<PlayerMoveInputEvent>(OnPlayerMove);
        }

        public void Dispose() {
            GameContext.SceneEvents.Unsubscribe<PlayerMoveInputEvent>(OnPlayerMove);
        }

        public void Move() {
            Vector2 moveVector = _inputVector * _moveSpeed * Time.fixedDeltaTime;
            Vector3 velocity = Vector3.zero;
            velocity.x = moveVector.x;

            _rigidbody.linearVelocity = velocity;
        }

        private void OnPlayerMove(PlayerMoveInputEvent evt) {
            if (_inputVector.sqrMagnitude > 0) {
                IsWalking = true;
            } else {
                IsWalking = false;
            }

            _inputVector = evt.InputVector;
            Move();
        }

        #region Unity Lifecycle
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }
        private void Start() {
            GameContext.SceneServices.Register(this);
        }
        #endregion
    }
}