using Gameplay.AudioSystems;
using Gameplay.Cameras;
using Gameplay.Players;
using Gameplay.Players.AttackSystems;
using Gameplay.Players.Common;
using Gameplay.Players.HealthSystems;
using Gameplay.Players.Movements;
using Gameplay.Services;
using Gameplay.Services.Collisions;
using Gameplay.Services.Common.Inputs;
using Gameplay.Services.Loots;
using UI.Scenes.Gameplay;
using UnityEngine;
using Zenject;

namespace Infrastructures.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Player _player;

        public override void InstallBindings()
        {
            _player = Instantiate(_player);
            Container.Bind<Player>().FromInstance(_player).AsSingle();

            Container.BindInterfacesTo<LevelManager>().AsSingle();
            Container.BindInterfacesTo<CameraController>().AsSingle();
            
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.BindInterfacesTo<PlayerController>().AsSingle();
            Container.BindInterfacesTo<PlayerHealth>().AsSingle();
            Container.BindInterfacesTo<PlayerInputMovementService>().AsSingle();
            Container.BindInterfacesTo<PlayerAttackSystem>().AsSingle();
            Container.BindInterfacesTo<BulletService>().AsSingle();

            Container.BindInterfacesTo<WaveService>().AsSingle();
            Container.BindInterfacesTo<EnemiesManager>().AsSingle();
            Container.BindInterfacesTo<CollisionService>().AsSingle();
            Container.BindInterfacesTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesTo<LootService>().AsSingle();
            
            Container.BindInterfacesTo<GameplayEntryPoint>().AsSingle();
            Container.BindInterfacesTo<AudioSystemController>().AsSingle();
            
        }
    }
}
