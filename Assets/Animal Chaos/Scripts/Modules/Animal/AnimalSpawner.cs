using System;
using UnityEngine;

namespace GDPP2.UjiKom.AnimalSystem {
    public class AnimalSpawner : MonoBehaviour, IInitializable, IDisposable {
        [Header("Interval Settings")]
        [SerializeField] private int _spawnPerInterval = 1;
        [SerializeField] private float _spawnInterval = 2f;
        private float _timeSinceLastInterval = 0;

        [Header("Spawn Area Settings")]
        [SerializeField] private Transform _min;
        [SerializeField] private Transform _max;

        [Header("Animal Settings")]
        [SerializeField] private AnimalData[] _animalsToSpawn;

        public void Initialize() {
            GameContext.SceneEvents.Subscribe<GameplayTimerEvent>(OnGameTimerChanged);
        }

        public void Dispose() {
            GameContext.SceneEvents.Unsubscribe<GameplayTimerEvent>(OnGameTimerChanged);
        }

        private void OnGameTimerChanged(GameplayTimerEvent evt) {
            if (evt.TimeElapsed - _timeSinceLastInterval >= _spawnInterval) {
                Spawn();

                _timeSinceLastInterval = evt.TimeElapsed;
            }
        }

        private void Spawn() {

            for (int i = 0; i < _spawnPerInterval; i++) {
                Vector3 spawnPos = GetSpawnPoint();
                int randomIndex = UnityEngine.Random.Range(0, _animalsToSpawn.Length);
                AnimalData animalToSpawn = _animalsToSpawn[randomIndex];

                Animal animal = animalToSpawn.GetAnimalInstance(transform);
                animal.transform.position = spawnPos;
                animal.transform.rotation = transform.rotation;
                animal.Move();

                Debug.Log($"Spawned <color=orange>{animalToSpawn.name}</color>");
            }
        }

        private Vector3 GetSpawnPoint() {
            Vector3 spawnPoint = _min.position;
            spawnPoint.x = UnityEngine.Random.Range(_min.position.x, _max.position.x);

            return spawnPoint;
        }

        private void Start() {
            GameContext.SceneServices.Register(this);
            _timeSinceLastInterval = 0;

            Spawn();
        }
    }
}