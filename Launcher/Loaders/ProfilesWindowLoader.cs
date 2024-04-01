using System;
using System.IO;
using System.Runtime.InteropServices;
using Launcher.Classes;
using Launcher.Handlers;
using Microsoft.Win32.SafeHandles;
using SinRpLauncher.Loaders;
using SinRpLauncher.Util;

namespace Launcher.Loaders
{
    public class ProfilesWindowLoader : IWindowLoader
    {
        WindowProfiles wp;

        public ProfilesWindowLoader(WindowProfiles wp)
        {
            this.wp = wp;
        }


        public void LoadWindow()
        {
            // set all profiles data and even empty
            wp.SetProfilesList(Directory.GetCurrentDirectory() + '\\' + PathRoots.DataDirectory + '\\' + PathRoots.ProfilesFile);
            XamlUtil.SetCursorsToControls(wp);

            string cPath = Directory.GetCurrentDirectory() + '\\' + PathRoots.DataDirectory + '\\' + PathRoots.ProfilesWndColorThemeDatFile;
            using(ThemesHandler thnd = new ThemesHandler())
            {
                thnd.LoadColorThemeToProfilesWindow(cPath, wp);
            }
        }
    }
}
