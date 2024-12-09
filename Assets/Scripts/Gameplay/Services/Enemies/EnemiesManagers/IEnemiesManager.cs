using Gameplay.Enemies.Abstractions;
using System;

namespace Gameplay.Services
{
    public interface IEnemiesManager
    {
        void AddEnemy(AbstractEnemy enemy);
        IObservable<AbstractEnemy> OnSpawnEnemy { get; }
        IObservable<AbstractEnemy> OnDestroyEnemy { get; }
    }
}