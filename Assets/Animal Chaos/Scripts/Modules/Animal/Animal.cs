using UnityEngine;

namespace GDPP2.UjiKom.AnimalSystem {
    [SelectionBase]
    [RequireComponent(typeof(Rigidbody))]
    public class Animal : MonoBehaviour, IMovable {
        [SerializeField] private AnimalData _data;
        private Rigidbody _rigidbody;

        public int MoveSpeed => _data.MoveSpeed;

        public void Move() {
            Vector3 movementVector = Vector3.forward * MoveSpeed * Time.fixedDeltaTime;

            _rigidbody.linearVelocity = movementVector;
        }

        #region Unity Lifecycle
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start() {
            Move();
        }
        #endregion
    }
}