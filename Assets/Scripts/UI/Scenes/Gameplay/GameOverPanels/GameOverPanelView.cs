using Assets.Scripts.UI.UiControllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scenes.Gameplay.GameOverPanels
{
    public class GameOverPanelView : UiView
    {
        [field: SerializeField] public Button RestartGameButton { get; private set; }
        [field: SerializeField] public Button GoToMainMenuButton { get; private set; }
    }
}
