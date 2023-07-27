using SpaceCombat.Infrastructure.Services;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        GameObject InstantiateLobbyManager();
        void InstantiateUIRoot();
        void InstantiatePopup();
    }
}
