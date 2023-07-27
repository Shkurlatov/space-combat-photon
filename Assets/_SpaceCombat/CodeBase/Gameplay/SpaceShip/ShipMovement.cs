using SpaceCombat.Infrastructure.Input;
using SpaceCombat.Utilities;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class ShipMovement : MonoBehaviour
    {
        private const float INPUT_SENSITIVITY = 0.001f;
        private const float FORCE_MULTIPLIER = 1000.0f;

        public float RotationSpeed = 28.0f;
        public float MovementSpeed = 12.0f;

        private IInputService _input;
        private float _force;

        private Rigidbody _rigidbody;
        private ScreenSize _screenSize;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(IInputService input, ScreenSize screenSize)
        {
            _input = input;
            _screenSize = screenSize;
        }

        private void Update()
        {
            if (_input.Axis.sqrMagnitude > INPUT_SENSITIVITY)
                _force = _input.Axis.sqrMagnitude * FORCE_MULTIPLIER;
        }

        public void FixedUpdate()
        {
            Vector3 direction = new Vector3(_input.Axis.x, 0, _input.Axis.y);

            if (direction != Vector3.zero)
            {
                Vector3 force = _force * MovementSpeed * Time.fixedDeltaTime * direction.normalized;
                _rigidbody.AddForce(force);
            }

            if (_rigidbody.velocity != Vector3.zero)
                transform.forward = Vector3.Slerp(transform.forward, _rigidbody.velocity, RotationSpeed * Time.fixedDeltaTime);

            CheckExitScreen();
        }

        private void CheckExitScreen()
        {
            if (Mathf.Abs(_rigidbody.position.x) > _screenSize.HalfWidth)
                _rigidbody.position = new Vector3(Mathf.Sign(_rigidbody.position.x) * _screenSize.HalfWidth, 0, _rigidbody.position.z);

            if (Mathf.Abs(_rigidbody.position.z) > _screenSize.HalfHeight)
                _rigidbody.position = new Vector3(_rigidbody.position.x, 0, Mathf.Sign(_rigidbody.position.z) * _screenSize.HalfHeight);
        }
    }
}