using Launcher.Classes;
using Microsoft.Win32.SafeHandles;
using SinRpLauncher.Util;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Launcher.BaseClasses
{
    /// <summary>
    /// Main base class. Contains error path log and all path log
    /// </summary>
    public class MainBaseClass 
    {
        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);


        private string _userPath = Directory.GetCurrentDirectory();

        private string _errLogPath = Path.Combine(Environment.CurrentDirectory, PathRoots.LogDirectory, PathRoots.ErrLogFile);
        private string _allLogPath = Path.Combine(Environment.CurrentDirectory, PathRoots.LogDirectory, PathRoots.AllLogFile);


        protected string userPath
        {
            get { return _userPath; }
        }


        /// <summary>
        /// Return path to log file for errors
        /// </summary>
        protected string errLogPath 
        { 
            get { return _errLogPath; }
        }


        /// <summary>
        /// Return path to log file for all events (not for exceptions)
        /// </summary>
        protected string allLogPath
        {
            get { return _allLogPath; }
        }
    }
}
