using System;
using System.Collections.Generic;
using Launcher.Classes;
using System.IO;
using System.Windows;
using Launcher.Handlers;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using SinRpLauncher.Debug;
using SinRpLauncher.Util;
using SinRpLauncher.Loaders;
using SinRpLauncher.Repository;

namespace Launcher.Loaders
{
    /// <summary>
    /// Class for handle loading of main window
    /// </summary>
    public class MainWindowsLoader : IWindowLoader
    {

        private MainWindow mw;
        private MainWindowHandlers mwHnds;

        public MainWindowsLoader(MainWindow mw, MainWindowHandlers mwHnds)
        {
            this.mw = mw;
            this.mwHnds = mwHnds;
        }


        public void LoadWindow()
        {
            // create handler for themes handle
            Handlers.ThemesHandler _ThemeHandler = new Handlers.ThemesHandler();

            mw.WindowStyle = WindowStyle.SingleBorderWindow; // for animate window minimize

            if (InfoClass.IsDebugMode) // check is debug mode
            {
                DebugTools.ShowConsoleWindow();
                DebugTools.PrintDebugInfo();
                DeveloperConsole c = new DeveloperConsole(mw);
                c.Show();
            }

            Utils.WriteSerialNumber();

            // check if user have connection to in-et
            InfoClass.UserHasConnection = Utils.userHasInternetConnection();
            if (!InfoClass.UserHasConnection)
            {
                Utils.MWSetLauncherStatus(mw, ConnectionStatusBrush.HASINET,
                                              LanguagesTexts.SubDescriprtionNotHasInternet,
                                              LanguagesTexts.SubDescriprtionNotHasInternet);
                mw.UpdateProgressBar.Value = 0;
            }

            // Get from json file prev user data and put it to fields
            XamlUtil.SetCursorsToControls(mw);
            mw.WaitRefreshIcon.Visibility = Visibility.Hidden;

            mw.CurrentNews = mw.NewsImg1; // set current news as first news
            mw.CurrentNewsEllipse = mw.Frst1; // set selected toggle ellipse as first
            mw.CurrentNewsEllipse.Fill = Utils.SelectedToggleColor; // fill it as selected
            mwHnds.ShowNewsHnd(mw, mw.CurrentNewsEllipse.Name); // show news

            if (!InfoClass.IsCurrentVersion && InfoClass.UserHasConnection)
            {
                mw.TextBlockUpdates.Text = LanguagesTexts.SubDescriprtionHasInternetNotUpdated;
                mw.UpdateAndInternetIndicator.Fill = ConnectionStatusBrush.HASINETNOTUPDATED;
                mw.UpdateProgressBar.Value = 0;
                //mw.UpdateDownloadButton.IsEnabled = true;
            }

            InfoClass.InterfaceLang = Utils.GetIntefaceLanguage();

            // path to json file with data
            string jpath = Directory.GetCurrentDirectory() + '\\' + PathRoots.DataDirectory + '\\' + PathRoots.DataAccountFile; 
            if (UserRepository.GetUserData(jpath) != null) 
            {
                Dictionary<string, string> UserData = UserRepository.GetUserData(jpath); // get user data from json file
                if (string.IsNullOrWhiteSpace(UserData["NickName"])) // if user have nickname already, put it to field
                {
                    mw.PlaceHolderNickNameBox.Visibility = Visibility.Hidden; // need to hide placeholder
                    mw.NickNameBox.Text = UserData["NickName"]; // put nickname to field
                }
                mw.ServersContainerListBox.SelectedIndex = int.Parse(UserData["ServerID"]);
            }

            // refresh it. For get sure that is a valid info about launcher 
            mwHnds.RefreshCheckConnetionBorderButtonHnd(mw);

            // colors xml file path
            string cPath = Directory.GetCurrentDirectory() + '\\'   + PathRoots.DataDirectory + '\\'
                                                                    + PathRoots.MainWndColorThemeDatFile; 
            _ThemeHandler.LoadColorThemeToMainWindow(cPath, mw); // load colors to window


            if (InfoClass.IsDebugMode)  mw.BuildName.Visibility = Visibility.Visible;
            else                        mw.BuildName.Visibility = Visibility.Hidden;
        }
    }
}
