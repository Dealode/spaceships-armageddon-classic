using UnityEngine;

namespace CodeBase.Units
{
    [RequireComponent(typeof(Rigidbody))]
    public class UnitMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public float Speed => _rigidbody.velocity.magnitude;
        public Vector3 SpeedVector => _rigidbody.velocity;

        public UnitState State { get; private set; }

        private void Awake()
        {
            _rigidbody ??= GetComponentInParent<Rigidbody>();
        }

        public void InitializeWithState(UnitState unitState)
        {
            State = unitState;
        }

        public void Move(Vector3 direction)
        {
            if (direction.sqrMagnitude < 0.01f)
                return;

            MoveTowards(ref _rigidbody, direction);
        }

        private void MoveTowards(ref Rigidbody rigidbody, Vector3 direction)
        {
            // Move rigidbody towards direction
            var velocity = rigidbody.velocity;
            var forward = rigidbody.transform.forward;
            var angle = Vector3.Angle(forward, direction);

            if (velocity.magnitude < State.Speed)
                rigidbody.AddForce(
                    forward * direction.magnitude * State.Speed * AngleFactor(angle) / State.Acceleration *
                    Time.deltaTime,
                    ForceMode.VelocityChange);
            
            if (angle > 1f)
                rigidbody.rotation = CalcRotation(direction, forward);
        }

        private float AngleFactor(float angle)
        {
            return Mathf.Cos(angle * Mathf.PI / 180);
        }

        private Quaternion CalcRotation(Vector3 direction, Vector3 forward)
        {
            var step = State.RotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(forward, direction, step, 0.0f);
            return Quaternion.LookRotation(newDir);
        }
    }
}