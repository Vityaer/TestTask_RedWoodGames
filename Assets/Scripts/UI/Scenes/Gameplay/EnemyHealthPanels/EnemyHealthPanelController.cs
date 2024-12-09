using Assets.Scripts.UI.UiControllers;
using Cysharp.Threading.Tasks;
using Gameplay.Common.WrapperPools;
using Gameplay.Enemies.Abstractions;
using Gameplay.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using UniRx;
using Utils.Asyncs;
using Zenject;

namespace UI.Scenes.Gameplay.EnemyHealthPanels
{
    public class EnemyHealthPanelController : UiController<EnemyHealthPanelView>, IInitializable, IDisposable
    {
        private readonly IEnemiesManager _enemiesManager;
        private readonly ILevelManager _levelManager;

        private readonly CompositeDisposable _disposables = new();
        private readonly Dictionary<AbstractEnemy, EnemyHealthView> _workSliders = new();
        private readonly CancellationTokenSource _cancellationTokenSource = new();

        private List<EnemyHealthView> _addedHelthView = new();
        private List<EnemyHealthView> _removedHelthView = new();
        private WrapperPool<EnemyHealthView> _enemyHealthPool;

        public EnemyHealthPanelController(IEnemiesManager enemiesManager, ILevelManager levelManager)
        {
            _enemiesManager = enemiesManager;
            _levelManager = levelManager;
        }

        public void Initialize()
        {
            _levelManager.OnGameOver.Subscribe(_ => ClearAll()).AddTo(_disposables);
            _enemiesManager.OnSpawnEnemy.Subscribe(OnSpawnEnemy).AddTo(_disposables);
            _enemiesManager.OnDestroyEnemy.Subscribe(OnDestroyEnemy).AddTo(_disposables);
            _enemyHealthPool = new(View.EnemyHealthViewPrefab, OnCreateHealthView, View.HealthsContainer);

            CustomUpdate(_cancellationTokenSource.Token).Forget();
        }

        private void OnDestroyEnemy(AbstractEnemy enemy)
        {
            _removedHelthView.Add(_workSliders[enemy]);
        }

        private async UniTaskVoid CustomUpdate(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Yield(token);

                foreach (var healthSlider in _addedHelthView)
                    _workSliders.Add(healthSlider.TargetEnemy, healthSlider);
                _addedHelthView.Clear();

                foreach (var removedSlider in _removedHelthView)
                {
                    _workSliders.Remove(removedSlider.TargetEnemy);
                    _enemyHealthPool.Release(removedSlider);
                }
                _removedHelthView.Clear();

                foreach (var slider in _workSliders)
                {
                    slider.Value.Move();
                }
            }
        }

        private void OnSpawnEnemy(AbstractEnemy enemy)
        {
            var healthSlider = _enemyHealthPool.Get();
            healthSlider.SetData(enemy);
            _addedHelthView.Add(healthSlider);
        }

        private void OnCreateHealthView(EnemyHealthView view)
        {
            view.Init(View.HealthsContainer, View.Offset);
        }

        private void ClearAll()
        {
            foreach (var slider in _workSliders)
            {
                _removedHelthView.Add(slider.Value);
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.TryCancel();
            _disposables.Dispose();
        }
    }
}
