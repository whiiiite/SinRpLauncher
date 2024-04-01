using System;
using System.IO;
using System.Runtime.InteropServices;
using Launcher.Classes;
using Microsoft.Win32.SafeHandles;
using SinRpLauncher.Util;

namespace Launcher.BaseClasses
{
    /// <summary>
    /// Base class for contains directories and files for launcher
    /// </summary>
    public class BaseLauncherFilesSystem : IDisposable
    {
        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);


        static string userPath = Environment.CurrentDirectory;

        private static string _cursorsPath = userPath + '\\' + PathRoots.ImagesDirectory + '\\' + PathRoots.CursDirectory;


        static private string[] _directories = new string[] 
        { 
            userPath + '\\' + PathRoots.DataDirectory + '\\',   // 0
            userPath + '\\' + PathRoots.TextDirectory + '\\',   // 1
            userPath + '\\' + PathRoots.ImagesDirectory + '\\', // 2
            userPath + '\\' + PathRoots.LogDirectory + '\\',    // 3
            userPath + '\\',                                    // 4
            cursorsPath + '\\',                                 // 5
        };


        static private string[] _files = new string[]
        {
            _directories[0] + PathRoots.ClientSettingsFile,
            _directories[0] + PathRoots.DataAccountFile,
            _directories[0] + PathRoots.ProfilesFile,
            _directories[0] + PathRoots.LaunchSettingsFile,
            _directories[0] + PathRoots.ServersDataFile,
            _directories[0] + PathRoots.MainWndColorThemeDatFile,
            _directories[0] + PathRoots.SettingsWndColorThemeDatFile,
            _directories[0] + PathRoots.ProfilesWndColorThemeDatFile,
            _directories[0] + PathRoots.HotKeysFile,
            _directories[0] + PathRoots.SerialNumberFile,

            _directories[1] + PathRoots.ImagesFile,
            _directories[1] + PathRoots.LinksSocialMediaJson,
            _directories[1] + PathRoots.UaLangPackFile,
            _directories[1] + PathRoots.EnLangPackFile,
            _directories[1] + PathRoots.RuLangPackFile,
            _directories[1] + PathRoots.LangListFile,

            _directories[3] + PathRoots.ErrLogFile,
            _directories[3] + PathRoots.AllLogFile,

            _directories[5] + PathRoots.LinkCurFile,
            _directories[5] + PathRoots.NormalCurFile,
        };


        /// <summary>
        /// Return all files in all directories of launcher
        /// </summary>
        protected string[] files 
        { 
            get { return _files; } 
        }


        /// <summary>
        /// Return all directories of launcher
        /// </summary>
        protected string[] directories 
        { 
            get { return _directories; } 
        }


        static protected string cursorsPath 
        {
            get { return _cursorsPath; } 
        }


        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle.Dispose();
                }

                _disposedValue = true;
            }
        }
    }
}
