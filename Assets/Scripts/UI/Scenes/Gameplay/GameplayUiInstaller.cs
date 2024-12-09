using UI.Extentions;
using UI.Scenes.Gameplay.AmmoPanels;
using UI.Scenes.Gameplay.EnemyHealthPanels;
using UI.Scenes.Gameplay.GameOverPanels;
using UI.Scenes.MainMenu;
using UnityEngine;
using Zenject;

namespace UI.Scenes.Gameplay
{
    public class GameplayUiInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private AmmoPanelView _ammoPanelView;
        [SerializeField] private GameOverPanelView _gameOverPanelView;
        [SerializeField] private EnemyHealthPanelView _enemyHealthPanelView;
        
        public override void InstallBindings()
        {
            var canvas = Object.Instantiate(_canvas);
            Container.BindUiView<AmmoPanelController, AmmoPanelView>(_ammoPanelView, canvas.transform);
            Container.BindUiView<EnemyHealthPanelController, EnemyHealthPanelView>(_enemyHealthPanelView, canvas.transform);
            Container.BindUiView<GameOverPanelController, GameOverPanelView>(_gameOverPanelView, canvas.transform);
            BindWindows();
        }

        private void BindWindows()
        {
            Container.BindInterfacesAndSelfTo<GameplayWindow>().AsSingle();
        }
    }
}
