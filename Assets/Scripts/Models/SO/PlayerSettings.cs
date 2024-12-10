using UnityEngine;

namespace Models.Games
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ScriptableObjects/Game/Player/PlayerSettings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField] public float RunSpeed { get; private set; }
        [field: SerializeField] public float RechargeTime { get; private set; }
    }
}
