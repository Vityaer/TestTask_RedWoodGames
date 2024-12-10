using System.Collections.Generic;
using UnityEngine;

namespace Models.Games.Levels
{
    public class LevelContainer : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerStartPoint { get; private set; }
        [field: SerializeField] public List<Transform> EnemyStartPoints { get; private set; }
    }
}
