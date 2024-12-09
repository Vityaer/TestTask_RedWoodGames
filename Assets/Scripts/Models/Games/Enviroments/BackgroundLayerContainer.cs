using System;
using UnityEngine;

namespace Gameplay.Enviroments
{
    [Serializable]
    public class BackgroundLayerContainer
    {
        [field: SerializeField] public Transform Layer { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float CameraDelta { get; private set; }
    }
}
