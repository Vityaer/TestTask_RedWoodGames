namespace Assets.Scripts.UI.UiControllers
{
    public interface IUiController
    {
        bool IsAutoShow { get; }

        void Hide();
        void Show();
    }
}