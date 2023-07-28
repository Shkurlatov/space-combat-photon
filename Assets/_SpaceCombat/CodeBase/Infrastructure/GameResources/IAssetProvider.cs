using SpaceCombat.Infrastructure.Services;
using UnityEngine;

namespace SpaceCombat.Infrastructure.GameResources
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
        GameObject InstantiateGlobal(string path, Vector3 position);
        GameObject InstantiateGlobal(string path, Vector3 position, Quaternion rotation);
    }
}
