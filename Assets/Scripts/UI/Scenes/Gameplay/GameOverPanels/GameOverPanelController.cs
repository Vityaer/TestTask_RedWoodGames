using Assets.Scripts.UI.UiControllers;
using Gameplay.Services;
using Infrastructures.Common;
using Infrastructures.Common.SceneServices;
using System;
using UniRx;
using Zenject;

namespace UI.Scenes.Gameplay.GameOverPanels
{
    public class GameOverPanelController : UiController<GameOverPanelView>, IInitializable, IDisposable
    {
        private readonly ILevelManager _levelManager;
        private readonly ISceneService _sceneSwitcher;
        private readonly CompositeDisposable _disposables = new();

        public GameOverPanelController(ISceneService sceneSwitcher, ILevelManager levelManager)
        {
            _sceneSwitcher = sceneSwitcher;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            _levelManager.OnGameOver.Subscribe(_ => Show()).AddTo(_disposables);
            View.RestartGameButton.OnClickAsObservable().Subscribe(_ => RestartGame()).AddTo(_disposables);
            View.GoToMainMenuButton.OnClickAsObservable().Subscribe(_ => GoToMainMenu()).AddTo(_disposables);
        }

        private void RestartGame()
        {
            _levelManager.Restart();
            Hide();
        }

        private void GoToMainMenu()
        {
            _sceneSwitcher.LoadScene(GameConstants.Scenes.MAIN_MENU_SCENE_NAME);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
