using Gameplay.Enemies.Abstractions;
using Gameplay.Services;
using System;
using UnityEngine;

namespace Gameplay.Common.WrapperPools
{
    public class EnemyWrapperPool<T> : WrapperPool<T>
           where T : AbstractEnemy
    {
        private readonly IEnemyFactory _enemyFactory;

        public EnemyWrapperPool(T prefab, IEnemyFactory enemyFactory, Action<T> actionOnCreate, Transform parent = null)
            : base(prefab, actionOnCreate, parent)
        {
            _enemyFactory = enemyFactory;
        }

        protected override T Create()
        {
            var result = _enemyFactory.CreateEnemy(Prefab);

            if (ActionOnCreate != null)
                ActionOnCreate(result);

            result.transform.SetParent(Parent);
            return result;
        }
    }
}
