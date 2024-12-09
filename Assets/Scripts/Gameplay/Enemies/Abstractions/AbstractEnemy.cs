using Gameplay.Enemies.HealthSystems;
using Gameplay.Enemies.Movements;
using Gameplay.Players;
using Infrastructures.Common;
using Models.Games;
using UniRx;
using UnityEngine;
using Utils;

namespace Gameplay.Enemies.Abstractions
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [field: SerializeField] public EnemySettings Settings { get; private set; }
        [field: SerializeField] public EnemyMovement EnemyMovement { get; private set; }
        [field: SerializeField] public EnemyHealth Health { get; private set; }
        [field: SerializeField] public AudioClip DeathSFX { get; private set; }

        public ReactiveCommand OnPlayerColiision = new();

        public void Init(Player player)
        {
            EnemyMovement.Init(player, Settings);
            EnemyMovement.SetData(_animator);
            Health.Init(Settings);
        }

        public void Refresh()
        {
            Health.Refresh();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerUtils.CheckIntersection(collision.gameObject, GameConstants.Fight.PlayerLayer))
                OnPlayerColiision.Execute();
        }
    }
}
