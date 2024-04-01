using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.IO;
using Launcher.Classes;
using Launcher.BaseClasses;
using System.Threading;
using System.Diagnostics;
using SinRpLauncher.Classes;
using CDialogWindow;
using Launcher.Extentions;
using System.Windows.Threading;
using SinRpLauncher;
using SinRpLauncher.Util;
using Path = System.IO.Path;
using SinRpLauncher.Repository;

namespace Launcher.Handlers
{
    public class MainWindowHandlers : BaseHandler
    {
        MainWindow mw;
        Thread scrollNewsThread;

        private CancellationTokenSource downloadCancelTokenSource;

        private bool gameDownloadInProcess;

        private Task DownloadGameTask;

        public MainWindowHandlers(MainWindow mw)
        {
            this.mw = mw;

            downloadCancelTokenSource = new CancellationTokenSource();
            gameDownloadInProcess = false;
        }

        /// <summary>
        /// Handler for enter to game button. 
        /// <br/>
        /// Checks some values and allow or not entry to game
        /// </summary>
        /// <param name="mw"></param>
        public async void ToGameButtonClickHnd(MainWindow mw)
        {
            string pathDataAcc = Path.Combine(InfoClass.CurrentDir, PathRoots.DataDirectory, PathRoots.DataAccountFile);
            MTAServerRepository mtaServerRepository = new MTAServerRepository();

            DialogWindow? msgBox = Utils.GetPrepairedDialogBox();

            if (!ValidateEntryGameData(msgBox, mw.NickNameBox.Text))
                return;

            await RefreshCheckConnetionBorderButtonHnd(mw);

            UserAccount userAccount = new UserAccount();
            userAccount.NickName = mw.NickNameBox.Text.Trim(); 
            userAccount.Server = (byte)mw.ServersContainerListBox.SelectedIndex; 

            string NickName = userAccount.NickName;
            byte SelectedServer = userAccount.Server;

            // get data about picked server
            string path = Path.Combine(InfoClass.CurrentDir, PathRoots.DataDirectory, PathRoots.ServersDataFile);
            MTAServer mtaServ = mtaServerRepository.GetMTAServer(path, SelectedServer);

            UserRepository.WriteUserData(pathDataAcc, userAccount); // serialize data and write to account data json file

            if (!HasPermissionEntryToGame())
            {
                HandleIssuesWhileEntryToGame(msgBox);
                return;
            }

            msgBox.ShowCustomDialogWindow("Вы зайдете на сервер с ником: " 
                + $"{NickName} " + "На сервер: " + SelectedServer + 1, "",
                DialogWindowButtons.YesNoCancel);

            EntryToGame(msgBox.ResponseDialog, mtaServ);

            msgBox = null;
        }


        /// <summary>
        /// Indicates if all ok and user has permission to entry the game
        /// </summary>
        /// <returns></returns>
        private bool HasPermissionEntryToGame()
        {
            return InfoClass.IsCurrentVersion &&
                        GameFilesSystem.GameIsInstalled &&
                        InfoClass.UserHasConnection ||
                        Opcodes.adm_ver_on;
        }


        /// <summary>
        /// Validate info about user for entry game. 
        /// Assert the messages in msgBox if some errors
        /// </summary>
        /// <param name="msgBox"></param>
        /// <param name="nickName"></param>
        /// <returns></returns>
        private bool ValidateEntryGameData(DialogWindow msgBox, string nickName)
        {
            if (string.IsNullOrWhiteSpace(nickName))
            {
                msgBox.ShowCustomDialogWindow("Для начала введите ник", LanguagesTexts.ErrorWord);
                return false;
            }

            if (!(UserDataUtil.NickNameIsValid(nickName) || Opcodes.adm_ver_on))
            {
                msgBox.ShowCustomDialogWindow(LanguagesTexts.DescriptionWrongNickname, LanguagesTexts.ErrorWord);
                return false;
            }

            return true;
        }


