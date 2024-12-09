using Gameplay.Enemies.Abstractions;

namespace Gameplay.Services
{
    public interface IEnemyFactory
    {
        T CreateEnemy<T>(T prefab)
            where T : AbstractEnemy;
    }
}