using System;
using Launcher.BaseClasses;
using System.Windows.Input;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using SinRpLauncher.Handlers.HotKeysHandlers;

namespace Launcher.Handlers.HotKeysHandlers
{
    public class SettingsWindowHotKeysHandler : BaseHandler, IHotKeysHandler
    {
        WindowSettings sw;


        public SettingsWindowHotKeysHandler(WindowSettings sw)
        {
            this.sw = sw;
        }


        public void HandleHotKeys(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                sw.Close();
            }
            if (e.Key == Key.Enter)
            {
                sw.DialogResult = true;
            }
        }
    }
}
