using GDPP2.UjiKom.Input;
using System;
using UnityEngine;

namespace GDPP2.UjiKom.AnimalSystem {
    public class ProjectileSpawner : MonoBehaviour, IInitializable, IDisposable {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private ProjectileData _projectileData;
        [SerializeField][Tooltip("Time (s) between each projectile")] private float _fireRate;
        private GameplayTimer _gameTimer;
        private float _timeSinceLastFire;
        bool _canFire;

        public void Initialize() {
            GameContext.SceneEvents.Subscribe<AttackInputEvent>(OnAttackInput);
            GameContext.SceneEvents.Subscribe<GameplayTimerEvent>(OnGameTimerChanged);
        }

        public void Dispose() {
            GameContext.SceneEvents.Unsubscribe<AttackInputEvent>(OnAttackInput);
            GameContext.SceneEvents.Unsubscribe<GameplayTimerEvent>(OnGameTimerChanged);
        }

        private void OnAttackInput(AttackInputEvent evt) {
            if (!_canFire) {
                return;
            }

            FireProjectile();
        }

        private void FireProjectile() {
            _canFire = false;
            _timeSinceLastFire = _gameTimer.TimeElapsed;
            Projectile projectile = _projectileData.GetInstance(transform);
            projectile.transform.position = _spawnPoint.position;
            projectile.transform.rotation = _spawnPoint.rotation;
            projectile.Spawn();
        }

        private void OnGameTimerChanged(GameplayTimerEvent evt) {
            if (evt.TimeElapsed - _timeSinceLastFire >= _fireRate) {
                _canFire = true;
            }
        }

        private void Start() {
            GameContext.SceneServices.Register(this);
            _gameTimer = GameContext.SceneServices.Get<GameplayTimer>();
        }
    }
}