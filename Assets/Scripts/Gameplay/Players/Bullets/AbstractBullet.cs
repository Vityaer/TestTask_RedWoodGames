using Infrastructures.Common;
using Models.Games.Bullets;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Utils;

namespace Gameplay.Players
{
    public abstract class AbstractBullet : MonoBehaviour
    {
        [SerializeField] protected Transform Transform;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private BulletSettings _bulletSettings;

        [Header("SFX")]
        [SerializeField] private List<AudioClip> _startFlySFX;
        [SerializeField] private List<AudioClip> _collionSFX;

        private ReactiveCommand<GameObject> _onObjectCollion = new();
        private bool _work = false;

        public IObservable<GameObject> OnObjectCollion => _onObjectCollion;
        public BulletSettings BulletSettings => _bulletSettings;
        public List<AudioClip> StartFlySFX => _startFlySFX;
        public List<AudioClip> CollionSFX => _collionSFX;

        public virtual void SetData(Vector3 startPosition, Vector2 direction)
        {
            _work = true;
            Transform.position = startPosition;
            _rigidbody.velocity = direction * _bulletSettings.FlySpeed;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_work)
                return;

            if(LayerUtils.CheckIntersection(collision.gameObject, GameConstants.Fight.BulletTargetMask))
                    _onObjectCollion.Execute(collision.gameObject);
        }

        public void Stop()
        {
            _work = false;
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