        /// <summary>
        /// Handles the issues if something happend while entry to game
        /// </summary>
        /// <param name="msgBox"></param>
        private async void HandleIssuesWhileEntryToGame(DialogWindow msgBox)
        {
            if (!InfoClass.UserHasConnection)
            {
                msgBox.ShowCustomDialogWindow(LanguagesTexts.SubDescriprtionNotHasInternet, LanguagesTexts.DescriptionMaybeNoInet,
                                                DialogWindowButtons.Ok);
            }
            else if (!InfoClass.IsCurrentVersion)
            {
                msgBox.ShowCustomDialogWindow(LanguagesTexts.DescriptionUpdateFilesLauncher, "", DialogWindowButtons.Ok);
                await UpdateDownloadHnd(Opcodes.UpdateLauncher);
            }
            else if (!GameFilesSystem.GameIsInstalled)
            {
                GameIsNotInstallIssueHandler(msgBox);
            }
            else if (!GameFilesSystem.HashIsEqualsFtp(GameFilesSystem.GameDir))
            {
                MessageBox.Show("Хэш не валиден, но я еще тут не сделал обработчик");
            }
        }


        /// <summary>
        /// Handler for issue when game is not installed 
        /// </summary>
        /// <param name="msgBox"></param>
        private async void GameIsNotInstallIssueHandler(DialogWindow msgBox)
        {
            msgBox.AgreeButton.Content = LanguagesTexts.AllWords[14];
            msgBox.NoButton.Content = LanguagesTexts.AllWords[39];

            msgBox.ShowCustomDialogWindow(LanguagesTexts.DescriptionDownloadGame, "", DialogWindowButtons.YesNoCancel);

            if (msgBox.ResponseDialog == DialogWindowResponse.Yes)
            {
                SettingsWindowHandlers settingsWindowHandlers = new SettingsWindowHandlers();

                settingsWindowHandlers.FileSourceButton_ClickHnd(new WindowSettings());
                await RefreshCheckConnetionBorderButtonHnd(mw);
            }
            else if (msgBox.ResponseDialog == DialogWindowResponse.No)
            {
                await UpdateDownloadHnd(Opcodes.DownloadGame);
            }
            else if (msgBox.ResponseDialog == DialogWindowResponse.Cancel) ; // manually do nothing
        }


        private void EntryToGame(DialogWindowResponse response, MTAServer server)
        {
            string pathExeGame = Path.Combine(GameFilesSystem.GameDir, GameFilesSystem.MTADir, GameFilesSystem.MTAExeFile);

            if (File.Exists(pathExeGame) && response == DialogWindowResponse.Yes)
                Process.Start(pathExeGame, server.GetIPEnterServer());
        }


        /// <summary>
        /// Handler when launcher is closing
        /// </summary>
        public void CloseButtonHnd()
        {
            List<string> l = LanguagesTexts.AllWords;       // get all words in language
            string text = l[29];                            // text for message box
            DialogWindow msgBox = new DialogWindow();       
            msgBox.SetLanguage(InfoClass.InterfaceLang);    // set language to dialog box
            bool? msgBoxResult = msgBox.ShowCustomDialogWindow(text);
            if (msgBoxResult == true)
            {
                Application.Current.Shutdown();
            }
        }


        public void MinimizedButtonHnd(MainWindow mw)
        {
            mw.WindowState = WindowState.Minimized;
        }


        public void PlaceHolderNickNameBoxHnd(MainWindow mw)
        {
            if (string.IsNullOrWhiteSpace(mw.NickNameBox.Text))
            {
                mw.PlaceHolderNickNameBox.Visibility = Visibility.Hidden; // hide the placeholder
                mw.NickNameBox.Focus(); // focus to the nickname text box
            }
        }


        /// <summary>
        /// Handler when nickname text box lost the focus
        /// </summary>
        /// <param name="mw"></param>
        public void NickNameBox_LostFocusHnd(MainWindow mw)
        {
            if (!string.IsNullOrWhiteSpace(mw.NickNameBox.Text)) return;

            List<string> l = LanguagesTexts.AllWords; // get all words in current lang
            mw.PlaceHolderNickNameBox.Visibility = Visibility.Visible; // show the placeholder
            mw.PlaceHolderNickNameBox.Text = "\n" + l[9]; // set the text of placeholder
        }


