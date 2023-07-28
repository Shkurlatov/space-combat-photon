using Photon.Pun;
using UnityEngine;

namespace SpaceCombat.Infrastructure.GameResources
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Transform parent)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, parent);
        }

        public GameObject InstantiateGlobal(string path, Vector3 position)
        {
            return PhotonNetwork.InstantiateRoomObject(path, position, Quaternion.identity, 0);
        }

        public GameObject InstantiateGlobal(string path, Vector3 position, Quaternion rotation)
        {
            return PhotonNetwork.Instantiate(path, position, rotation, 0);
        }
    }
}
