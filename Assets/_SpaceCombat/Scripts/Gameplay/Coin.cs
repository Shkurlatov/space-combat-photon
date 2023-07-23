using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

namespace SpaceCombat.Gameplay
{
    public class Coin : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("SpaceShip"))
            {
                other.GetComponent<PhotonView>().Owner.AddScore(1);

                Destroy(gameObject);
            }
        }

        public void OnCollisionEnter(Collision collision)
        {

            //if (isDestroyed)
            //{
            //    return;
            //}

            //if (collision.gameObject.CompareTag("Bullet"))
            //{
            //    if (photonView.IsMine)
            //    {
            //        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            //        bullet.Owner.AddScore(isLargeAsteroid ? 2 : 1);

            //        DestroyAsteroidGlobally();
            //    }
            //    else
            //    {
            //        DestroyAsteroidLocally();
            //    }
            //}
            //else if (collision.gameObject.CompareTag("Player"))
            //{
            //    if (photonView.IsMine)
            //    {
            //        collision.gameObject.GetComponent<PhotonView>().RPC("DestroySpaceship", RpcTarget.All);

            //        DestroyAsteroidGlobally();
            //    }
            //}
        }
    }
}