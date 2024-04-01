using System;
using System.Runtime.InteropServices;
using Launcher.Classes;
using Launcher.Handlers;
using Microsoft.Win32.SafeHandles;
using SinRpLauncher.Loaders;
using SinRpLauncher.Util;

namespace Launcher.Loaders
{
    /// <summary>
    /// Loader for <see cref="WindowSettings"/>
    /// </summary>
    public class SettingsWindowLoader : IWindowLoader
    {


        WindowSettings ws;

        public SettingsWindowLoader(WindowSettings ws)
        {
            this.ws = ws;
        }

        public void LoadWindow()
        {
            XamlUtil.SetCursorsToControls(ws);
            ws.GamePathTextBox.Text = GameFilesSystem.GameDir;
            ws.LanguagesComboxBox.SelectedIndex = SelectedLangIndex(Utils.GetIntefaceLanguage());

            string xmlcolpath = InfoClass.CurrentDir + '\\' + PathRoots.DataDirectory + '\\' + PathRoots.SettingsWndColorThemeDatFile;
            using (ThemesHandler _th = new ThemesHandler())
            {
                _th.LoadColorThemeToSettingsWindow(xmlcolpath, ws);
            }
        }


        private int SelectedLangIndex(Languages l)
        {
            int index = 0;  
            string s = LanguagesHandler.GetNameByLangEnum(l);
            switch(s)
            {
                case LanguagesNames.ENNAME: index = 0; break;
                case LanguagesNames.UANAME: index = 1; break;
                case LanguagesNames.RUNAME: index = 2; break;
            }

            return index;
        }
    }
}
