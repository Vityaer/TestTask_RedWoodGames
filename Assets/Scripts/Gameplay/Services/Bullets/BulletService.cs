using Gameplay.Common.WrapperPools;
using Gameplay.Players;
using Gameplay.Players.AttackSystems;
using Gameplay.Players.Bullets;
using Gameplay.Services.Bullets;
using Models.Games;
using Models.Games.Bullets;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class BulletService : IBulletService, IInitializable, IDisposable
    {
        private readonly StorageService _storageService;
        private readonly CompositeDisposable _disposables = new();
        private readonly ILevelManager _levelManager;
        private readonly Player _player;

        private ReactiveCommand _onEmptyAmmoCount = new();
        private ReactiveCommand<int> _onChangeAmmoCount = new();
        private ReactiveCommand<BulletCollision> _onBulletTargetCollision = new();
        private WrapperPool<AbstractBullet> _bulletPool;

        private List<AbstractBullet> _workBillets = new();
        private ReactiveCommand<AbstractBullet> _onSpawnBullet = new();
        private int _currentAmmo;

        public int CurrentAmmo => _currentAmmo;
        public ReactiveCommand OnEmptyAmmoCount => _onEmptyAmmoCount;
        public IObservable<int> OnChangeAmmoCount => _onChangeAmmoCount;
        public IObservable<BulletCollision> OnBulletTargetCollision => _onBulletTargetCollision;
        public IObservable<AbstractBullet> OnSpawnBullet => _onSpawnBullet;

        public BulletService(
            Player player,
            StorageService storageService,
            ILevelManager levelManager
            )
        {
            _player = player;
            _storageService = storageService;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            var bulletParent = new GameObject("BulletsParent").transform;
            _bulletPool = new WrapperPool<AbstractBullet>(_storageService.GameplaySettings.Bullet, OnCreateBullet, bulletParent);
            _levelManager.OnStartGame.Subscribe(_ => Restart()).AddTo(_disposables);
            _levelManager.OnGameOver.Subscribe(_ => Restart()).AddTo(_disposables);
            Refresh();
        }

        public void Shoot()
        {
            if (_currentAmmo <= 0)
            {
                UnityEngine.Debug.LogError("Try shoot with 0 bullets in ammo.");
                return;
            }

            var bullet = _bulletPool.Get();
            _workBillets.Add(bullet);

            var direction = _player.Movement.IsFaceRight ? Vector2.right : Vector2.left;
            bullet.SetData(_player.BulletStartPoint.position, direction);
            _currentAmmo -= 1;
            _onChangeAmmoCount.Execute(_currentAmmo);
            _onSpawnBullet.Execute(bullet);

            if (_currentAmmo == 0)
                _onEmptyAmmoCount.Execute();
        }

        public void AddAmmo(int count)
        {
            _currentAmmo += count;
            _onChangeAmmoCount.Execute( _currentAmmo );
        }

        private void Restart()
        {
            foreach (var bullet in _workBillets)
                _bulletPool.Release(bullet);
            _workBillets.Clear();
            Refresh();
        }

        private void Refresh()
        {
            _currentAmmo = _storageService.LevelSettings.StartAmmo;
            _onChangeAmmoCount.Execute( _currentAmmo );

            if (_currentAmmo == 0)
            {
                UnityEngine.Debug.LogError("Start count ammo is 0.");
                _onEmptyAmmoCount.Execute();
            }
        }

        private void OnCreateBullet(AbstractBullet bullet)
        {
            bullet.OnObjectCollion.Subscribe(target => OnObjectCollion(bullet, target)).AddTo(_disposables);
        }

        private void OnObjectCollion(AbstractBullet bullet, GameObject target)
        {
            bullet.Stop();
            _bulletPool.Release(bullet);
            _workBillets.Remove(bullet);

            var bulletCollision = new BulletCollision(bullet, target, bullet.BulletSettings.Damage);
            _onBulletTargetCollision.Execute(bulletCollision);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
