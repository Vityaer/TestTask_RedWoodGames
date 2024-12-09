using CustomComponents;
using System;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.UI.UiControllers
{
    public class UiController<T> : IUiController where T : UiView
    {
        [Inject] protected readonly T View;
        public bool IsAutoShow => View.AutoShow;

        public void Hide()
        {
            View.Hide();
        }

        public void Show()
        {
            View.Show();
        }

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            var injectableComponents = View.GetComponentsInChildren<IInjectable>();
            foreach (var component in injectableComponents)
                diContainer.Inject(component);
        }
    }
}
