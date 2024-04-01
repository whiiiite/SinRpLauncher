using System;
using System.Windows.Input;
using Launcher.BaseClasses;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using SinRpLauncher.Handlers.HotKeysHandlers;

namespace Launcher.Handlers.HotKeysHandlers
{
    public class WindowProfilesHotKeysHandler : BaseHandler, IHotKeysHandler
    {

        WindowProfiles wp;

        public WindowProfilesHotKeysHandler(WindowProfiles wp)
        {
            this.wp = wp;
        }


        public void HandleHotKeys(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                wp.Close();
            }
        }
    }
}
