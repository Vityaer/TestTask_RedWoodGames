using Models.Games;
using System;
using UniRx;
using UnityEngine;

namespace Gameplay.Enemies.HealthSystems
{
    public class EnemyHealth : MonoBehaviour
    {
        private EnemySettings _enemySettings;

        private float _currentHealth;
        private ReactiveCommand<float> _onChangeHealth = new();

        public ReactiveCommand OnDeath = new();
        public IObservable<float> OnChangeHealth => _onChangeHealth;
        public float MaxHealth => _enemySettings.Health;

        public void Init(EnemySettings enemySettings)
        {
            _enemySettings = enemySettings;
            Refresh();
        }

        public void Refresh()
        {
            _currentHealth = _enemySettings.Health;
        }

        public void ApplyDamage(float damage)
        {
            if (_currentHealth <= 0)
            {
                UnityEngine.Debug.LogError("Try damage for dead enemy.");
                return;
            }

            _currentHealth -= damage;

            if (_currentHealth < 0)
                _currentHealth = 0;

            _onChangeHealth.Execute(_currentHealth);

            if(_currentHealth == 0)
                OnDeath.Execute();
        }
    }
}
