using Cysharp.Threading.Tasks;
using Gameplay.Common.Abstractions;
using Gameplay.Services;
using Gameplay.Services.Bullets;
using Gameplay.Services.Common.Inputs;
using Infrastructures.Common;
using Models.Common;
using Models.Games;
using System.Threading;
using UniRx;
using UnityEngine;

namespace Gameplay.Players.AttackSystems
{
    public class PlayerAttackSystem : GameplayService
    {
        private readonly IInputService _inputService;
        private readonly StorageService _storageService;
        private readonly IBulletService _bulletService;
        private readonly Player _player;

        private bool _canAttack = true;

        public PlayerAttackSystem(
            IInputService inputService,
            StorageService storageService,
            IBulletService bulletService,
            Player player
            )
        {
            _inputService = inputService;
            _storageService = storageService;
            _bulletService = bulletService;
            _player = player;
        }

        public override void Start()
        {
            _canAttack = true;
            base.Start();
        }

        protected override void Tick()
        {
            if (!_canAttack)
                return;

            if (_inputService.GetKey(InputActionType.Shoot))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            _player.StartAnimation(GameConstants.Visual.ANIMATOR_SHOOT_NAME_HASH);
            _bulletService.Shoot();
            _canAttack = false;

            if(CancellationTokenSource != null && !CancellationTokenSource.IsCancellationRequested)
                WaitTime(CancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid WaitTime(CancellationToken token)
        {
            await UniTask.Delay(
                Mathf.RoundToInt(_storageService.PlayerSettings.RechargeTime * 1000),
                cancellationToken: token
                );
            _canAttack = true;
        }
    }
}
