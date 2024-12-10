using Models.Games.Enemies;
using Models.Games.Levels;
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
        [SerializeField] private LevelContainer _levelContainer;
        [SerializeField] private List<EnemyLevelContainer> _enemyContainers;

        public LevelContainer LevelContainer => _levelContainer;
        public List<EnemyLevelContainer> EnemyContainers => _enemyContainers;
        public int StartAmmo => _startAmmo;
        public Vector2 EnemyCreateRandomTime => _enemyCreateRandomTime;
    }
}
