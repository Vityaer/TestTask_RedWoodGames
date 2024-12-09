using UI.Extentions;
using UI.Scenes.MainMenu.MainPanels;
using UnityEngine;
using Zenject;

namespace UI.Scenes.MainMenu
{
    public class MainMenuUiInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private MainMenuPanelView _mainMenuPanelView;

        public override void InstallBindings()
        {
            var canvas = Instantiate(_canvas);
            Container.BindUiView<MainMenuPanelController, MainMenuPanelView>(_mainMenuPanelView, canvas.transform);
            BindWindows();

            Container.BindInterfacesTo<MainMenuEntryPoint>().AsSingle();
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<MainMenuWindow>().AsSingle();
        }
    }
}
