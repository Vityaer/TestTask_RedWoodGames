using Models.Games.Enemies;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Models.Games
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/Game/LevelSettings", order = 2)]
    public class LevelSettings : SerializedScriptableObject
    {
        [SerializeField][Min(1)] private int _startAmmo;
        [SerializeField][MinMaxSlider(0.001f, 30f, true)] private Vector2 _enemyCreateRandomTime;

        [field: SerializeField] public LevelContainer LevelContainer { get; private set; }
        [field: SerializeField] public List<EnemyLevelContainer> EnemyContainers { get; private set; }


        public int StartAmmo => _startAmmo;
        public Vector2 EnemyCreateRandomTime => _enemyCreateRandomTime;
    }
}
