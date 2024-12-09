using System.Diagnostics;
using Zenject;

namespace UI.Scenes.MainMenu
{
    public class MainMenuEntryPoint : IInitializable
    {
        private readonly MainMenuWindow _mainMenuWindow;

        public MainMenuEntryPoint(MainMenuWindow mainMenuWindow)
        {
            _mainMenuWindow = mainMenuWindow;
        }

        public void Initialize()
        {
            UnityEngine.Debug.Log("MainMenuEntryPoint Initialize");
            _mainMenuWindow.Open();
        }
    }
}
