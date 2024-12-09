using UniRx;

namespace Gameplay.Services
{
    public interface ILevelManager
    {
        ReactiveCommand OnGameOver { get; }
        ReactiveCommand OnStartGame { get; }
        void Restart();
        void GameOver();
    }
}