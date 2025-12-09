using UnityEngine;

public class Pursuer : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed = 3f;
    private Rigidbody _rb;

    private void FixedUpdate() => HandleMovement();
    private void Awake() => _rb = gameObject.GetComponent<Rigidbody>();

    private void HandleMovement()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < 3)
            return;
        Vector3 direction = _target.transform.position - transform.position;
        direction.Normalize();
        Vector3 movement = direction * _moveSpeed;

        _rb.linearVelocity = new Vector3(movement.x, _rb.linearVelocity.y, movement.z);
    }
}