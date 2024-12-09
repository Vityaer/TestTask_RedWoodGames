using Gameplay.Common.Abstractions.Movements;
using Infrastructures.Common;
using System;
using UnityEngine;

namespace Gameplay.Players.Movements
{
    public class PlayerMovement : AbstractMovement
    {
        private float _speed;

        public void Init(float speed)
        {
            _speed = speed;
        }

        public void Move(Vector2 direction)
        {
            ChangeAnimationParametr(GameConstants.Visual.ANIMATOR_RUN_NAME_HASH,
                Mathf.Abs(direction.x));

            Rigidbody.velocity = direction * _speed;
            CheckFlip(direction);
        }
    }
}
