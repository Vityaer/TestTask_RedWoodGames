using Cinemachine;
using Gameplay.AudioSystems;
using Gameplay.Loots;
using Gameplay.Players;
using UnityEngine;

namespace Assets.Scripts.Models.Games
{
    [CreateAssetMenu(fileName = "GameplaySettings", menuName = "ScriptableObjects/Game/Common/GameplaySettings", order = 1)]
    public class GameplaySettings : ScriptableObject
    {
        [field: SerializeField] public Loot LootPrefab { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera CameraPrefab { get; private set; }
        [field: SerializeField] public AbstractBullet Bullet { get; private set; }

    }
}
