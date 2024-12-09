using System;
using UnityEngine;

namespace Gameplay.Common.Abstractions.Movements
{
    public class AbstractMovement : MonoBehaviour
    {
        [SerializeField] private Transform _body;
        [SerializeField] private bool _isFaceRight;
        [SerializeField] private Rigidbody2D _rigidbody;

        private Animator _animator;

        protected Rigidbody2D Rigidbody => _rigidbody;

        public Transform Body => _body;
        public bool IsFaceRight => _isFaceRight;

        public void SetData(Animator animator)
        {
            _animator = animator;
        }

        protected void CheckFlip(Vector2 direction)
        {
            if ((direction.x != 0) && ((direction.x > 0) ^ (IsFaceRight)))
            {
                Flip();
            }
        }

        protected void ChangeAnimationParametr(int hash, float value)
        {
            _animator.SetFloat(hash, value);
        }

        protected void Flip()
        {
            var localScale = _body.localScale;
            localScale.x *= -1f;
            _body.localScale = localScale;
            _isFaceRight = !_isFaceRight;
        }

        public void Refresh()
        {
            Rigidbody.velocity = Vector2.zero;
        }
    }
}
