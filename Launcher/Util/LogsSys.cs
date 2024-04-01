using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Launcher.Classes
{
    /// <summary>
    /// Represents simple logging system
    /// </summary>
    public static class LogsSys
    {

        /// <summary>
        /// Use for catch. Method for write in error file(elog.txt) summary about exception/error
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sender"></param>
        /// <param name="exc"></param>
        /// <param name="t"></param>
        public static void WriteErrorLog(string path, object sender, Exception exc)
        {
            CreateLogIfNotExists(path);

            if (!File.Exists(path)) return; 
            
            string log = "\n" + ( Utils.FormatDateTime(DateTime.Now) + " : " + sender.ToString() + " : " + exc.ToString()).Replace('\n', ' ');
            log = log.Replace('\\', '/');
            File.AppendAllText(path, log);
        }


        /// <summary>
        /// Method for write log for some event in alog.txt. Not for exceptions or errors
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <param name="time"></param>
        public static void WriteSimpleLog(string path, object sender, string msg = "")
        {
            CreateLogIfNotExists(path);

            if (!File.Exists(path)) return;

            string log = "\n" + ( Utils.FormatDateTime(DateTime.Now) + " : " + sender.ToString() + " : " + msg).Replace('\n', ' ');
            log = log.Replace('\\', '/');
            File.AppendAllText(path, log);
        }


        /// <summary>
        /// Creates the log file if it doesn't exits
        /// </summary>
        /// <param name="path"></param>
        private static void CreateLogIfNotExists(string path) 
        {
            if (!File.Exists(path))
            {
                FileStream fstream = File.Create(path);
                fstream.Close();
            }
        }


        /// <summary>
        /// Constructs message for simple log. Contains value from property 'Name' of sender as a UIelement
        /// and add additional message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="add"></param>
        /// <returns></returns>
        public static string ConstructMsg(object sender, object? additonal)
        {
            if (sender is not FrameworkElement) return string.Empty;

            return Utils.GetValueFromProp(sender, "Name") + ' ' + additonal ?? ""; // construct message
        }


        /// <summary>
        /// Write the system info about hardware and system in log file 
        /// </summary>
        /// <param name="path"></param>
        public static void WriteSystemConfigInfo(string path)
        {
            CreateLogIfNotExists(path);

            if (!File.Exists(path)) return;

            string log = string.Empty;
            log += "[" + Utils.FormatDateTime(DateTime.Now) + "]" + '\n';
            log += "Process ID: " + Process.GetCurrentProcess().Id.ToString();
            log += '\n';
            log += "Process name: " + Process.GetCurrentProcess().ProcessName;
            log += '\n';

            File.WriteAllText(path, log); // need to rewrite data about hardware
        }
    }
}
