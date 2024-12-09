using Gameplay.Services;
using Models.Games;
using Zenject;

namespace UI.Scenes.Gameplay
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly GameplayWindow _gameplayWindow;
        private readonly StorageService _storageService;
        private readonly ILevelManager _levelManager;

        public GameplayEntryPoint(StorageService storageService, GameplayWindow gameplayWindow, ILevelManager levelManager)
        {
            _storageService = storageService;
            _gameplayWindow = gameplayWindow;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            CreateLevel();
            _levelManager.Restart();
        }

        private void CreateLevel()
        {
            UnityEngine.Object.Instantiate(_storageService.LevelSettings.LevelContainer);
        }
    }
}
