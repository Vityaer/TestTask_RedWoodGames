using Gameplay.Players;
using Models.Games.Bullets;
using System;
using UniRx;

namespace Gameplay.Services.Bullets
{
    public interface IBulletService
    {
        int CurrentAmmo { get; }
        void AddAmmo(int count);
        void Shoot();
        IObservable<AbstractBullet> OnSpawnBullet { get; }
        IObservable<int> OnChangeAmmoCount { get; }
        IObservable<BulletCollision> OnBulletTargetCollision { get; }
        ReactiveCommand OnEmptyAmmoCount { get; }
    }
}
