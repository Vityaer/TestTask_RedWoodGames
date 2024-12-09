using Gameplay.Enemies.Abstractions;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class EnemyFactory : IEnemyFactory, IInitializable
    {
        private Transform _enemyContainer;

        public void Initialize()
        {
            _enemyContainer = new GameObject("enemiesContainer").transform;
        }

        public T CreateEnemy<T>(T prefab)
            where T : AbstractEnemy
        {
            var result = UnityEngine.Object.Instantiate(prefab, _enemyContainer);
            return result;
        }
    }
}
