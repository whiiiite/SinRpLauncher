using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using Launcher.BaseClasses;
using System.Windows;
using System.Diagnostics;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using Launcher.Handlers;
using CDialogWindow;
using Launcher.Extentions;
using SinRpLauncher.Util;

namespace Launcher.Classes
{

    /// <summary>
    /// 
    /// Class for check files system and work with it
    /// Update and Check
    /// 
    /// </summary>

    public class LauncherFilesSystem : BaseLauncherFilesSystem, IFilesSystem, IDisposable
    {
        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);


        /// <summary>
        /// Method for download.
        /// This method can be invoke ONLY if updater is loses his work and didnt started for update automatically
        /// </summary>
        /// <param name="mw"></param>
        /// <returns></returns>
        static public void UpdateDownload()
        {
            List<string> l = LanguagesTexts.AllWords;
            DialogWindow customDialogWindow = new DialogWindow();
            customDialogWindow.SetLanguage(InfoClass.InterfaceLang);
            string caption = l[0];
            string mainText = l[28];
            customDialogWindow.ShowCustomDialogWindow(mainText, caption);

            if(customDialogWindow.ResponseDialog == DialogWindowResponse.Yes)
            {
                LauncherFilesSystem.StartUpdater(Directory.GetCurrentDirectory() + '\\',
                                 PathRoots.LauncherUpdaterExe,
                                 PathRoots.InvokeUpdaterArg);
            }
            else{} // else we just do nothing
        }


        /// <summary>
        /// Checks all directories and files in launcher files system. Return NOT EMPTY string
        /// if has some issues
        /// </summary>
        /// <returns></returns>
        private async Task<string> CheckFilesIsWhole()
        {
            /// return string empty if dont have any problems with files or directories
            /// return a string if some file or directory is broke or have issues
            try
            {
                return await Task.Factory.StartNew(string() =>
                {
                    string cantWorkString = "Невозможно продолжать работу лаунчера.";

                    foreach (string directory in base.directories)
                    {
                        if (!Directory.Exists(directory))
                            return $"Директория:{directory}.\nНе существует или повреждена!\n{cantWorkString}";
                    }

                    foreach (string file in base.files)
                    {
                        if (!File.Exists(file))
                            return $"Файл:{file}.\nНе существует или поврежден!\n{cantWorkString}"; ;
                    }

                    return string.Empty;
                });
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        /// <summary>
        /// Return result of method that check files is whole. Return NOT EMPTY string if some error,
        /// returned string - is error text
        /// </summary>
        public async Task<string> FilesIsWhole()
        {
            try
            {
                return await CheckFilesIsWhole();
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// If version isn't current. Start process of update launcher
        /// </summary>
        public static void StartUpdater(string curDir, string updaterExe, string invokeArg)
        {
            string ExeFilePath = Path.Combine(curDir, updaterExe);

            if (File.Exists(ExeFilePath))
                Process.Start(updaterExe, invokeArg);

            Application.Current.Shutdown();
        }


        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle.Dispose();
                }

                _disposedValue = true;
            }

            // Call base class implementation.
            base.Dispose(disposing);
        }
    }
}
