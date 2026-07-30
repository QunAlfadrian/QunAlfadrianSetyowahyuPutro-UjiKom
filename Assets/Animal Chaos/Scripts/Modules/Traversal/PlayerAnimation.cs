using GDPP2.UjiKom.Input;
using System;
using UnityEngine;

namespace GDPP2.UjiKom.Traversal {
    public class PlayerAnimation : MonoBehaviour, IInitializable, IDisposable {
        [SerializeField] private Player _player;
        [SerializeField] private Animator _animator;

        private void Start() {
            GameContext.SceneServices.Register(this);
        }

        public void Initialize() {
            GameContext.SceneEvents.Subscribe<AttackInputEvent>(OnAttackInput);
            GameContext.SceneEvents.Subscribe<GameOverEvent>(OnGameOver);
        }

        public void Dispose() {
            GameContext.SceneEvents.Unsubscribe<AttackInputEvent>(OnAttackInput);
            GameContext.SceneEvents.Unsubscribe<GameOverEvent>(OnGameOver);
        }

        private void OnGameOver(GameOverEvent evt) {
            _animator.SetBool("GameOver", true);
        }

        private void OnAttackInput(AttackInputEvent evt) {
            _animator.SetTrigger("Throw");
        }

        private void FixedUpdate() {
            _animator.SetBool("IsWalking", _player.IsWalking);
            _animator.SetFloat("HorizontalVelocity", _player.InputVector.x);
        }
    }
}