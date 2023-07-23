using SpaceCombat.Infrastructure.Services;
using UnityEngine;

namespace SpaceCombat.Infrastructure.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool IsAttackButtonUp();
    }
}
