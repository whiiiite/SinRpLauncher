using System.Windows.Input;

namespace SinRpLauncher.Handlers.HotKeysHandlers
{
    public interface IHotKeysHandler
    {
        void HandleHotKeys(KeyEventArgs e) { }
    }
}