        /// <summary>
        ///  Handler for social media buttons(border buttons) and also nav bar buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mw"></param>
        /// <returns></returns>
        public async Task SocialMediaButtonHnd(object sender, MainWindow mw)
        {
            object? button = sender as Button;
            if (sender is Button)
            {
                button = (Button)sender;
            }

            await Task.Factory.StartNew(() =>
            {
                try
                {
                    mw.Dispatcher.Invoke(() => 
                    { 
                        Utils.RedirectToSocialMedia(Utils.GetValueFromProp(button, "Name")); 
                    });
                }
                catch (Exception ex)
                {
                    Utils.ShowMessageBoxErrorIfNotDebug(ex);
                }
            });
        }


        /// <summary>
        /// Method for change picked news ellipse for new news and call Method for show new news
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="sender"></param>
        public void ToggleNewsHnd(MainWindow mw, object sender)
        {
            Ellipse ellipse = (Ellipse)sender;
            if(ellipse != mw.CurrentNewsEllipse)
            {
                ThemesHandler themesHandler = new ThemesHandler();
                themesHandler.ToggleChangeAnim(mw, mw.CurrentNewsEllipse, ellipse, new TimeSpan(0, 0, 0, 0, 300), "Margin");
                mw.CurrentNewsEllipse = Utils.PickNewsToggle(mw.CurrentNewsEllipse, ellipse);
                ShowNewsHnd(mw, mw.CurrentNewsEllipse.Name);
            }
        }


        /// <summary>
        /// Method for show image of picked news
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="nameEllipse"></param>
        public void ShowNewsHnd(MainWindow mw, string currEllipseName)
        {
            Utils.SetNewsImage(mw, currEllipseName);
        }


        /// <summary>
        /// While selected item in combobox changed - get info about server and put it to text block
        /// </summary>
        /// <param name="mw"></param>
        /// <returns></returns>
        public async Task ServersBox_SelectionChangedHnd(MainWindow mw)
        {
            await Task.Factory.StartNew(() =>
            {
                mw.Dispatcher.Invoke(DispatcherPriority.Background, () =>
                {
                    try
                    {
                        mw.PickedServer.ServerPicked = false;

                        ServerContainer tmpContainer = (ServerContainer)mw.ServersContainerListBox
                                                        .Items[mw.ServersContainerListBox.SelectedIndex];

                        if (tmpContainer.IsActive)
                        {
                            mw.PickedServer = tmpContainer;
                            mw.PickedServer.ServerPicked = true;
                        }
                        else if (!tmpContainer.IsActive)
                        {
                            mw.ServersContainerListBox.SelectedItem = mw.PickedServer;
                            mw.PickedServer.ServerPicked = true;
                        }

                        // temporary we have only one server. Set info about first server from xml
                        MTAServer server = new MTAServer();
                        int playersCount = server.GetPlayersData(0).current;
                        mw.PickedServer.CurrentPlayersCount = (uint)playersCount;
                    }
                    catch(Exception ex) 
                    {
                        Utils utils = new Utils();
                        LogsSys.WriteErrorLog(utils.GetErrorLogPath(), "ServersBox_SelectionChangedHnd", ex);
                        DialogWindow d = Utils.GetPrepairedDialogBox();
                        d.ShowCustomDialogWindow(LanguagesTexts.DescriptionErrorWhileGetData, "", DialogWindowButtons.Ok);
                    }
                });
            });
        }


        /// <summary>
        /// Opens dialog settings window
        /// </summary>
        public async void SettingsButtonHnd(MainWindow mw)
        {
            WindowSettings settingsWnd = new WindowSettings(ref mw);
            // contain old data
            string tmpFolder    = settingsWnd.GamePathTextBox.Text;
            int tmpSelectedLang = settingsWnd.LanguagesComboxBox.SelectedIndex;

            settingsWnd.Topmost = true;

            LanguagesHandler.SetLanguageToMainWindow(mw, InfoClass.InterfaceLang);

            settingsWnd.ShowDialog();

            // if data was changed
            if(settingsWnd.GamePathTextBox.Text != tmpFolder || 
                tmpSelectedLang != settingsWnd.LanguagesComboxBox.SelectedIndex)
            {
                await RefreshCheckConnetionBorderButtonHnd(mw);
            }
        }


