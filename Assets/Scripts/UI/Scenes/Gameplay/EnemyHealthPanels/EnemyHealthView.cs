using DG.Tweening;
using Gameplay.Enemies.Abstractions;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Scenes.Gameplay.EnemyHealthPanels
{
    public class EnemyHealthView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _animationSpeed;
        [SerializeField] private RectTransform _rectTransform;

        private IDisposable _healthDisposable;
        private AbstractEnemy _targetEnemy;
        private Tween _healthTween;
        private RectTransform _container;
        private Vector2 _offset;

        public AbstractEnemy TargetEnemy => _targetEnemy;

        public void Init(RectTransform container, Vector2 offset)
        {
            _container = container;
            _offset = offset;
        }

        public void SetData(AbstractEnemy abstractEnemy)
        {
            _targetEnemy = abstractEnemy;
            _healthTween.Kill();
            _slider.value = 1f;

            _healthDisposable?.Dispose();
            _healthDisposable = abstractEnemy.Health.OnChangeHealth.Subscribe(ChangeHealthVisual);
        }
        public void Move()
        {
            var screenPoint = Camera.main.WorldToScreenPoint(_targetEnemy.transform.position);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _container,
                screenPoint,
                null,
                out var result
                );
            _rectTransform.anchoredPosition = result + _offset;
        }

        private void ChangeHealthVisual(float obj)
        {
            _healthTween.Kill();
            var targetValue = obj / _targetEnemy.Settings.Health;
            _healthTween = _slider.DOValue(targetValue, _animationSpeed)
                .SetSpeedBased(true);
        }

        private void OnDestroy()
        {
            _healthTween.Kill();
            _healthDisposable?.Dispose();
        }
    }
}
