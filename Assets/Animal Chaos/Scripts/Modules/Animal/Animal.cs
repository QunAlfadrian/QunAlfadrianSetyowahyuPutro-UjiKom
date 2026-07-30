using GDPP2.UjiKom.Scoring;
using System;
using UnityEngine;

namespace GDPP2.UjiKom.AnimalSystem {
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class Animal : MonoBehaviour, IInitializable, IDisposable, IMovable, ISpawnable {
        [SerializeField] private AnimalData _data;
        private Rigidbody _rigidbody;

        public int MoveSpeed => _data.MoveSpeed;

        public void Move() {
            Vector3 movementVector = transform.forward * MoveSpeed * Time.fixedDeltaTime;

            _rigidbody.linearVelocity = movementVector;
        }

        public void Spawn() {
            Move();
        }

        public void Initialize() {
            GameContext.SceneEvents.Subscribe<GameOverEvent>(OnGameOver);
        }

        public void Dispose() {
            GameContext.SceneEvents.Unsubscribe<GameOverEvent>(OnGameOver);
        }

        private void OnGameOver(GameOverEvent evt) {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other) {
            bool isProjectile = other.CompareTag("Projectile");
            bool isAnimalBorder = other.CompareTag("AnimalBorder");

            if (isProjectile) {
                GameContext.SceneEvents.Publish(new ScoreEvent(_data.Score));
                Destroy(gameObject);
            } else if (isAnimalBorder) {
                GameContext.SceneEvents.Publish(new ScoreEvent(_data.ScorePenalty));
                Destroy(gameObject);
            }
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