        /// <summary>
        /// Handle update download button. Download update if need. Download game if need
        /// </summary>
        /// <param name="mw"></param>
        /// <returns></returns>
        public async Task UpdateDownloadHnd(int opcode)
        {
            try
            {
                if (opcode == Opcodes.UpdateLauncher && InfoClass.IsCurrentVersion == false)
                {
                    LauncherFilesSystem.UpdateDownload();
                    InfoClass.IsCurrentVersion = true;
                }
                else if (opcode == Opcodes.DownloadGame && !GameFilesSystem.GameIsInstalled)
                {
                    try
                    {
                        gameDownloadInProcess = true;
                        downloadCancelTokenSource = new CancellationTokenSource();

                        GameFilesSystem gameFilesSystem = new GameFilesSystem();
                        DownloadGameTask = gameFilesSystem.DownloadGameAsync(mw,
                        PathRoots.TorrentToDownload,
                        downloadCancelTokenSource
                        );

                        await DownloadGameTask;
                    }
                    catch (Exception ex)
                    {
                        Utils.ShowMessageBoxErrorIfNotDebug(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
            }
        }


        /// <summary>
        /// Opens dialog profiles of user. If return true(profile was picked) - change nickanme and server. 
        /// If false - do nothing
        /// </summary>
        /// <param name="mw"></param>
        public void UserProfilesButtonHnd(MainWindow mw)
        {
            WindowProfiles windowProfiles = new WindowProfiles();
            bool? dialogResult = windowProfiles.ShowDialog();

            if (dialogResult == false || dialogResult == null) return;

            byte pickedServer = windowProfiles.PickedProfileServer;
            string pickedNickName = windowProfiles.PickedProfileNickName;

            mw.PlaceHolderNickNameBox.Visibility = Visibility.Hidden;
            mw.ServersContainerListBox.SelectedIndex = pickedServer - 1;
            mw.NickNameBox.Text = pickedNickName;
        }


        public void Button_MouseEnterHnd(ThemesHandler _ThemesHandler, object sender)
        {
            _ThemesHandler.ButtonMouseEnter((Button)sender);
        }


        public void Button_MouseLeaveHnd(ThemesHandler _ThemesHandler, object sender)
        {
            _ThemesHandler.ButtonMouseLeave((Button)sender);
        }


        public void NickNameBox_MouseEnterHnd(ThemesHandler _ThemesHandler, object sender)
        {
        }


        public void NickNameBox_MouseLeaveHnd(ThemesHandler _ThemesHandler, object sender)
        {
        }


        /// <summary>
        /// Refresh info about launcher. Like internet connection, Is current version of launcher, etc.
        /// </summary>
        /// <param name="mw"></param>
        public async Task RefreshCheckConnetionBorderButtonHnd(MainWindow mw)
        {
            // is a wrapper method for call main logic
            // it was created for solve the problem with long freeze thread while refresh

            await RefreshCheckConnectionBorderButtonHandler(mw);
        }


        /// <summary>
        /// Refresh info about launcher. Like internet connection, Is current version of launcher, etc.
        /// </summary>
        /// <param name="mw"></param>
        private async Task RefreshCheckConnectionBorderButtonHandler(MainWindow mainWindow)
        {
            // Show loading icon and disable button
            DisableRefresh(mainWindow);

            await Task.Delay(170); // Fake delay for UI

            // Check current version
            // set to InfoClass some data about version
            CheckAndSetCurrectVersionAndConnection();

            bool hasInternetConnection = InfoClass.UserHasConnection;
            bool isCurrentVersion = InfoClass.IsCurrentVersion;

            if (hasInternetConnection)
                SetIfNotInetConnection(mainWindow);

            LoadNewsImagesIfNotLoaded(mainWindow);

            // if launcher download something - dont move forward
            if (InfoClass.DownloadInProcess) return;
            
            // Check if game files are correct
            bool gameFilesIsCorrect = GameFilesSystem.GameFilesIsCorrect(GameFilesSystem.GameDir);
            if (!gameFilesIsCorrect)
                SetIfGameNotCorrect(mainWindow);
            else
                GameFilesSystem.GameIsInstalled = true;
            
            if (!isCurrentVersion)
                SetIfNotCurrentVersion(mainWindow);
            
            if (isCurrentVersion && GameFilesSystem.GameIsInstalled)
                SetIfAllOk(mainWindow);

            // Hide loading icon and enable button
            EnableRefresh(mainWindow);
        }


        /// <summary>
        /// Sets global variables and mainwindow UI 
        /// if user has not inet connection
        /// </summary>
        /// <param name="mainWindow"></param>
        private void SetIfNotInetConnection(MainWindow mainWindow)
        {
            Utils.MWSetLauncherStatus(mainWindow, ConnectionStatusBrush.HASNTINET,
            LanguagesTexts.SubDescriprtionNotHasInternet,
            LanguagesTexts.SubDescriprtionNotHasInternet);
        }


        /// <summary>
        /// Sets global variables and mainwindow UI if is all ok with launcher
        /// </summary>
        /// <param name="mainWindow"></param>
        private void SetIfAllOk(MainWindow mainWindow)
        {
            Utils.MWSetLauncherStatus(mainWindow, ConnectionStatusBrush.HASINET,
                            LanguagesTexts.DescriptionHaveGoodPlay,
                            LanguagesTexts.SubDescriprtionHasInternetAndAllUpdates);

            mainWindow.toGameButton.Content = LanguagesTexts.AllWords[10];
            mainWindow.StopDownloadButton.Visibility = Visibility.Hidden;
            mainWindow.UpdateProgressBar.Value = mainWindow.UpdateProgressBar.Maximum;
        }


        /// <summary>
        /// Sets global variables and mainwindow UI if is not current version of launcher
        /// </summary>
        /// <param name="mainWindow"></param>
        private void SetIfNotCurrentVersion(MainWindow mainWindow)
        {
            Utils.MWSetLauncherStatus(mainWindow, ConnectionStatusBrush.HASINETNOTUPDATED,
                            LanguagesTexts.DescriptionWarningUpdateFiles,
                            LanguagesTexts.SubDescriprtionHasInternetNotUpdated);

            mainWindow.toGameButton.Content = LanguagesTexts.AllWords[0];
        }


        /// <summary>
        /// Sets global variables and mainwindow UI if game is not downloaded
        /// </summary>
        /// <param name="mainWindow"></param>
        private void SetIfGameNotCorrect(MainWindow mainWindow)
        {
            Utils.MWSetLauncherStatus(mainWindow, ConnectionStatusBrush.GAMEFILESINSTWHOLE,
            LanguagesTexts.DescriptionWarningUpdateFiles,
            LanguagesTexts.SubDescriptionGameIsntDownloaded);

            GameFilesSystem.GameIsInstalled = false;
            mainWindow.toGameButton.Content = LanguagesTexts.AllWords[0];
        }


        /// <summary>
        /// Checks and Sets global variables and mainwindow UI 
        /// if is current version or not
        /// </summary>
        /// <param name="mainWindow"></param>
        private void CheckAndSetCurrectVersionAndConnection()
        {
            VersionCode versionCode = VersionControl.IsCurrentVersion();

            bool hasInternetConnection = Utils.userHasInternetConnection();
            bool isCurrentVersion = versionCode == VersionCode.Current;
            bool isExceptionVerCode = versionCode == VersionCode.Exception;

            InfoClass.UserHasConnection = hasInternetConnection && !isExceptionVerCode;
            InfoClass.IsCurrentVersion = isCurrentVersion;
            InfoClass.InterfaceLang = Utils.GetIntefaceLanguage();
        }


        /// <summary>
        /// Disables opportunity to refresh UI
        /// </summary>
        /// <param name="mainWindow"></param>
        private void DisableRefresh(MainWindow mainWindow)
        {
            mainWindow.WaitRefreshIcon.Visibility = Visibility.Visible;
            mainWindow.RefreshCheckConnetionBorderButton.IsEnabled = false;
        }


        /// <summary>
        /// Enables opportunity to refresh UI
        /// </summary>
        /// <param name="mainWindow"></param>
        private void EnableRefresh(MainWindow mainWindow)
        {
            mainWindow.WaitRefreshIcon.Visibility = Visibility.Hidden;
            mainWindow.RefreshCheckConnetionBorderButton.IsEnabled = true;
        }


        /// <summary>
        /// Loads new images if some of them does not loaded yet.
        /// </summary>
        /// <param name="mainWindow"></param>
        private void LoadNewsImagesIfNotLoaded(MainWindow mainWindow)
        {
            Utils.LoadNewsImages(mainWindow); // If some news images are not downloaded, load them
            InfoClass.HasFailedNewsImages = false;
        }



        /// <summary>
        /// Open custom pick color dialog. For pick background & foreground for control
        /// </summary>
        /// <param name="sender">menu item</param>
        public void PickColorControlHnd(object sender, MainWindow mw)
        {
            if (sender is not MenuItem)
                return;

            string mainWindowThemePath = Path.Combine(InfoClass.CurrentDir,
                           PathRoots.DataDirectory, PathRoots.MainWndColorThemeDatFile);

            // we need got menuitem
            MenuItem menuItem = (MenuItem)sender;
            FrameworkElement owner = (FrameworkElement)((ContextMenu)menuItem.Parent).PlacementTarget;

            ThemesHandler themesHandler = new ThemesHandler();
            if (owner == mw.MinimizeButton || owner == mw.CloseButton
                || owner.Name.ToLower().StartsWith("nav"))
            {
                themesHandler.PickControlColors(menuItem, offBG:true); // pick color for control
            }
            else
            {
                themesHandler.PickControlColors(menuItem); // pick color for control
            }
            themesHandler.SetNewMainWindowColorsThemeToXML(mainWindowThemePath, mw); // set new data to xml
        }


        /// <summary>
        /// Handler for scroll news thread
        /// </summary>
        private void HandlerScrollNewsThread()
        {
            int timeoutMlSec = 7000; // timeout for scroll news thread
            int c = 0; // counter
            while (true)
            {
                mw.Dispatcher.Invoke(() =>{
                    switch (c)
                    {
                        case 0:
                            ToggleNewsHnd(mw, mw.Frst1);
                            c++;
                            break;
                        case 1:
                            ToggleNewsHnd(mw, mw.Scnd2);
                            c++;
                            break;
                        case 2:
                            ToggleNewsHnd(mw, mw.Thrd3);
                            c++;
                            break;
                        case 3:
                            ToggleNewsHnd(mw, mw.Frth4);
                            c++;
                            break;
                        case 4:
                            ToggleNewsHnd(mw, mw.Fvth5);
                            c = 0;
                            break;
                    }
                });
                scrollNewsThread.Join(timeoutMlSec); // wait
            }
        }


        /// <summary>
        /// Initialize thread for scrolling news
        /// </summary>
        /// <returns>Thread</returns>
        private Thread InitScrollNewsThread()
        {
            ThreadStart startHnd = HandlerScrollNewsThread;
            return new Thread(startHnd);
        }


        /// <summary>
        /// Starts thread for scroll news images
        /// </summary>
        /// <returns>Thread for operating this one</returns>
        public Thread StartScrollNewsThread()
        {
            Thread scrollNewsThread = InitScrollNewsThread();
            scrollNewsThread.Start();

            // put thread to field in class for get it after.
            this.scrollNewsThread = scrollNewsThread; 

            return scrollNewsThread;
        }


        /// <summary>
        /// Handler when at least 1 new image is not downloaded
        /// </summary>
        public void NewsImg_Failed_Download_Hnd()
        {
            InfoClass.HasFailedNewsImages = true;
        }


        /// <summary>
        /// Handler when user change the text in textbox for nickname
        /// </summary>
        public void NickNameBoxTextChangedHnd()
        {
            if(mw.NickNameBox.Text.Length == 0)
            {
                mw.PlaceHolderNickNameBox.Visibility = Visibility.Visible;
            }
            else if(mw.NickNameBox.Text.Length > 0)
            {
                mw.PlaceHolderNickNameBox.Visibility = Visibility.Hidden;
            }

            return;
        }


        /// <summary>
        /// Stops the downloading of game 
        /// </summary>
        public async void StopDownloadButton_ClickHnd()
        {
            if (gameDownloadInProcess)
            {
                downloadCancelTokenSource.Cancel();
                gameDownloadInProcess = false;
                return;
            }

            gameDownloadInProcess = true;
            downloadCancelTokenSource = new CancellationTokenSource();
            
            try
            {
                DownloadGameTask = UpdateDownloadHnd(Opcodes.DownloadGame);
                await DownloadGameTask;
            }
            catch(Exception) 
            {

            }
        }
    }
}
