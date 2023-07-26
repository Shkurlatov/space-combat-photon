using SpaceCombat.Infrastructure.Services;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        GameObject CreateMainLobbyPanel();
        void CreatePopup();
        void CreateUIRoot();
    }
}
