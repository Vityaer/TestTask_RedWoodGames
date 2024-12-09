using Cysharp.Threading.Tasks;
using Gameplay.Common;
using Gameplay.Common.Abstractions;
using Gameplay.Common.WrapperPools;
using Gameplay.Enemies.Abstractions;
using Gameplay.Players;
using Gameplay.Services.Bullets;
using Models.Games;
using Models.Games.Bullets;
using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using Utils.Asyncs;
using Zenject;

namespace Gameplay.Services
{
    public class EnemiesManager : GameplayService, IEnemiesManager, IInitializable
    {
        private readonly StorageService _storageService;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ILevelManager _levelManager;
        private readonly Player _player;

        private readonly List<AbstractEnemy> _enemies = new();
        private readonly Dictionary<AbstractEnemy, EnemyWrapperPool<AbstractEnemy>> _enemiesPools = new();
        private readonly Dictionary<AbstractEnemy, EnemyWrapperPool<AbstractEnemy>> _createdEnemies = new();

        private ReactiveCommand<AbstractEnemy> _onSpawnEnemy = new();
        private ReactiveCommand<AbstractEnemy> _onDestroyEnemy = new();
        private List<AbstractEnemy> _addedEnemies = new();
        private List<AbstractEnemy> _removeEnemies = new();

        public IObservable<AbstractEnemy> OnSpawnEnemy => _onSpawnEnemy;
        public IObservable<AbstractEnemy> OnDestroyEnemy => _onDestroyEnemy;

        public EnemiesManager(
            StorageService storageService,
            IEnemyFactory enemyFactory,
            ILevelManager levelManager,
            Player player
            )
        {
            _enemyFactory = enemyFactory;
            _storageService = storageService;
            _levelManager = levelManager;
            _player = player;
        }

        public void Initialize()
        {
            _levelManager.OnStartGame.Subscribe(_ => Restart()).AddTo(Disposables);
            foreach (var enemyContainer in _storageService.LevelSettings.EnemyContainers)
            {
                var pool = new EnemyWrapperPool<AbstractEnemy>(enemyContainer.Prefab, _enemyFactory, IternalOnEnemyCreate);
                _enemiesPools.Add(enemyContainer.Prefab, pool);
            }
        }

        private void Restart()
        {
            foreach (var enemy in _createdEnemies)
            {
                _enemies.Remove(enemy.Key);
                _createdEnemies[enemy.Key].Release(enemy.Key);
            }
            _createdEnemies.Clear();
            _addedEnemies.Clear();
            _removeEnemies.Clear();
        }

        public void AddEnemy(AbstractEnemy abstractEnemy)
        {
            var enemy = _enemiesPools[abstractEnemy].Get();
            _addedEnemies.Add(enemy);
            _createdEnemies.Add(enemy, _enemiesPools[abstractEnemy]);

            var points = _storageService.LevelSettings.LevelContainer.EnemyStartPoints;
            var randomPositionIndex = UnityEngine.Random
                .Range(0, points.Count);
            enemy.SetPosition(points[randomPositionIndex].position);
            enemy.Refresh();
            _onSpawnEnemy.Execute(enemy);
        }

        private void RemoveEnemy(AbstractEnemy abstractEnemy)
        {
            _removeEnemies.Add(abstractEnemy);
        }

        protected override void Tick()
        {
            foreach (var addedEnemy in _addedEnemies)
                _enemies.Add(addedEnemy);
            _addedEnemies.Clear();

            foreach (var removedEnemy in _removeEnemies)
            {
                _enemies.Remove(removedEnemy);
                _createdEnemies[removedEnemy].Release(removedEnemy);
                _createdEnemies.Remove(removedEnemy);
            }
            _removeEnemies.Clear();

            for (var i = 0; i < _enemies.Count; i++)
            {
                _enemies[i].EnemyMovement.Move();
            }
        }

        private void IternalOnEnemyCreate(AbstractEnemy enemy)
        {
            enemy.Init(_player);
            enemy.Health.OnDeath.Subscribe(_ => OnDeathEnemy(enemy)).AddTo(Disposables);
        }

        private void OnDeathEnemy(AbstractEnemy enemy)
        {
            _onDestroyEnemy.Execute(enemy);
            RemoveEnemy(enemy);
        }
    }
}
