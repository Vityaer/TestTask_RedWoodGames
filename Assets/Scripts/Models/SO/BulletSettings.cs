using Gameplay.Players;
using UnityEngine;

namespace Models.Games.Bullets
{
    [CreateAssetMenu(fileName = "BulletSettings", menuName = "ScriptableObjects/Game/Player/BulletSettings", order = 2)]
    public class BulletSettings : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float FlySpeed { get; private set; }
    }
}
