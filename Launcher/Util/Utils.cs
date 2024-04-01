using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Net.NetworkInformation;
using Launcher.BaseClasses;
using System.Runtime.InteropServices;
using Launcher.Models;
using Launcher.Handlers;
using System.Threading.Tasks;
using CDialogWindow;
using SinRpLauncher.Util;
using Launcher.Extentions;
using Path = System.IO.Path;

namespace Launcher.Classes
{
    public class Utils : MainBaseClass
    {
        static private BrushConverter converterColor;
        static private SolidColorBrush? simpleToggleColor;
        static private SolidColorBrush? _SelectedToggleColor;

        static public SolidColorBrush SelectedToggleColor;


        static Utils()
        {
            converterColor = new BrushConverter();
            simpleToggleColor = (SolidColorBrush?)converterColor.ConvertFrom("#9c9a9a");
            _SelectedToggleColor = (SolidColorBrush?)converterColor.ConvertFrom("#ffd900");

            SelectedToggleColor = _SelectedToggleColor ?? Brushes.White;
        }


        /// <summary>
        /// Start proccess for url. Open browser with url
        /// </summary>
        /// <param name="url"></param>
        public static void OpenUrl(string url)
        {
            // opens url (only for windows os)
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }


        /// <summary>
        /// Loads the image by url
        /// </summary>
        /// <param name="img"></param>
        /// <param name="uriStr"></param>
        public static void LoadImageSource(Image img, string uriStr)
        {
            img.BeginInit();
            img.Source = new BitmapImage(new Uri(uriStr)); 
            img.EndInit();
        }


        /// <summary>
        /// Load the images of launcher if has failed images (news)
        /// </summary>
        /// <param name="mw"></param>
        public static void LoadNewsImages(MainWindow mw)
        {
            if (InfoClass.HasFailedNewsImages)
            {
                MainModel _model = new MainModel(); // create model for get news uris
                // loads all images
                Utils.LoadImageSource(mw.NewsImg1, _model.News1);
                Utils.LoadImageSource(mw.NewsImg2, _model.News2);
                Utils.LoadImageSource(mw.NewsImg3, _model.News3);
                Utils.LoadImageSource(mw.NewsImg4, _model.News4);
                Utils.LoadImageSource(mw.NewsImg5, _model.News5);
            }
        }


        /// <summary>
        /// Set new order of news image. Put it to front, and send to back previous. Work by name
        /// </summary>
        /// <param name="mw"></param>
        /// <param name="nameEllipse"></param>
        /// <returns>void</returns>
        /// <exception cref="Exception"></exception>
        public static void SetNewsImage(MainWindow mw, string nameEllipse)
        {
            try
            {
                switch (nameEllipse.ToLower())
                {
                    // Check name of ellipses that we got in parameter.
                    // If we detected it - put order of this to top and send previous to back
                    // Send given news image as a current. For change it in future.
                    case InfoClass.FIRST_NEWS_IMAGE:
                        mw.CurrentNews = SetNewsOrder(mw.CurrentNews, mw.NewsImg1);
                        break;

                    case InfoClass.SECOND_NEWS_IMAGE:
                        mw.CurrentNews = SetNewsOrder(mw.CurrentNews, mw.NewsImg2);
                        break;

                    case InfoClass.THIRD_NEWS_IMAGE:
                        mw.CurrentNews = SetNewsOrder(mw.CurrentNews, mw.NewsImg3);
                        break;

                    case InfoClass.FOUTH_NEWS_IMAGE:
                        mw.CurrentNews = SetNewsOrder(mw.CurrentNews, mw.NewsImg4);
                        break;

                    case InfoClass.FIVTH_NEWS_IMAGE:
                        mw.CurrentNews = SetNewsOrder(mw.CurrentNews, mw.NewsImg5);
                        break;

                    // If we didnt detected anything, put first news as default
                    // Also it help do not got some exception. Especially in start of work program
                    default:
                        int order = Panel.GetZIndex(mw.CurrentNews); // get order of current news img
                        mw.CurrentNews = mw.NewsImg1;
                        Panel.SetZIndex(mw.CurrentNews, order + 10);
                        break;
                }
            }
            catch (Exception)
            {
                throw; // just trust me
            }
            
        }


        /// <summary>
        /// <para>Set old current image order To back.</para> 
        /// <para>Set new image order To top</para>
        /// </summary>
        /// <returns>New current image</returns>
        private static Image SetNewsOrder(Image currNews, Image clickedNews)
        {
            int order = Panel.GetZIndex(currNews); // get order of current news img

            Panel.SetZIndex(currNews, order - 10); // set current to back
            Panel.SetZIndex(clickedNews, order + 10); // set new to top

            currNews = clickedNews; // current news img is a new image already
            return currNews; // return new current news
        }


