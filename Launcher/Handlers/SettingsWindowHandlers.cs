using System;
using System.Collections.Generic;
using Launcher.BaseClasses;
using Launcher.Classes;
using System.Windows.Controls;
using System.Text.Json;
using System.IO;
using SinRpLauncher.Settings;
using SinRpLauncher.Util;

namespace Launcher.Handlers
{
    internal class SettingsWindowHandlers : BaseHandler
    {

        private WindowSettings SettingsWindow;

        public SettingsWindowHandlers() 
        {
        }

        public SettingsWindowHandlers(WindowSettings sw)
        {
            SettingsWindow = sw;
        }

        public void CloseButtonHnd(WindowSettings ws)
        {
            ws.Close();
        }


        public void ApplySettingsButtonHnd(WindowSettings ws)
        {
            ws.DialogResult = true;
        }


        public void CancelSettingsButtonHnd(WindowSettings ws)
        {
            ws.DialogResult = false;
        }


        public void Button_MouseEnterHnd(object sender, ThemesHandler _th)
        {
            _th.ButtonMouseEnter((Button)sender);
        }


        public void Button_MouseLeaveHnd(object sender, ThemesHandler _th)
        {
            _th.ButtonMouseLeave((Button)sender);
        }


        public void FileSourceButton_ClickHnd(WindowSettings ws)
        {
            string pathToJson = GameFilesSystem.GameDir;
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowserDialog.InitialDirectory = ws.GamePathTextBox.Text;

            System.Windows.Forms.DialogResult resDlg = folderBrowserDialog.ShowDialog();

            if(resDlg == System.Windows.Forms.DialogResult.OK)
            {
                ws.GamePathTextBox.Text = folderBrowserDialog.SelectedPath;

                string selectedPath = folderBrowserDialog.SelectedPath;

                WriteGamePathToJson(selectedPath, pathToJson);
                GameFilesSystem.GameDir = selectedPath;

                MTAServer mtaServer = new MTAServer();
                mtaServer.SetRegistersForServer(); // sets registers to HKEY LOCAL MACHINE

                folderBrowserDialog.Dispose(); // need for unfreeze thread
            }
        }


        private void WriteGamePathToJson(string pathToGame, string pathToJson)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("game_dir", pathToGame);

            string serData = JsonSerializer.Serialize(data);

            if(serData != null)
            {
                File.WriteAllText(pathToJson, serData);
            }
        }


        public void ChangeColorControlMenuItem_ClickHnd(object sender, WindowSettings sw)
        {
            if (sender is not MenuItem)
                return;

            string cPath = Path.Combine(InfoClass.CurrentDir,
                           PathRoots.DataDirectory, PathRoots.SettingsWndColorThemeDatFile);

            // we need got menuitem
            MenuItem menuItem = (MenuItem)sender;
            using (ThemesHandler tHnd = new ThemesHandler())
            {
                tHnd.PickControlColors(menuItem); // pick color for control
                tHnd.SetNewSettingsWindowColorsThemeToXML(cPath, sw); // set new data to xml
            }
        }


        public void LanguagesComboxBox_SelectionChangedHnd(ComboBox cb)
        {
            if (cb.SelectedItem == null) return;

            //string langStr = ((ComboBoxItem)cb.SelectedItem).Content.ToString() ?? LanguagesHandler.RUALIES;
            string langName = cb.SelectedValue.ToString() ?? LanguagesAliases.RUALIES;
            langName = LanguagesHandler.GetLanguageNameByAlies(langName); // get code name from full name like English -> en

            Languages lang = LanguagesHandler.GetLangByName(langName); // get language object from string
            LanguagesHandler.ChangeInterfaceLanguage(lang); // change in info class

            // write to launch settings
            string settsPath = Path.Combine(InfoClass.CurrentDir, PathRoots.DataDirectory, PathRoots.LaunchSettingsFile);
            Utils.WriteLaunchSettings(settsPath, lang);
        }


        public void SetDefaultThemesButton_ClickHnd(MainWindow mw)
        {
            if (mw == null) return;
            ThemesHandler th = new ThemesHandler();

            string mainWndXMLThemePath = Path.Combine(InfoClass.CurrentDir, 
                PathRoots.DataDirectory, PathRoots.MainWndColorThemeDatFile);

            string settingsWndXMLThemePath = Path.Combine(InfoClass.CurrentDir, 
                PathRoots.DataDirectory, PathRoots.SettingsWndColorThemeDatFile);

            string profilesWndXMLThemePath = Path.Combine(InfoClass.CurrentDir, 
                PathRoots.DataDirectory, PathRoots.ProfilesWndColorThemeDatFile);

            Action<string> createDir = (dir) => 
            {
                using (Stream s = File.Create(dir)) ;
            };


            if (!File.Exists(mainWndXMLThemePath))
                createDir(mainWndXMLThemePath);

            if(!File.Exists(settingsWndXMLThemePath))
                createDir(settingsWndXMLThemePath);

            if(!File.Exists(profilesWndXMLThemePath))
                createDir(profilesWndXMLThemePath);

            // write and set default theme to main window
            File.WriteAllText(mainWndXMLThemePath, StdXmlThemes.MainWindowThemeXML);
            th.LoadColorThemeToMainWindow(mainWndXMLThemePath, mw);
            th.SetNewMainWindowColorsThemeToXML(mainWndXMLThemePath, mw);

            // write and set default theme for settings window
            File.WriteAllText(settingsWndXMLThemePath, StdXmlThemes.SettingsWindowThemeXML);
            th.LoadColorThemeToSettingsWindow(settingsWndXMLThemePath, SettingsWindow);
            th.SetNewSettingsWindowColorsThemeToXML(settingsWndXMLThemePath, SettingsWindow);

            // just write default theme to xml file for profiles window
            File.WriteAllText(profilesWndXMLThemePath, StdXmlThemes.ProfilesWindowThemeXML);
        }
    }
}
