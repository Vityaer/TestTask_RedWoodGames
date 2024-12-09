using Cysharp.Threading.Tasks;
using Models.Games;
using System;
using System.Threading;
using UniRx;
using UnityEngine;
using Utils.Asyncs;
using Zenject;

namespace Gameplay.Services
{
    public class WaveService : IWaveService, IInitializable, IDisposable
    {
        private readonly StorageService _storageService;
        private readonly LevelSettings _levelSettings;
        private readonly IEnemiesManager _enemiesManager;
        private readonly ILevelManager _levelManager;

        private CompositeDisposable _disposables = new();
        private CancellationTokenSource _cancellationTokenSource;

        public WaveService(StorageService storageService, IEnemiesManager enemiesManager, ILevelManager levelManager)
        {
            _storageService = storageService;
            _enemiesManager = enemiesManager;
            _levelManager = levelManager;

            _levelSettings = _storageService.LevelSettings;
        }

        public void Initialize()
        {
            _levelManager.OnGameOver.Subscribe(_ => Stop()).AddTo(_disposables);
            _levelManager.OnStartGame.Subscribe(_ => Start()).AddTo(_disposables);
        }

        public void Start()
        {
            _cancellationTokenSource.TryCancel();
            _cancellationTokenSource = new();
            CustomUpdate(_cancellationTokenSource.Token).Forget();
        }

        public void Stop()
        {
            _cancellationTokenSource.TryCancel();
            _cancellationTokenSource = null;
        }

        private async UniTaskVoid CustomUpdate(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var randomDelay = UnityEngine.Random
                    .Range(_levelSettings.EnemyCreateRandomTime.x,
                    _levelSettings.EnemyCreateRandomTime.y);

                await UniTask.Delay(Mathf.RoundToInt(randomDelay * 1000), cancellationToken: token);

                var randomEnemyIndex = UnityEngine.Random.Range(0, _levelSettings.EnemyContainers.Count);
                var randomEnemyContainer = _levelSettings.EnemyContainers[randomEnemyIndex];
                _enemiesManager.AddEnemy(randomEnemyContainer.Prefab);
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
            Stop();
        }
    }
}
