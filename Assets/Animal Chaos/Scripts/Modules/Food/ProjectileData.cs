using UnityEngine;

namespace GDPP2.UjiKom.AnimalSystem {
    [CreateAssetMenu(menuName = "Data/Projectile", order = 1)]
    public class ProjectileData : ScriptableObject {
        [SerializeField] private int _moveSpeed;
        [SerializeField] private float _lifeTime;
        [SerializeField] private GameObject _prefab;

        public int MoveSpeed => _moveSpeed;
        public float LifeTime => _lifeTime;
        public GameObject Prefab => _prefab;

        public Projectile GetInstance(Transform parent) {
            GameObject projectileObject = Instantiate(_prefab, parent);
            return projectileObject.GetComponent<Projectile>();
        }
    }
}