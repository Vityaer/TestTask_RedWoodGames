using Gameplay.Common.Abstractions.Movements;
using Gameplay.Players;
using Infrastructures.Common;
using Models.Games;
using UnityEngine;

namespace Gameplay.Enemies.Movements
{
    public class EnemyMovement : AbstractMovement
    {
        [SerializeField] private bool _isFacingRight;

        private Player _player;
        private EnemySettings _settings;

        public void Init(Player player, EnemySettings settings)
        {
            _player = player;
            _settings = settings;
        }

        public void Move()
        {
            var direction = (_player.transform.position - Body.position).normalized;
            direction.y = 0;
            Rigidbody.velocity = direction * _settings.Speed;
            CheckFlip(direction);

            ChangeAnimationParametr(GameConstants.Visual.ANIMATOR_RUN_NAME_HASH,
                Mathf.Abs(direction.x));
        }
    }
}
