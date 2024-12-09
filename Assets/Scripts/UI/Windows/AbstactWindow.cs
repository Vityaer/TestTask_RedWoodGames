using Assets.Scripts.UI.UiControllers;
using System.Collections.Generic;
using Zenject;

namespace UI.Windows
{
    public abstract class AbstactWindow : IInitializable
    {
        private List<IUiController> _controllers = new();

        public abstract void Initialize();

        public void Open()
        {
            foreach (var controller in _controllers)
            {
                if (controller.IsAutoShow)
                    controller.Show();
            }
        }

        public void Close()
        {
            foreach (var controller in _controllers)
            {
                controller.Hide();
            }
        }

        protected void AddController<T>(T controller)
            where T : IUiController
        {
            _controllers.Add(controller);
        }
    }
}
