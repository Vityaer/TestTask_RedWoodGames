using Gameplay.AudioSystems;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Models.SO
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "ScriptableObjects/Game/Audio/AudioSettings", order = 2)]
    public class SFXSettings : ScriptableObject
    {
        [field: SerializeField] public SFX SFXPrefab { get; private set; }
        [field: SerializeField] public AudioClip GameOverSFX { get; private set; }

        [SerializeField][MinMaxSlider(0.1f, 3f, true)] private Vector2 _pitchRandomInterval;

        public Vector2 PitchRandomInterval => _pitchRandomInterval;
    }
}
