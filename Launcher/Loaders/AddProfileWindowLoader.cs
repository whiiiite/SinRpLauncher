using System;
using System.Runtime.InteropServices;
using Launcher.Classes;
using Microsoft.Win32.SafeHandles;
using SinRpLauncher.Loaders;
using SinRpLauncher.Util;

namespace Launcher.Loaders
{
    public class AddProfileWindowLoader : IWindowLoader
    {
        AddProfileWindow apw;

        public AddProfileWindowLoader(AddProfileWindow apw)
        {
            this.apw = apw;
        }

        public void LoadWindow()
        {
            apw.ProfileServersBox.SelectedIndex = 0;
            XamlUtil.SetCursorsToControls(apw);
        }
    }
}
