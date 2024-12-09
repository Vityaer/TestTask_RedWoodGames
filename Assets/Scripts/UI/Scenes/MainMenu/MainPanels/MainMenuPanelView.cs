using Assets.Scripts.UI.UiControllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scenes.MainMenu.MainPanels
{
    public class MainMenuPanelView : UiView
    {
        [field: SerializeField] public Button StartGameButton { get; private set; }
        [field: SerializeField] public Button ExitGameButton { get; private set; }
    }
}
