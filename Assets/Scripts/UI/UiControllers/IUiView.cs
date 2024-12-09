namespace UI.UiControllers
{
    public interface IUiView
    {
        bool AutoShow { get; }
        void Show();
        void Hide();
    }
}
