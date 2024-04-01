using Launcher.Handlers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace Launcher.Classes
{
    /// <summary>
    /// Struct for get color in different in-et connection status.
    /// Red - when has not in-et connection.
    /// Orange - when has connection but hasnt installed updates
    /// LightGreen - when has connection and all updates is installed
    /// </summary>
    public struct ConnectionStatusBrush
    {
        /// <returns>
        /// Color for indicator when in-et is connected and launcher updated to last upd
        /// </returns>
        static public Brush HASINET
        {
            get { return Brushes.LightGreen; }
        }

        /// <returns>
        /// Color for indicator when in-et is connected, but launcher didnt updated
        /// </returns>
        static public Brush HASINETNOTUPDATED
        {
            get { return Brushes.Orange; }
        }

        /// <returns>
        /// Color for indicator when in-et is not connected
        /// </returns>
        static public Brush HASNTINET
        {
            get { return Brushes.Red; }
        }

        /// <returns>
        /// Color for indicator when game is not downloaded OR is not files of game whole
        /// </returns>
        static public Brush GAMEFILESINSTWHOLE
        {
            get { return Brushes.Red; }
        }
    }

    /// <summary>
    /// Class for contains some TI information
    /// </summary>
    public class InfoClass
    {
        #region Easter

        // for mainwindow hot key handler
        public static List<string> EasterCodes = new List<string>() { "aezakmi".ToUpper(), "baguvix".ToUpper(), 
                                                        "hesoyam".ToUpper(), "ineedsomehelp".ToUpper() };

        private static char[] _codeStr = new char[32] { '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
                                                        '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0',
                                                        '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', 
                                                        '\0', '\0', };
        public static char[] CodeStr
        {
            get { return _codeStr; } set { _codeStr = value; }
        }

        #endregion // Easter



        private static Languages LangsTmp = Utils.GetIntefaceLanguage();

        /// <summary>
        /// Contains current version name for check in from server 
        /// </summary>
        public const string curVer = "sin_rp-1.0.1.8_alpha";


        /// <summary>
        /// Return directory for debug project
        /// </summary>
        public static string DebugDir
        {
            get { return @"C:\Progs\VisualStudioProjects\Launcher\Launcher\bin\Debug\net6.0-windows"; }
        }
     

        /// <summary>
        /// Return directory for release
        /// </summary>
        public static string ReleaseDir
        {
            get { return Environment.CurrentDirectory; }
        }


        /// <summary>
        /// Return current project directory debug/release. Can be used to reference in more files
        /// </summary>
        public static string CurrentDir
        {
            get { return ReleaseDir; }
        }



        private static bool _UserHasConnection = false;
        /// <summary>
        /// Contains true or false. Returns and can be set - Has user internet connection or not
        /// </summary>
        public static bool UserHasConnection
        {
            get { return _UserHasConnection; }
            set { _UserHasConnection = value; }
        }

        private static bool _isDebugMode = true;
        /// <summary>
        /// this property set by programmer. If true - on start up shows debug window. else - it will not
        /// <para>Also launcher will have some debug functions</para>
        /// </summary>
        public static bool IsDebugMode { get { return _isDebugMode; } set { _isDebugMode = value; } }


        private static bool _IsCurrentVersion = false;
        /// <summary>
        /// Return true if laucher has passed Version Control, else - false. Can contain true or false.
        /// This value sets in LoadLauncherWindowLoader.
        /// </summary>
        public static bool IsCurrentVersion { get { return _IsCurrentVersion; } set { _IsCurrentVersion = value; } }


        private static bool _HasFailedNewsImages = true;
        /// <summary>
        /// Contains and return if some news images - is failed while downloaded
        /// </summary>
        public static bool HasFailedNewsImages
        {
            get { return _HasFailedNewsImages; }
            set { _HasFailedNewsImages = value; }
        }

        private static Languages _InterfaceLang = LangsTmp;
        public static Languages InterfaceLang 
        { 
            get { return _InterfaceLang; }  
            set { _InterfaceLang = value; LanguageText = new Handlers.LanguagesHandler(InterfaceLang).Words; } 
        }


        static protected List<string> LanguageText = new Handlers.LanguagesHandler(InterfaceLang).Words;


        private static bool _DownloadInProcess = false;
        /// <summary>
        /// Return bool value. If true - game/update - is in process of download. Else - false
        /// </summary>
        public static bool DownloadInProcess
        { 
            get { return _DownloadInProcess; } 
            set { _DownloadInProcess = value; } 
        }


        public const string FIRST_NEWS_IMAGE = "frst1";
        public const string SECOND_NEWS_IMAGE = "scnd2";
        public const string THIRD_NEWS_IMAGE = "thrd3";
        public const string FOUTH_NEWS_IMAGE = "frth4";
        public const string FIVTH_NEWS_IMAGE = "fvth5";
    }
}
