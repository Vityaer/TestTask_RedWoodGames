using Assets.Scripts.UI.UiControllers;
using Infrastructures.Common;
using Infrastructures.Common.SceneServices;
using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Scenes.MainMenu.MainPanels
{
    public class MainMenuPanelController : UiController<MainMenuPanelView>, IInitializable, IDisposable
    {
        private readonly ISceneService _sceneSwitcher;
        private readonly CompositeDisposable _disposables = new();

        public MainMenuPanelController(ISceneService sceneSwitcher)
        {
            _sceneSwitcher = sceneSwitcher;
        }

        public void Initialize()
        {
            View.StartGameButton.OnClickAsObservable().Subscribe(_ => StartGame()).AddTo(_disposables);
            View.ExitGameButton.OnClickAsObservable().Subscribe(_ => ExitGame()).AddTo(_disposables);
        }

        private void StartGame()
        {
            _sceneSwitcher.LoadScene(GameConstants.Scenes.GAMEPLAY_SCENE_NAME);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
