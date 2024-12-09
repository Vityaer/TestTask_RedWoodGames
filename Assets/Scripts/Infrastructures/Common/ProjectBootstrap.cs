using Infrastructures.Common;
using Models.Games;
using UnityEngine;
using Zenject;

namespace Infrastructures
{
    public class ProjectBootstrap : MonoInstaller
    {
        [SerializeField] private StorageService _storageService;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneService>().AsSingle();
            Container.Bind<StorageService>().FromInstance(_storageService).AsSingle();
        }
    }
}
