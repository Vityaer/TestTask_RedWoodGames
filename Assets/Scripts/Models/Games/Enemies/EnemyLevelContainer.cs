using Gameplay.Enemies.Abstractions;
using UnityEngine;

namespace Models.Games.Enemies
{
    public class EnemyLevelContainer
    {
        [field: SerializeField] public AbstractEnemy Prefab { get; private set; }
    }
}
