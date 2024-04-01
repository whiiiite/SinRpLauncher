using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Launcher.Classes;
using Launcher.Handlers;
using System.Threading;
using System.Windows;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using SinRpLauncher.Util;
using SinRpLauncher.Loaders;

namespace Launcher.Loaders
{
    public class LoadLauncherWindowLoader : IWindowLoaderAsync
    {
        LoadLauncherWindow lw;

        public LoadLauncherWindowLoader(LoadLauncherWindow lw)
        {
            this.lw = lw;
        }


        public async Task LoadWindowAsync()
        {
            GameFilesSystem.CreateSpecialDirs();

            bool UserHasInetConnection = Utils.userHasInternetConnection();
            if (UserHasInetConnection)
            {
                if (VersionControl.IsCurrentVersion() == VersionCode.IsNotCurrent) // Check is current version
                {
                    InfoClass.IsCurrentVersion = false; // if is not current version - set property to false

                    LauncherFilesSystem.StartUpdater(InfoClass.CurrentDir,
                                                     PathRoots.LauncherUpdaterExe,
                                                     PathRoots.InvokeUpdaterArg);
                }
            }

            await Task.Factory.StartNew(() =>
            {
                LauncherFilesSystem lfs = new LauncherFilesSystem();
                List<string> l = LanguagesTexts.AllWords;
                for (int i = 0; i < 101; i++)
                {
                    this.lw.Dispatcher.Invoke(() =>
                    {
                        lw.CircleProgressBar.ProgressValue = i;
                    });

                    if (i == 50)
                    {
                        this.lw.Dispatcher.Invoke(async () => {
                            this.lw.ChangeLoadTextBlockMeth(l[20]);
                            string sysMsg = await lfs.FilesIsWhole(); // get message from files check system
                            if (sysMsg != string.Empty)              // if return isnt string empty it means ONLY error string
                            {
                                AutoClosingMsgBox.Show(sysMsg, "Err", 3500);
                                Application.Current.Shutdown();
                            }
                        });
                    }

                    if (i == 75)
                    {
                        this.lw.Dispatcher.Invoke(() => {
                            this.lw.ChangeLoadTextBlockMeth(l[21]);
                        });
                        Thread.Sleep(1000);
                    }
                    Thread.Sleep(5);
                }
            });
        }
    }
}
