using System.Threading.Tasks;
using Launcher.BaseClasses;
using System.IO;
using System;
using System.Collections.Generic;

using MonoTorrent.Client;
using System.Windows;
using System.Threading;
using Launcher.Handlers;
using SinRpLauncher.Util;
using MonoTorrent;
using SinRpLauncher.Extentions;
using System.Net;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace Launcher.Classes
{
    public class GameFilesSystem : BaseGameFilesSystem, IFilesSystem
    {



        private static bool _GameIsInstalled = false;
        /// <summary>
        /// Return true if game dir != empty.
        /// Else - false
        /// </summary>
        public static bool GameIsInstalled
        {
            get { return _GameIsInstalled; }
            set { _GameIsInstalled = value; }
        }


        private static bool _GameFilesIsWhole = true;
        /// <summary>
        /// Return game files is whole 
        /// </summary>
        public static bool GameFilesIsWhole
        {
            get { return _GameFilesIsWhole; }
            set { _GameFilesIsWhole = value; }
        }


        /// <summary>
        /// Return <b>"Multi Theft Auto.exe"</b>
        /// </summary>
        public static string MTAExeFile
        {
            get { return "Multi Theft Auto.exe"; }
        }


        /// <returns>"MTA"</returns>
        public static string MTADir
        {
            get { return "mta"; }
        }


        /// <returns>"gta_sa.exe"</returns>
        public static string GTASAExeFile
        {
            get { return "gta_sa.exe"; }
        }


        private static string jkeyGameDir = "game_dir";
        private static string _GameDir = GetGameDir(InfoClass.CurrentDir, PathRoots.DataDirectory, PathRoots.GameDirFile, jkey: jkeyGameDir);
        /// <summary>
        /// Return directory of game. Don't include backslash(\) in end 
        /// </summary>
        public static string GameDir
        {
            get { return _GameDir; }
            set { _GameDir = value; }
        }



        /// <summary>
        /// If return NOT empty string - it means that files have some issues
        /// </summary>
        /// <returns></returns>
        private Task<string> _CheckFilesIsWhole()
        {
            return Task.Factory.StartNew(string() =>
            {
                foreach(string file in base.files)
                {
                    if (!File.Exists(file))
                    {
                        return "Файл " + file + " Не существует. Невозможно запустить игру";
                    }
                }
                foreach(string dir in base.directories)
                {
                    if (!Directory.Exists(dir))
                    {
                        return "Директория " + dir + " Не существует. Невозможно запустить игру";
                    }
                }
                return string.Empty;
            });
        }


        /// <summary>
        /// If return NOT empty string - it means that files have some issues
        /// </summary>
        /// <returns></returns>
        public async Task<string> FilesIsWhole()
        {
            return await _CheckFilesIsWhole();
        }


        /// <summary>
        /// Checks the hashes from special DB.
        /// </summary>
        /// <returns>False - if hashes is not equals. True - if files was not edited and hashes equals </returns>
        /// <exception cref="NotImplementedException"></exception>
        private bool CheckGameHash()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Check some special files and directories in game directory 
        /// </summary>
        /// <returns>Bool value. If true - game files is correct, else - false</returns>
        public static bool GameFilesIsCorrect(string pathToGame)
        {
            return  (
                        File.Exists(pathToGame + '\\' + GameFilesSystem.GTASAExeFile)   &&
                        Directory.Exists(pathToGame + '\\' + GameFilesSystem.MTADir)    
                    ); 
        }


        /// <summary>
        /// Creates some special directories for game
        /// </summary>
        public static void CreateSpecialDirs()
        {
            string sysDisk = Path.GetPathRoot(Environment.SystemDirectory) ?? "C:\\";
            string mainDir = Path.Combine(sysDisk, "ProgramData\\");
            string projectName = "MTA Sin All";
            string dir0 = mainDir + projectName;
            string dir1 = dir0 + "\\Common";
            string dir2 = dir1 + "\\data";

            void mkDir0() { Directory.CreateDirectory(dir0); }
            void mkDir1() { Directory.CreateDirectory(dir1); }
            void mkDir2() { Directory.CreateDirectory(dir2); }

            void main()
            {
                if (!Directory.Exists(dir0)) mkDir0();
                if (!Directory.Exists(dir1)) mkDir1();
                if (!Directory.Exists(dir2)) mkDir2();
            }

            main();
        }


        /// <summary>
        /// Download game from server
        /// </summary>
        /// <returns></returns>
        // DO NOT MAKE IT STATIC
        public async Task DownloadGameAsync(MainWindow mw, string torrentData, CancellationTokenSource cts)
        {
            const int httpListeningPort = 55125;

            EngineSettingsBuilder esb = new EngineSettingsBuilder
            {
                AllowPortForwarding = true,
                AutoSaveLoadDhtCache = true,
                AutoSaveLoadFastResume = true,
                AutoSaveLoadMagnetLinkMetadata = true,
                DhtPort = httpListeningPort,
                ListenPort = httpListeningPort,

                HttpStreamingPrefix = new Uri($"http://127.0.0.1:{httpListeningPort}/")
            };

            // init engine
            ClientEngine engine = new ClientEngine(esb.ToSettings());

            // check the game dir exists
            string gamePath = GameDir;
            if (!Directory.Exists(gamePath))
                Directory.CreateDirectory(gamePath);

            TorrentSettingsBuilder settingsBuilder = new TorrentSettingsBuilder
            {
                MaximumConnections = 60,
            };

            TorrentSettings torrentSettings = settingsBuilder.ToSettings();


            // set magnetlink OR game path
            if (torrentData.IsMagnetLink()) 
            {
                InfoHash hashOfMagnetLink = InfoHash.FromHex(torrentData.GetHashFromMagnetLink());
                MagnetLink link = new MagnetLink(hashOfMagnetLink);
                await engine.AddAsync(link, gamePath, torrentSettings);
            }
            else // is just a path to the .torrent file
            {
                await engine.AddAsync(torrentData, gamePath, torrentSettings);
            }


            // start all torrents
            foreach (TorrentManager manager in engine.Torrents)
            {
                mw.TextBlockUpdates.Text = LanguagesTexts.DescriptionDownloadInProcess;
                await manager.StartAsync();
            }

            // async task of download the game
            await Task.Run(() =>
            {
                double torrentProgress = 0;
                mw.Dispatcher.Invoke(async () => 
                {
                    while (engine.IsRunning)
                    {
                        DownloadInProgressSet(mw);
                        foreach (TorrentManager manager in engine.Torrents)
                        {
                            if (cts.Token.IsCancellationRequested)
                            {
                                DownloadStoppedSet(mw);
                                cts.Token.ThrowIfCancellationRequested();
                                return;
                            }

                            SetProgress(mw, manager.Progress);
                            await Task.Delay(100);

                            // if is complete
                            if (manager.Progress >= 100d)
                            {
                                SetProgress(mw, 100);
                                torrentProgress = 100d;
                                break;
                            }
                        }

                        if (torrentProgress == 100d)
                        {
                            engine.Dispose();
                            return;
                        }
                    }

                    if(torrentProgress == 100d)
                        DownloadCompleteSet(mw);
                });
            }, cts.Token);
        }


        /// <summary>
        /// Sets some fields that indicates that download is in progress
        /// </summary>
        /// <param name="mw"></param>
        private void DownloadInProgressSet(MainWindow mw)
        {
            mw.TextBlockUpdates.Text = LanguagesTexts.DescriptionDownloadInProcess;
            mw.SettingsButton.IsEnabled = false;
            mw.StopDownloadButton.Visibility = Visibility.Visible;
            mw.toGameButton.IsEnabled = false;
            mw.StopDownloadButton.Content = "Stop";
            mw.RefreshCheckConnetionBorderButton.IsEnabled = false;
        }


        /// <summary>
        /// Sets some fields that indicates that download is in stopped
        /// </summary>
        /// <param name="mw"></param>
        private void DownloadStoppedSet(MainWindow mw)
        {
            mw.StopDownloadButton.Content = "Continue";
            mw.TextBlockUpdates.Text = LanguagesTexts.DescriptionDownloadStopped;
            mw.toGameButton.IsEnabled = true;
            mw.SettingsButton.IsEnabled = true;
            mw.RefreshCheckConnetionBorderButton.IsEnabled = true;
        }


        /// <summary>
        /// Sets some fields that indicates that download is in finally complete
        /// </summary>
        /// <param name="mw"></param>
        private void DownloadCompleteSet(MainWindow mw)
        {
            mw.TextBlockUpdates.Text = LanguagesTexts.DescriptionDownloadComplete;
            mw.RefreshCheckConnetionBorderButton.IsEnabled = true;
            mw.toGameButton.IsEnabled = true;
            mw.SettingsButton.IsEnabled = true;
            mw.StopDownloadButton.Visibility = Visibility.Hidden;
            mw.UpdateProgressBar.Value = 100d;
        }


        /// <summary>
        /// Just sets progress to the progress bar of downloading game 
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="progress"></param>
        private void SetProgress(MainWindow mw, double progress)
        {
            mw.SubTextBlockUpdates.Text = progress.ToString("F2") + '%';

            // Создаем анимацию для перемещения значения прогресса
            DoubleAnimation animation = new DoubleAnimation
            {
                From = mw.UpdateProgressBar.Value, // Начальное значение
                To = progress, // Конечное значение
                Duration = TimeSpan.FromSeconds(1) // Длительность анимации
            };

            // Применяем анимацию к свойству Value прогресс-бара
            mw.UpdateProgressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameDir"></param>
        /// <returns><see cref="true"/> if hash in ftp and game are equals</returns>
        public static bool HashIsEqualsFtp(string gameDir)
        {
            string pathFtp = FtpHelper.F_URL + FtpHelper.HashOfGame;
            using Stream st = FtpHelper.GetStreamFileFtp(pathFtp, new NetworkCredential(FtpHelper.F_L_NAME, FtpHelper.F_PW));
            using StreamReader streamReader = new StreamReader(st);
            string data = streamReader.ReadToEnd();
            string hashLocal = "no hash right here now. Back tomorrow ^_^.\r\nAFS GROUP.";

            if (data != hashLocal)
            {
                return false;
            }

            return true;
        }


        /// <returns>Game path</returns>
        public static string GetGameDir(string curPath, string dataDir, string gameDirFile, string jkey)
        {
            Dictionary<string, string> g_dir = Utils.GetAllItemsFromJson<string, string>(curPath + '\\' + dataDir + '\\' + gameDirFile);
            if (g_dir != null)
                return g_dir[jkey];
            return string.Empty;
        }
    }
}
