using Gameplay.Common.WrapperPools;
using Gameplay.Enemies.Abstractions;
using Gameplay.Loots;
using Gameplay.Players;
using Gameplay.Services;
using Gameplay.Services.Bullets;
using Gameplay.Services.Collisions;
using Gameplay.Services.Loots;
using Models.Games;
using Models.SO;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

namespace Gameplay.AudioSystems
{
    public class AudioSystemController : IInitializable, IDisposable
    {
        private readonly IBulletService _bulletService;
        private readonly SFXSettings _sFXSettings;
        private readonly ILevelManager _levelManager;
        private readonly IEnemiesManager _enemiesManager;
        private readonly ILootService _lootService;
        private readonly ICollsionService _collsionService;
        private readonly Player _player;

        private readonly CompositeDisposable _disposables = new();
        private readonly List<SFX> _workList = new();

        private WrapperPool<SFX> _wrapperPool;
        public AudioSystemController(
            StorageService storageService,
            IBulletService bulletService,
            ILevelManager levelManager,
            IEnemiesManager enemiesManager,
            ILootService lootService,
            ICollsionService collsionService,
            Player player)
        {
            _sFXSettings = storageService.SFXSettings;
            _bulletService = bulletService;
            _levelManager = levelManager;
            _enemiesManager = enemiesManager;
            _lootService = lootService;
            _collsionService = collsionService;
            _player = player;
        }


        public void Initialize()
        {
            var audioParent = new GameObject("Audio");
            _wrapperPool = new(_sFXSettings.SFXPrefab, OnSpawnSFX, audioParent.transform);

            _collsionService.OnBulletCollion.Subscribe(PlayerBulletCollionSFX).AddTo(_disposables);
            _lootService.OnGetUpLoot.Subscribe(PlayGetUpLootSFX).AddTo(_disposables);
            _enemiesManager.OnDestroyEnemy.Subscribe(PlayDeathEnemySFX).AddTo(_disposables);
            _levelManager.OnGameOver.Subscribe(_ => PlayGameOverSFX()).AddTo(_disposables);
            _bulletService.OnSpawnBullet.Subscribe(PlayShootSFX).AddTo(_disposables);
        }


        private void PlayerBulletCollionSFX(AbstractBullet bullet)
        {
            var randomIndex = UnityEngine.Random.Range(0, bullet.CollionSFX.Count);
            var clip = bullet.CollionSFX[randomIndex];
            PlayRandomPitchSFX(clip, bullet.transform.position);
        }

        private void PlayGetUpLootSFX(Loot loot)
        {
            PlayRandomPitchSFX(loot.GetUpSFX, loot.transform.position);
        }

        private void PlayDeathEnemySFX(AbstractEnemy enemy)
        {
            PlayRandomPitchSFX(enemy.DeathSFX, enemy.transform.position);
        }

        private void PlayGameOverSFX()
        {
            PlaySFX(_sFXSettings.GameOverSFX, _player.transform.position);
        }

        private void PlaySFX(AudioClip clip, Vector3 position)
        {
            var sfx = _wrapperPool.Get();
            _workList.Add(sfx);
            sfx.transform.position = position;
            sfx.Play(clip);
        }

        private void PlayShootSFX(AbstractBullet bullet)
        {
            var randomIndex = UnityEngine.Random.Range(0, bullet.StartFlySFX.Count);
            var clip = bullet.StartFlySFX[randomIndex];
            PlayRandomPitchSFX(clip, bullet.transform.position);
        }

        private void PlayRandomPitchSFX(AudioClip clip, Vector3 position)
        {
            var sfx = GetSFX();
            sfx.transform.position = position;
            var pitch = UnityEngine.Random.Range(
                _sFXSettings.PitchRandomInterval.x,
                _sFXSettings.PitchRandomInterval.y
                );
            sfx.Play(clip, pitch);
        }

        private SFX GetSFX()
        {
            var sfx = _wrapperPool.Get();
            _workList.Add(sfx);
            return sfx;
        }

        private void OnSpawnSFX(SFX sFX)
        {
            sFX.OnSFXEnd.Subscribe(ReturnSFXToPool).AddTo(_disposables);
        }

        private void ReturnSFXToPool(SFX sFX)
        {
            _workList.Remove(sFX);
            _wrapperPool.Release(sFX);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
