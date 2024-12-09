using Assets.Scripts.UI.UiControllers;
using Cysharp.Threading.Tasks;
using Gameplay.Services.Bullets;
using System;
using UniRx;
using Zenject;

namespace UI.Scenes.Gameplay.AmmoPanels
{
    public class AmmoPanelController : UiController<AmmoPanelView>, IInitializable, IDisposable
    {
        private readonly IBulletService _bulletService;
        private readonly CompositeDisposable _disposables = new();

        public AmmoPanelController(IBulletService bulletService)
        {
            _bulletService = bulletService;
        }

        public void Initialize()
        {
            _bulletService.OnChangeAmmoCount.Subscribe(OnChangeAmmoCount).AddTo(_disposables);
            OnChangeAmmoCount(_bulletService.CurrentAmmo);
        }

        private void OnChangeAmmoCount(int count)
        {
            View.CurrentAmmoText.text = $"{count}";
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
