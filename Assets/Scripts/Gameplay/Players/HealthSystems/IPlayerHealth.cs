using UniRx;

namespace Gameplay.Players.HealthSystems
{
    public interface IPlayerHealth
    {
        ReactiveCommand OnPlayerDeath { get; }
    }
}