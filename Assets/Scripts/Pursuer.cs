using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    internal class Pursuer : MonoBehaviour
    {
        private const float MinDistanceToPlayer = 3f;

        [SerializeField] private Transform _target;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed = 3f;

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            if (Vector3.Distance(transform.position, _target.transform.position) < MinDistanceToPlayer)
                return;

            Vector3 direction = (_target.transform.position - transform.position).normalized;
            Vector3 movement = direction * _moveSpeed;

            _rigidbody.linearVelocity = new Vector3(movement.x, _rigidbody.linearVelocity.y, movement.z);
        }
    }
}