using UnityEngine;

namespace Models.Games
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "ScriptableObjects/Game/Enemies/EnemySettings", order = 1)]
    public class EnemySettings : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _speed;
        [SerializeField] private int _ammoBonus;

        public float Health => _health;
        public float Speed => _speed;
        public int AmmoBonus => _ammoBonus;
    }
}
