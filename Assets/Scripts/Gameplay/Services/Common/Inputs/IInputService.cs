using Models.Common;
using UnityEngine;

namespace Gameplay.Services.Common.Inputs
{
    public interface IInputService
    {
        bool GetKey(InputActionType inputActionType);
        bool GetKey(KeyCode leftArrow);
    }
}
