using Gameplay.Players.HealthSystems;
using System;
using UI.Scenes.Gameplay.GameOverPanels;
using UniRx;
using Zenject;

namespace Gameplay.Services
{
    public class LevelManager : ILevelManager
    {
        private ReactiveCommand _onGameOver = new();
        private ReactiveCommand _onRestartGame = new();

        public ReactiveCommand OnStartGame => _onRestartGame;
        public ReactiveCommand OnGameOver => _onGameOver;

        public void Restart()
        {
            _onRestartGame.Execute();
        }

        public void GameOver()
        {
            _onGameOver.Execute();
        }
    }
}
