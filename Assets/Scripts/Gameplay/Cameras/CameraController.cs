using Gameplay.Players;
using Models.Games;
using Zenject;

namespace Gameplay.Cameras
{
    public class CameraController : IInitializable
    {
        private readonly Player _player;
        private readonly StorageService _storageService;

        public CameraController(StorageService storageService, Player player)
        {
            _player = player;
            _storageService = storageService;
        }

        public void Initialize()
        {
            var virtualCamera = UnityEngine.Object.Instantiate(_storageService.GameplaySettings.CameraPrefab);
            virtualCamera.Follow = _player.transform;
        }
    }
}
