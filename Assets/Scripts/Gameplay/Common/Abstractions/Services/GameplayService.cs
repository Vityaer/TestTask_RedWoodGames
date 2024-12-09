using Cysharp.Threading.Tasks;
using Gameplay.Common.Interfaces;
using Gameplay.Services;
using System;
using System.Threading;
using UniRx;
using Utils.Asyncs;
using Zenject;

namespace Gameplay.Common.Abstractions
{
    public abstract class GameplayService : IGameplayService, IDisposable
    {
        protected CancellationTokenSource CancellationTokenSource;
        protected CompositeDisposable Disposables = new();

        [Inject]
        public void Construct(ILevelManager levelManager)
        {
            levelManager.OnStartGame.Subscribe(_ => Start()).AddTo(Disposables);
            levelManager.OnGameOver.Subscribe(_ => Stop()).AddTo(Disposables);
        }

        public virtual void Start()
        {
            if(CancellationTokenSource != null)
                Stop();

            CancellationTokenSource = new();

            CustomUpdate(CancellationTokenSource.Token).Forget();

        }

        public virtual void Stop()
        {
            CancellationTokenSource.TryCancel();
        }

        private async UniTaskVoid CustomUpdate(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Yield(token);
                Tick();
            }
        }

        protected abstract void Tick();

        public void Dispose()
        {
            Disposables.Dispose();
            Stop();
        }
    }
}
