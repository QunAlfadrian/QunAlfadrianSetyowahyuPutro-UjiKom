using UnityEngine;

namespace GDPP2.UjiKom.AnimalSystem {
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class Animal : MonoBehaviour, IMovable, ISpawnable {
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

        #region Unity Lifecycle
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }
        #endregion
    }
}