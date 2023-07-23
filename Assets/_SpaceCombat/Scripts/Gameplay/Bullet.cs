using SpaceCombat.Gameplay.Ship;
using UnityEngine;

namespace SpaceCombat.Gameplay
{
    public class Bullet : MonoBehaviour
    {
        public void Start()
        {
            Destroy(gameObject, 3.0f);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("SpaceShip"))
            {
                collision.gameObject.GetComponent<ShipDestroy>().DestroyShip();

                Destroy(gameObject);
            }
        }

        public void InitializeBullet(Vector3 originalDirection, float lag)
        {
            transform.forward = originalDirection;

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = originalDirection * 100.0f;
            rigidbody.position += rigidbody.velocity * lag;
        }
    }
}