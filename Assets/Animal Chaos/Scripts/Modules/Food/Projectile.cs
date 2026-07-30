using UnityEngine;

namespace GDPP2.UjiKom.AnimalSystem {
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour, IMovable, ISpawnable {
        [SerializeField] private ProjectileData _data;
        private Rigidbody _rigidbody;
        private float _spawnedTime;

        public int MoveSpeed => _data.MoveSpeed;
        public float LifeTime => _data.LifeTime;

        public void Move() {
            Vector3 movementVector = transform.forward * MoveSpeed * Time.fixedDeltaTime;
            _rigidbody.linearVelocity = movementVector;
        }

        public void Spawn() {
            _spawnedTime = GameContext.SceneServices.Get<GameplayTimer>().TimeElapsed;
            Move();
        }

        private void OnGameTimerChanged(GameplayTimerEvent evt) {
            if (evt.TimeElapsed - _spawnedTime >= LifeTime) {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other) {
            if (other.CompareTag("Animal")) {
                Destroy(gameObject);
            }
        }

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            GameContext.SceneEvents.Subscribe<GameplayTimerEvent>(OnGameTimerChanged);
        }

        private void OnDestroy() {
            GameContext.SceneEvents?.Unsubscribe<GameplayTimerEvent>(OnGameTimerChanged);
        }
    }
}