        /// <summary>
        /// Set color to picked toggle of news. Return previous toggle to simple color.
        /// </summary>
        /// <param name="currentEllipse"></param>
        /// <param name="newCurrentEllipse"></param>
        /// <exception cref="Exception"></exception>
        // ref arg for get it from other class as variable
        public static Ellipse PickNewsToggle(Ellipse currentEllipse, Ellipse newCurrentEllipse)
        {
            try
            {
                if (currentEllipse != null && newCurrentEllipse != null)
                {
                    currentEllipse.Fill = simpleToggleColor;
                    currentEllipse = newCurrentEllipse;
                    currentEllipse.Fill = _SelectedToggleColor;
                }
                // it can be null no one was selected
                else if (currentEllipse == null && newCurrentEllipse != null)
                {
                    currentEllipse = newCurrentEllipse;
                    currentEllipse.Fill = _SelectedToggleColor;
                }
                return currentEllipse ?? new Ellipse();
            }
            catch (Exception)
            {
                throw;
            }
           
        }


        /// <summary>
        /// Method for get url of social media that associate with key from json
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetUrlSocialMedia(string key)
        {
            try 
            {
                // get the json data about social media links
                string jsonLinks = File.ReadAllText(Path.Combine(InfoClass.CurrentDir, 
                    PathRoots.TextDirectory, PathRoots.LinksSocialMediaJson));

                // deserialize to get url
                Dictionary<string, string>? urls = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonLinks);
                if (urls != null)
                {
                    return urls[key]; // get url from json by given key
                }
                else
                {
                    throw new NullReferenceException("Json linkssocialmedia got null");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Redirect to link for social media that gets from json. Find it from name of button 
        /// </summary>
        /// <param name="buttonName"></param>
        /// <exception cref="Exception"></exception>
        public static void RedirectToSocialMedia(string buttonName)
        {
            try
            {
                const string YOUTUBE_JSON_KEY = "youtube";
                const string VK_JSON_KEY = "vk";
                const string TEST_JSON_KEY = "test";

                string jkey; // key for json file
                switch (buttonName)
                {
                    case "NavBarCabinetButton":
                        jkey = TEST_JSON_KEY;
                        Utils.OpenUrl(Utils.GetUrlSocialMedia(jkey)); // open url with argument get link from json file
                        break;
                    case "NavBarForumButton":
                        jkey = TEST_JSON_KEY; 
                        Utils.OpenUrl(Utils.GetUrlSocialMedia(jkey)); // open url with argument get link from json file
                        break;
                    case "NavBarTechSupportButton":
                        jkey = TEST_JSON_KEY; 
                        Utils.OpenUrl(Utils.GetUrlSocialMedia(jkey)); // open url with argument get link from json file
                        break;
                    case "DiscordButton":
                        MessageBox.Show("На дискорд пока ссылки нет");
                        break;
                    case "YoutubeButton":
                        jkey = YOUTUBE_JSON_KEY; 
                        Utils.OpenUrl(Utils.GetUrlSocialMedia(jkey)); // open url with argument get link from json file
                        break;
                    case "VkButton":
                        jkey = VK_JSON_KEY; 
                        Utils.OpenUrl(Utils.GetUrlSocialMedia(jkey)); // open url with argument get link from json file
                        break;
                    default:
                        DialogWindow cdw = new DialogWindow();
                        cdw.ShowCustomDialogWindow("Error", "Что то пошло не так", DialogWindowButtons.Ok);
                        break;
                }  
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Picked language inteface from settings json</returns>
        public static Languages GetIntefaceLanguage()
        {
            string jpath = Path.Combine(InfoClass.CurrentDir, PathRoots.DataDirectory, 
                PathRoots.LaunchSettingsFile);
            Dictionary<string, string> settings = Utils.GetAllItemsFromJson<string, string>(jpath);
            if (settings != null)
            {
                string lang = settings["Lang"].ToLower();
                return LanguagesHandler.GetLangByName(lang);
            }

            return Languages.ru;
        }


        public static void WriteLaunchSettings(string path, Languages interfaceLang)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            string langParam = LanguagesHandler.GetNameByLangEnum(interfaceLang);

            settings.Add("Lang", langParam);

            string json = JsonSerializer.Serialize(settings);

            File.WriteAllText(path, json);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyStr"></param>
        /// <returns>Key value by string with name of it</returns>
        public static Key GetKeyByStringName(string keyStr)
        {
            return (Key)Enum.Parse(typeof(Key), keyStr);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns>String of key value from key enum</returns>
        public static string GetStringNameByKey(Key key)
        {
            return Enum.GetName(typeof(Key), key)?.Trim() ?? "R";
        }


        /// <summary>
        /// Do ping test to 1.1.1.1 ip. If success - return true, else - false
        /// </summary>
        /// <returns></returns>
        public static bool userHasInternetConnection()
        {
            try
            {
                Ping myPing = new Ping();
                string host = "1.1.1.1";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// return error log file path from base class
        /// </summary>
        /// <returns></returns>
        public string GetErrorLogPath()
        {
            try
            {
                return base.errLogPath;
            }
            catch
            {
                return "null";
            }
        }


        /// <summary>
        /// return simple log path from base class
        /// </summary>
        /// <returns></returns>
        public string GetAllLogPath()
        {
            try
            {
                return base.allLogPath;
            }
            catch
            {
                return "null";
            }
        }


        /// <summary>
        /// Method for get value from property of some class(type)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static string GetValueFromProp(object sender, string propName)
        {
            string propVal = sender.GetType().GetProperty(propName)?.GetValue(sender)?.ToString() ?? String.Empty;
            return propVal;
        }


        /// <summary>
        /// </summary>
        /// <returns>Color converter</returns>
        public static ColorConverter GetColorConverter()
        {
            return new ColorConverter();
        }

        /// <summary>
        /// </summary>
        /// <returns>Brush converter</returns>
        public static BrushConverter GetBrushConverter()
        {
            return new BrushConverter();
        }


        /// <summary>
        /// Set status of launcher statement. Change description and color of connection/update checker
        /// </summary>
        public static void MWSetLauncherStatus(MainWindow mw, Brush CheckerColor, string Descr, string subDescr)
        {
            mw.UpdateAndInternetIndicator.Fill = CheckerColor;
            mw.TextBlockUpdates.Text = Descr;
            mw.SubTextBlockUpdates.Text = subDescr;
        }


        public static Dictionary<TKey, TValue> GetAllItemsFromJson<TKey, TValue>(string path)
        {
            try
            {
                string jsonString = File.ReadAllText(path);
                Dictionary<TKey, TValue>? items = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(jsonString);
                return items ?? new Dictionary<TKey, TValue>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns>Path to profile of user in launcher</returns>
        public static string GetPathToJsonProfs()
        {
            return Path.Combine(InfoClass.CurrentDir, PathRoots.DataDirectory, PathRoots.ProfilesFile);
        }


        /// <summary>
        /// Convert the decimal byte number to the hex string 
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        public static string DecimalToHexByte(byte dec)
        {
            string hex = "";

            // convert byte to hexdecimal system
            string format = "X"; // hexdecimal format
            hex = dec.ToString(format);

            if(dec < 15)
            {
                hex = StringHelper.PushFront(hex, '0');
            }

            return hex;
        }


        /// <summary>
        /// Check for issues with hex code of color and repair it
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>string that represent hex format color(fixed)</returns>
        public static string FixHexCodeColor(string hex)
        {
            const char hexc = '#';        // first hex color char
            const int maxSize = 9;  // max size of hex color
            string def = "#000000"; // default value of hex color
            int cnt = hex.Length;

            if (string.IsNullOrEmpty(hex))
                hex = def; // if is null or empty -> set it to default color

            if(hex.Contains(hexc) == false) // if hex color doesnt contains '#'
            {
                hex = hexc + hex;
            }
            else if(hex.Contains(hexc) == true && hex[0] != hexc) // if it contains '#' but it not first
            {
                hex = StringHelper.ReplaceAllChars(hex, hexc, '0'); // just replace '#' by '0'
                hex = StringHelper.ReplaceFirst(hex, hexc); // put # to first
            }
            else if(hex.Contains(hexc) == true && hex[0] == hexc && StringHelper.CountChar(hex, hexc) > 1)
            {
                hex = StringHelper.ReplaceAllChars(hex, hexc, '0'); // just replace '#' by '0'
                hex = StringHelper.ReplaceFirst(hex, hexc); // put # to first
            }

            cnt = hex.Length;
            if(cnt % 2 == 0) // standart hex decimal color doesnt divisions by 2
            {
                hex += '0'; // just add '0' for fix size of hex color
            }

            if(hex.Length > maxSize) // it must be equals 9 as maximum
            {
                hex = StringHelper.CutStringSize(hex, maxSize);
            }

            return hex; // return fixed hex format color
        }


        /// <summary>
        /// Checks if is correct hex color string format. Like #0000BBAA
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static bool IsCorrectHexColor(string hex)
        {
            char hexc = '#';
            if (string.IsNullOrEmpty(hex))
                return false;

            if (hex.Contains(hexc) == false) // if hex color doesnt contains '#'
                return false;
            else if (hex.Contains(hexc) == true && hex[0] != hexc) // if it contains but it not first
                return false;

            int cntHexChars = StringHelper.CountChar(hex, hexc); // count '#' in hex color
            if (cntHexChars > 1 || cntHexChars <= 0) // it must be only 1 per color
                return false;

            int lenghtOfHexString = hex.Length; // c = lenght of string
            if (lenghtOfHexString % 2 == 0) // standart hex decimal color doesnt divisions by 2
                return false;

            return true;
        } 


        /// <summary>
        /// Fix hex color code if is incorrect
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>Hex color code</returns>
        public static string FixHexColorCodeIfIncorrect(string hex)
        {
            if(IsCorrectHexColor(hex) == false)
            {
                hex = FixHexCodeColor(hex);
            }
            return hex;
        }


        public static void TaskTimeIsTimedOut(Task task, uint delay)
        {
            if (!task.Wait(TimeSpan.FromMilliseconds(delay)))
                throw new Exception("Timed out");
            return;
        }


        /// <summary>
        /// Formats the datetime by special format
        /// </summary>
        /// <param name="t"></param>
        /// <returns>Formated date string</returns>
        public static string FormatDateTime(DateTime t)
        {
            return t.ToString("dd-MM-yyyy HH:mm:ss.fff");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="curProcName"></param>
        /// <param name="curProcId"></param>
        /// <returns>True if process with the same name, but not same id is started</returns>
        public static bool SameProcessStarted(string curProcName, int curProcId)
        {
            foreach (var proc in Process.GetProcesses())
            {
                if (proc.ProcessName == curProcName && proc.Id != curProcId)
                    return true; // if found
            }

            return false; // if not found
        }


        /// <summary>
        /// Generate random hex code by capacity
        /// </summary>
        /// <param name="capacity"></param>
        /// <returns>Random hex string code</returns>
        public static string GenerateHEXCode(uint capacity)
        {
            List<int> hexInt = new List<int>();
            Random r = new Random();
            for (int i = 0; i < capacity; i++)
            {
                hexInt.Add(r.Next(0, 255));
            }
            string hex = "";
            hex += "0x";
            for (int i = 0; i < capacity; i++)
            {
                hex += $"{hexInt[i]:X}";
            }

            return hex;
        }


        /// <summary>
        /// Writes serial number to launcher metadata
        /// </summary>
        public static void WriteSerialNumber()
        {
            //if (!InfoClass.UserHasConnection) return;

            string path = Path.Combine(InfoClass.CurrentDir, PathRoots.DataDirectory, PathRoots.SerialNumberFile);
            if (!File.Exists(path))
            {
                using Stream s = File.Create(path);
            }

            File.WriteAllText(path, GenerateHEXCode(10));
        }


        /// <summary>
        /// Get the ushort from the dll pointer of c++
        /// </summary>
        /// <param name="ptr"></param>
        /// <returns></returns>
        public static ushort[] GetUncypher(IntPtr ptr)
        {
            short len = Marshal.ReadInt16(ptr); // read lenght of the array
            ptr = IntPtr.Add(ptr, 2); // move to the next short int
            ushort[] resultShorts = new ushort[len]; 

            for (int i = 0; i < len; i++)
            {
                resultShorts[i] = (ushort)Marshal.ReadInt16(ptr); // read the short int
                ptr = IntPtr.Add(ptr, 2); // move to the next short int
            }
           
            return resultShorts; // return the array data
        }


        /// <returns>Dialog window with set language for it</returns>
        public static DialogWindow GetPrepairedDialogBox()
        {
            DialogWindow? msgBox = new DialogWindow();
            msgBox.SetLanguage(InfoClass.InterfaceLang);
            return msgBox;
        }


        /// <summary>
        /// Shows the exception full message if is not debug mode
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowMessageBoxErrorIfNotDebug(Exception ex)
        {
            if(InfoClass.IsDebugMode)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// Shows the exception full message if is not debug mode
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowCustomMessageBoxErrorIfNotDebug(Exception ex)
        {
            if (InfoClass.IsDebugMode)
            {
                DialogWindow dialogWindow = new DialogWindow();
                dialogWindow.SetLanguage(InfoClass.InterfaceLang);
                dialogWindow.ShowCustomDialogWindow(ex.ToString(), "", DialogWindowButtons.Ok);
            }
        }
    }
}
