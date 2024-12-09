using Gameplay.Players.Movements;
using Gameplay.Services.Common.Inputs;
using Models.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEditor.Tilemaps;
using UnityEngine;
using Zenject;

namespace Gameplay.Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private Animator _animator;

        [field: SerializeField] public PlayerMovement Movement { get; private set; }
        [field: SerializeField] public Transform BulletStartPoint { get; private set; }

        private void Awake()
        {
            Movement.Init(_playerSettings.RunSpeed);
            Movement.SetData(_animator);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            Movement.Refresh();
        }

        public void StartAnimation(int hash)
        {
            _animator.Play(hash);
        }
    }
}
