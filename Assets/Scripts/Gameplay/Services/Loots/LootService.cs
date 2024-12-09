using Gameplay.Common.WrapperPools;
using Gameplay.Enemies.Abstractions;
using Gameplay.Loots;
using Gameplay.Services.Bullets;
using Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using Zenject;

namespace Gameplay.Services.Loots
{
    public class LootService : ILootService, IInitializable, IDisposable
    {
        private readonly StorageService _storageService;
        private readonly IEnemiesManager _enemiesManager;
        private readonly IBulletService _bulletService;
        private readonly ILevelManager _levelManager;

        private readonly List<Loot> _workLoots = new();

        private WrapperPool<Loot> _lootPool;
        private CompositeDisposable _disposables = new();
        private ReactiveCommand<Loot> _onGetUpLoot = new();

        public IObservable<Loot> OnGetUpLoot => _onGetUpLoot;

        public LootService(
            StorageService storageService,
            IEnemiesManager enemiesManager,
            IBulletService bulletService,
            ILevelManager levelManager
            )
        {
            _enemiesManager = enemiesManager;
            _storageService = storageService;
            _bulletService = bulletService;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            _levelManager.OnStartGame.Subscribe(_ => ClearAll()).AddTo(_disposables);
            _levelManager.OnGameOver.Subscribe(_ => ClearAll()).AddTo(_disposables);
            _lootPool = new WrapperPool<Loot>(_storageService.GameplaySettings.LootPrefab, OnCreateLoot);
            _enemiesManager.OnDestroyEnemy.Subscribe(OnDestroyEnemy).AddTo(_disposables);
        }

        private void ClearAll()
        {
            foreach(var loot in _workLoots)
                _lootPool.Release(loot);
            _workLoots.Clear();
        }

        private void OnCreateLoot(Loot loot)
        {
            loot.OnPickUp.Subscribe(OnPickUpLoot).AddTo(_disposables);
        }

        private void OnPickUpLoot(Loot loot)
        {
            _onGetUpLoot.Execute(loot);
            _lootPool.Release(loot);
            _bulletService.AddAmmo(loot.AmmoBonus);
            _workLoots.Remove(loot);
            loot.Stop();
        }

        private void OnDestroyEnemy(AbstractEnemy enemy)
        {
            var loot = _lootPool.Get();
            loot.SetData(enemy.transform.position, enemy.Settings.AmmoBonus);
            _workLoots.Add(loot);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
