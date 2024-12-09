using Gameplay.Common.Abstractions;
using Gameplay.Services.Common.Inputs;
using Models.Common;
using UnityEngine;

namespace Gameplay.Players.Movements
{
    public class PlayerInputMovementService : GameplayService
    {
        private readonly Player _player;
        private readonly IInputService _inputService;

        private Vector2 _directionMove;

        public PlayerInputMovementService(IInputService inputService, Player player)
        {
            _inputService = inputService;
            _player = player;
        }

        protected override void Tick()
        {
            _directionMove = Vector2.zero;

            if (!_inputService.GetKey(InputActionType.Shoot))
            {
                if (_inputService.GetKey(InputActionType.MoveLeft) || _inputService.GetKey(KeyCode.LeftArrow))
                {
                    _directionMove.x = -1f;
                }
                else if (_inputService.GetKey(InputActionType.MoveRight) || _inputService.GetKey(KeyCode.RightArrow))
                {
                    _directionMove.x = 1f;
                }
            }

            _player.Movement.Move(_directionMove);
        }
    }
}
