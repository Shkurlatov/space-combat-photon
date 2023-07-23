using SpaceCombat.Infrastructure.Input;
using UnityEngine;

namespace SpaceCombat.Gameplay.Ship
{
    public class ShipMovement : MonoBehaviour
    {
        public float RotationSpeed = 20.0f;
        public float MovementSpeed = 10.0f;

        public const float Epsilon = 0.001f;

        private IInputService _input;
        private float _force = 0.0f;


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
            if (_input.Axis.sqrMagnitude > Epsilon)
                _force = _input.Axis.sqrMagnitude;
        }

        public void FixedUpdate()
        {
            Vector3 direction = new Vector3(_input.Axis.x, 0, _input.Axis.y);

            if (direction != Vector3.zero)
            {
                Vector3 force = _force * 1000.0f * MovementSpeed * Time.fixedDeltaTime * direction;
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