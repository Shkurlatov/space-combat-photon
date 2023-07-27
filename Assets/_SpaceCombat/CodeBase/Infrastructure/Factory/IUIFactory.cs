using Photon.Realtime;
using SpaceCombat.Infrastructure.Services;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        GameObject InstantiateLobbyManager();
        GameObject InstantiateUIRoot();
        GameObject InstantiateControlHud();
        void InstantiatePopup(Player winner, Transform parent);
    }
}
