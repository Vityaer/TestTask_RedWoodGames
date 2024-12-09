using Gameplay.Players;
using System;
using UniRx;

namespace Gameplay.Services.Collisions
{
    public interface ICollsionService
    {
        IObservable<AbstractBullet> OnBulletCollion { get; }
        ReactiveCommand OnEnemyCollisionPlayer { get; }
    }
}