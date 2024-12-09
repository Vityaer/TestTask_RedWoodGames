using Gameplay.Services;
using Models.Games;
using System;
using UniRx;
using Zenject;

namespace Gameplay.Players.Common
{
    public class PlayerController : IInitializable, IDisposable
    {
        private readonly ILevelManager _levelManager;
        private readonly Player _player;
        private readonly StorageService _storageService;

        private readonly CompositeDisposable _disposables = new();
        
        public PlayerController(StorageService storageService, ILevelManager levelManager, Player player)
        {
            _storageService = storageService;
            _levelManager = levelManager;
            _player = player;
        }

        public void Initialize()
        {
            _levelManager.OnStartGame.Subscribe(_ => Restart()).AddTo(_disposables);
            Restart();
        }

        private void Restart()
        {
            _player.SetPosition(_storageService.LevelSettings.LevelContainer.PlayerStartPoint.position);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
