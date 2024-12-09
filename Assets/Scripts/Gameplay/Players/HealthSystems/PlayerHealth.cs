using Gameplay.Services;
using Gameplay.Services.Bullets;
using Gameplay.Services.Collisions;
using System;
using UniRx;
using Zenject;

namespace Gameplay.Players.HealthSystems
{
    public class PlayerHealth : IPlayerHealth, IInitializable, IDisposable
    {
        private readonly IBulletService _bulletService;
        private readonly ICollsionService _collsionService;
        private readonly ILevelManager _levelManager;

        private readonly CompositeDisposable _disposables = new();
        private ReactiveCommand _onPlayerDeath = new();

        public ReactiveCommand OnPlayerDeath => _onPlayerDeath;

        public PlayerHealth(IBulletService bulletService, ICollsionService collsionService, ILevelManager levelManager)
        {
            _bulletService = bulletService;
            _collsionService = collsionService;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            _collsionService.OnEnemyCollisionPlayer.Subscribe(_ => Death()).AddTo(_disposables);
            _bulletService.OnEmptyAmmoCount.Subscribe(_ => Death()).AddTo(_disposables);
        }

        private void Death()
        {
            _levelManager.GameOver();
            OnPlayerDeath.Execute();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
