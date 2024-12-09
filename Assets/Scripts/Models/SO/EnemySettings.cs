using UnityEngine;

namespace Models.Games
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/Game/Enemies/EnemySettings", order = 1)]
    public class EnemySettings : ScriptableObject
    {
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int AmmoBonus { get; private set; }
    }
}
