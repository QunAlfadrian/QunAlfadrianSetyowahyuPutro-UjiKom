using UnityEngine;

namespace GDPP2.UjiKom.AnimalSystem {
    [CreateAssetMenu(menuName = "Data/Animal", order = 0)]
    public class AnimalData : ScriptableObject {
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _score;
        [SerializeField] private GameObject _prefab;

        public int MoveSpeed => _moveSpeed;
        public int Score => _score;
        public GameObject Prefab => _prefab;

        public GameObject GetInstance(Transform parent) {
            return Instantiate(_prefab, parent);
        }
    }
}