using UI.Scenes.MainMenu.MainPanels;
using UI.Windows;

namespace UI.Scenes.MainMenu
{
    public class MainMenuWindow : AbstactWindow
    {
        private readonly MainMenuPanelController _mainMenuPanelController;

        public MainMenuWindow(MainMenuPanelController mainMenuPanelController)
        {
            _mainMenuPanelController = mainMenuPanelController;
        }

        public override void Initialize()
        {
            UnityEngine.Debug.Log("MainMenuWindow Initialize");
            AddController(_mainMenuPanelController);
        }
    }
}
