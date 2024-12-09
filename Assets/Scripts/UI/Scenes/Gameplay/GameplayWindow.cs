using UI.Scenes.Gameplay.AmmoPanels;
using UI.Scenes.Gameplay.EnemyHealthPanels;
using UI.Windows;

namespace UI.Scenes.Gameplay
{
    public class GameplayWindow : AbstactWindow
    {
        private readonly AmmoPanelController _ammoPanelController;
        private readonly EnemyHealthPanelController _enemyHealthPanelController;

        public GameplayWindow(AmmoPanelController ammoPanelController, EnemyHealthPanelController enemyHealthPanelController)
        {
            _ammoPanelController = ammoPanelController;
            _enemyHealthPanelController = enemyHealthPanelController;
        }

        public override void Initialize()
        {
            AddController(_ammoPanelController);
            AddController(_enemyHealthPanelController);
            Open();
        }
    }
}
