using Models.Common;
using Models.Games;
using UnityEngine;

namespace Gameplay.Services.Common.Inputs
{
    public class InputService : IInputService
    {
        private readonly StorageService _storageService;

        public InputService(StorageService storageService)
        {
            _storageService = storageService;
        }

        public bool GetKey(InputActionType inputActionType)
        {
            return Input.GetKey(_storageService.InputSettings.InputBindings[inputActionType]);
        }

        public bool GetKey(KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }
    }
}
