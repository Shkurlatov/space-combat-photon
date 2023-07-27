using SpaceCombat.Gameplay.Ship;
using UnityEngine;

namespace SpaceCombat.Gameplay.Combat
{
    public class Bullet : MonoBehaviour
    {
        private const string SPACE_SHIP = "SpaceShip";

        public float Speed = 100.0f;
        public float DestroyTime = 3.0f;

        public void Start()
        {
            Destroy(gameObject, DestroyTime);
        }

        public void Initialize(Vector3 originalDirection, float speed, float lag, float destroyTime)
        {
            transform.forward = originalDirection;

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = originalDirection * speed;
            rigidbody.position += rigidbody.velocity * lag;

            Destroy(gameObject, destroyTime);
        }

        public void InitializeBullet(Vector3 originalDirection, float lag)
        {
            transform.forward = originalDirection;

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = originalDirection * Speed;
            rigidbody.position += rigidbody.velocity * lag;
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(SPACE_SHIP))
            {
                collision.gameObject.GetComponent<ShipProtection>().TakeDamage();

                Destroy(gameObject);
            }
        }
    }
}