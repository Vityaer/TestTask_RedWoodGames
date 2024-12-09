using Gameplay.Enemies.Abstractions;
using Gameplay.Players;
using Gameplay.Services.Bullets;
using Models.Games.Bullets;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UniRx;
using Zenject;

namespace Gameplay.Services.Collisions
{
    public class CollisionService : ICollsionService, IInitializable, IDisposable
    {
        private readonly IEnemiesManager _enemiesManager;
        private readonly IBulletService _bulletService;
        private readonly ILevelManager _levelManager;

        private readonly CompositeDisposable _disposables = new();
        private readonly Dictionary<AbstractEnemy, IDisposable> _enemies = new();

        private ReactiveCommand _onEnemyCollisionPlayer = new();
        private ReactiveCommand<AbstractBullet> _onBulletCollion = new();

        public ReactiveCommand OnEnemyCollisionPlayer => _onEnemyCollisionPlayer;
        public IObservable<AbstractBullet> OnBulletCollion => _onBulletCollion;

        public CollisionService(IEnemiesManager enemiesManager, IBulletService bulletService, ILevelManager levelManager)
        {
            _enemiesManager = enemiesManager;
            _bulletService = bulletService;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            _levelManager.OnGameOver.Subscribe(_ => ClearAll()).AddTo(_disposables);
            _enemiesManager.OnSpawnEnemy.Subscribe(OnSpawnEnemy).AddTo(_disposables);
            _enemiesManager.OnDestroyEnemy.Subscribe(OnDestroyEnemy).AddTo(_disposables);
            _bulletService.OnBulletTargetCollision.Subscribe(OnBulletTargetCollision).AddTo(_disposables);
        }

        private void OnBulletTargetCollision(BulletCollision bulletCollision)
        {
            _onBulletCollion.Execute(bulletCollision.Bullet);
            AbstractEnemy enemyTarget = null;
            foreach (var enemy in _enemies)
            {
                if (enemy.Key.gameObject.Equals(bulletCollision.Target))
                {
                    enemyTarget = enemy.Key;
                    break;
                }
            }

            if (enemyTarget == null)
                return;

            enemyTarget.Health.ApplyDamage(bulletCollision.Damage);
        }

        private void ClearAll()
        {
            _enemies.Values.ForEach(dispose => dispose.Dispose());
            _enemies.Clear();
        }

        private void OnSpawnEnemy(AbstractEnemy enemy)
        {
            var disposable = enemy.OnPlayerColiision.Subscribe(_ => OnEnemyColiisionPlayer());
            _enemies.Add(enemy, disposable);
        }

        private void OnDestroyEnemy(AbstractEnemy enemy)
        {
            _enemies[enemy].Dispose();
            _enemies.Remove(enemy);
        }

        private void OnEnemyColiisionPlayer()
        {
            _onEnemyCollisionPlayer.Execute();
        }

        public void Dispose()
        {
            ClearAll();
            _disposables.Dispose();
        }
    }
}
