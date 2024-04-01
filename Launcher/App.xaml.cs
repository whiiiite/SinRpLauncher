using Launcher.Classes;
using Microsoft.Win32;
using SinRpLauncher.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Process curProc = Process.GetCurrentProcess();
            bool isStarted = Utils.SameProcessStarted(curProc.ProcessName, curProc.Id);
            if (isStarted) // if launcher started
            {
                MessageBox.Show("Launcher already started", "error");
                Application.Current.Shutdown();
                return;
            }

            base.OnStartup(e);

            try
            {
                LogsSys.WriteSystemConfigInfo(  Path.Combine(InfoClass.CurrentDir,
                                                PathRoots.LogDirectory,
                                                PathRoots.SystemConfigFileLog)   );

                MTAServer mtaServer = new MTAServer();
                mtaServer.SetRegistersForServer();
            }
            catch(Exception ex)
            {
                Utils u = new Utils();
                LogsSys.WriteErrorLog(u.GetErrorLogPath(), "null", ex);
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
            }
        }
    }
}
