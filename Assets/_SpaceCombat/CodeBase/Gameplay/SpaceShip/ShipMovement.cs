using SpaceCombat.Infrastructure.Input;
using SpaceCombat.Utilities;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class ShipMovement : MonoBehaviour
    {
        private const float INPUT_SENSITIVITY = 0.001f;
        private const float FORCE_MULTIPLIER = 1000.0f;

        private Rigidbody _rigidbody;

        private IInputService _input;
        private SpaceSize _spaceSize;
        private float _rotationSpeed;
        private float _movementSpeed;

        private float _force;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(IInputService input, SpaceSize screenSize, float rotationSpeed, float movementSpeed)
        {
            _input = input;
            _spaceSize = screenSize;
            _rotationSpeed = rotationSpeed;
            _movementSpeed = movementSpeed;
        }

        private void Update()
        {
            if (_input.Axis.sqrMagnitude > INPUT_SENSITIVITY)
            {
                _force = _input.Axis.sqrMagnitude * FORCE_MULTIPLIER;
            }
        }

        public void FixedUpdate()
        {
            Vector3 direction = new Vector3(_input.Axis.x, 0, _input.Axis.y);

            if (direction != Vector3.zero)
            {
                Vector3 force = _force * _movementSpeed * Time.fixedDeltaTime * direction.normalized;
                _rigidbody.AddForce(force);
            }

            if (_rigidbody.velocity != Vector3.zero)
            {
                transform.forward = Vector3.Slerp(transform.forward, _rigidbody.velocity, _rotationSpeed * Time.fixedDeltaTime);
            }

            CheckExitScreen();
        }

        private void CheckExitScreen()
        {
            if (Mathf.Abs(_rigidbody.position.x) > _spaceSize.HalfWidth)
            {
                _rigidbody.position = new Vector3(Mathf.Sign(_rigidbody.position.x) * _spaceSize.HalfWidth, 0, _rigidbody.position.z);
            }

            if (Mathf.Abs(_rigidbody.position.z) > _spaceSize.HalfHeight)
            {
                _rigidbody.position = new Vector3(_rigidbody.position.x, 0, Mathf.Sign(_rigidbody.position.z) * _spaceSize.HalfHeight);
            }
        }
    }
}