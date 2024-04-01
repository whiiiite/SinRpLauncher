using System;

namespace SinRpLauncher.Util
{
    /// <summary>
    /// Contains the some parts of directories of launcher file system (roots)
    /// </summary>
    public class PathRoots
    {
        /// <summary>
        /// Return name of executable(exe) file with updater of launcher
        /// </summary>
        public const string LauncherUpdaterExe = "LauncherUpdater.exe";


        /// <summary>
        /// Return argument for start updater
        /// </summary>
        public const string InvokeUpdaterArg = "UpdateInvoke543";


        /// <summary>
        /// Return file that contains string with links social media in json format
        /// </summary>
        public const string LinksSocialMediaJson = "linkssocialmedia.json";


        /// <summary>
        /// Return file that contains string with game directory in json format
        /// </summary>
        public const string GameDirFile = "g_dir.sinrp";


        /// <summary>
        /// Return file name that contains client settings of laucher
        /// </summary>
        public const string ClientSettingsFile = "ClientSettings.sinrp";


        public const string MainDataFile = "MainDat.xml";


        /// <summary>
        /// Return file name that contains data of account
        /// </summary>
        public const string DataAccountFile = "DataAccount.sinrp";


        /// <summary>
        /// Return file name that contains data of launch settins. Like theme dark or light
        /// </summary>
        public const string LaunchSettingsFile = "LaunchSettings.sinrp";


        /// <summary>
        /// Return file name that contains data of profiles user
        /// </summary>
        public const string ProfilesFile = "Profiles.white";


        /// <summary>
        /// Return file name that contains data of hot keys and actions
        /// </summary>
        public const string HotKeysFile = "HotKeys.sinrp";


        /// <summary>
        /// Return file name that contains data of servers of project
        /// </summary>
        public const string ServersDataFile = "ServersData.sinrp";


        /// <summary>
        /// Return file name that contains data about system config file log
        /// </summary>
        public const string SystemConfigFileLog = "__sys_cfg.log";


        /// <summary>
        /// Return file name that contains data about color of interface mainwindow
        /// </summary>
        public const string MainWndColorThemeDatFile = "MainWndColorThemeDat.xml";


        /// <summary>
        /// Return file name that contains data about color theme of settings window
        /// </summary>
        public const string SettingsWndColorThemeDatFile = "SettingsWndColorThemeDat.xml";


        /// <summary>
        /// Return file name that contains data about color theme of profiles window
        /// </summary>
        public const string ProfilesWndColorThemeDatFile = "ProfilesWndColorThemeDat.xml";


        /// <summary>
        /// Return special file that contains serial number of launcher
        /// </summary>
        public const string SerialNumberFile = "marker-1.sn";


        /// <summary>
        /// Return file name that contains all logs events
        /// </summary>
        public const string AllLogFile = "alog.log";


        /// <summary>
        /// Return file name that contains error log
        /// </summary>
        public const string ErrLogFile = "elog.log";


        /// <summary>
        /// Return file name that contains images uris and urls
        /// </summary>
        public const string ImagesFile = "images.json";


        /// <summary>
        /// Return file name that contains russian language text
        /// </summary>
        public const string RuLangPackFile = "ru.slp";


        /// <summary>
        /// Return file name that contains english language text
        /// </summary>
        public const string EnLangPackFile = "en.slp";


        /// <summary>
        /// Return file name that contains ukraine language text
        /// </summary>
        public const string UaLangPackFile = "ua.slp";


        public const string LangListFile = "langlist.txt";


        /// <summary>
        /// Return file name that contains normal cursor
        /// </summary>
        public const string NormalCurFile = "Normal.cur";


        /// <summary>
        /// Return file name that contains link cursor
        /// </summary>
        public const string LinkCurFile = "Link.cur";


        /// <summary>
        /// Return name of folder where contains some text info for launcher
        /// </summary>
        public const string TextDirectory = "text";


        /// <summary>
        /// Return name of folder where contains some data for launcher
        /// </summary>
        public const string DataDirectory = @"data";


        /// <summary>
        /// Return name of folde with images
        /// </summary>
        public const string ImagesDirectory = @"images";


        /// <summary>
        /// Return name of folder with logs
        /// </summary>
        public const string LogDirectory = "log";

        /// <summary>
        /// Return name of folder with cursors
        /// </summary>
        public const string CursDirectory = "curs";

        /// <summary>
        /// Return name of folder that contains temporary files
        /// </summary>
        public const string TmpDirectory = "tmp";


        /// <summary>
        /// Contains magnet link or path to the torrent data
        /// </summary>
        public static string TorrentToDownload { get; set; } 
            = Environment.CurrentDirectory + '\\' + PathRoots.TmpDirectory + '\\' + "SIN_ROLE_PLAY.torrent";
    }
}
