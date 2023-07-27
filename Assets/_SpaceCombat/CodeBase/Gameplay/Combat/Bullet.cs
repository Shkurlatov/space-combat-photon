using SpaceCombat.Gameplay.Ship;
using UnityEngine;

namespace SpaceCombat.Gameplay.Combat
{
    public class Bullet : MonoBehaviour
    {
        public float Speed = 100.0f;
        public float DestroyTime = 3.0f;

        public void Start()
        {
            Destroy(gameObject, DestroyTime);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("SpaceShip"))
            {
                collision.gameObject.GetComponent<ShipProtection>().TakeDamage();

                Destroy(gameObject);
            }
        }

        public void InitializeBullet(Vector3 originalDirection, float lag)
        {
            transform.forward = originalDirection;

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = originalDirection * Speed;
            rigidbody.position += rigidbody.velocity * lag;
        }
    }
}