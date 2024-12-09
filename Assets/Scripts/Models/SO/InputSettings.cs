using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Models.Common
{
    [CreateAssetMenu(fileName = "InputSettings", menuName = "ScriptableObjects/Input/InputSettings", order = 1)]
    public class InputSettings : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<InputActionType, KeyCode> InputBindings { get; private set; }
    }
}
