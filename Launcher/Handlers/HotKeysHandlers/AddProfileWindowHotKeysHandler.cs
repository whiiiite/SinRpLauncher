using System;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Launcher.BaseClasses;
using Microsoft.Win32.SafeHandles;
using SinRpLauncher.Handlers.HotKeysHandlers;

namespace Launcher.Handlers.HotKeysHandlers
{
    public class AddProfileWindowHotKeysHandler : BaseHandler, IHotKeysHandler
    {
        AddProfileWindow apw;

        public AddProfileWindowHotKeysHandler(AddProfileWindow apw)
        {
            this.apw = apw;
        }

        public void HandleHotKeys(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                apw.Close();
            }
            if(e.Key == Key.Enter)
            {
                apw.DialogResult = true;
            }
        }
    }
}
