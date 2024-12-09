using Sirenix.OdinInspector;
using UI.UiControllers;
using UnityEngine;

namespace Assets.Scripts.UI.UiControllers
{
    public class UiView : SerializedMonoBehaviour, IUiView
    {
        [SerializeField] private bool _autoShow;

        public bool AutoShow => _autoShow;

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
