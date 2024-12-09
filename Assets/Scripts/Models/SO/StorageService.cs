using Assets.Scripts.Models.Games;
using Models.Common;
using Models.SO;
using UnityEngine;

namespace Models.Games
{
    [CreateAssetMenu(fileName = "StorageService", menuName = "ScriptableObjects/Game/StorageService", order = 1)]
    public class StorageService : ScriptableObject
    {
        [field: SerializeField] public SFXSettings SFXSettings { get; private set; }
        [field: SerializeField] public GameplaySettings GameplaySettings { get; private set; }
        [field: SerializeField] public LevelSettings LevelSettings { get; private set; }
        [field: SerializeField] public PlayerSettings PlayerSettings { get; private set; }
        [field: SerializeField] public InputSettings InputSettings { get; private set; }


    }
}
