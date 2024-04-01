using Launcher.BaseClasses;
using Launcher.Classes;
using Microsoft.Win32.SafeHandles;
using SinRpLauncher.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Launcher.Handlers
{
    public enum Languages
    {
        ru, en, ua
    }


    public static class LanguagesNames
    {
        public const string RUNAME = "ru";
        public const string ENNAME = "en";
        public const string UANAME = "ua";
    }


    public static class LanguagesAliases
    {
        public const string RUALIES = "Русский";
        public const string ENALIES = "English";
        public const string UAALIES = "Українська";
    }


    /// <summary>
    /// Class for handle languages in launcher.
    /// <para>By default is russian language.</para>
    /// Words can be get by index from top to bottom(from 0)
    /// </summary>
    public class LanguagesHandler : BaseHandler
    {

        public const string ext = ".slp";


        private Languages _Lang = Languages.ru;
        public Languages Lang { get { return _Lang; } set { _Lang = value; } }


        private List<string> _Words;
        public List<string> Words { get { return _Words; } private set { _Words = value; } }


        public LanguagesHandler(Languages language)
        {

            _Words = new List<string>()  // INITIALIZE NEED FOR MVVM
            {
            // value                            index
               "Обновить",                      // 0
               "Все обновления установлены",    // 1
               "Требуется обновление",          // 2
               "Нет подключения к интернету",   // 3
               "Игра не установлена",           // 4
               "Профили",                       // 5
               "Кабинет",                       // 6
               "Форум",                         // 7
               "Техподдержка",                  // 8
               "Введите ваш ник",               // 9
               "Играть",                        // 10
               "Отмена",                        // 11
               "Удалить",                       // 12
               "Добавить",                      // 13
               "Выбрать",                       // 14
               "Ваши профили",                  // 15
               "Применить",                     // 16
               "Подтвердить",                   // 17
               "Передний фон",                  // 18
               "Задний фон",                    // 19
               "Загрузка лаунчера...",          // 20
               "Проверяем файлы игра...",       // 21
               "Да",                            // 22
               "Нет",                           // 23
               "Имя профиля",                   // 24
               "Сервер",                        // 25
               "НикНейм",                       // 26
               "Игроков",                       // 27
            };

            Lang = language;
            string p = GetLangPackageFilePath(language);
            SetWords(p);
        }


        private void SetWords(string langPackPathFile)
        {
            string lp = langPackPathFile; // rename
            if(File.Exists(lp))
            {
                _Words = File.ReadAllText(lp).Split(',').ToList<string>();
                for (int i = 0; i < Words.Count(); i++)
                {
                    _Words[i] = _Words[i].Trim();
                }
            }
        }


        private string GetLangPackageFilePath(Languages language)
        {
            string path = GetLangPackPathByName(LanguagesNames.RUNAME);
            Languages l = language; // rename
            switch (l)
            {
                case Languages.ru:
                    path = GetLangPackPathByName(LanguagesNames.RUNAME);
                    break;
                case Languages.en:
                    path = GetLangPackPathByName(LanguagesNames.ENNAME);
                    break;
                case Languages.ua:
                    path = GetLangPackPathByName(LanguagesNames.UANAME);
                    break;
            }

            return path;
        }

        
        private string GetLangPackPathByName(string name)
        {
            name = name.Trim().ToLower();
            return InfoClass.CurrentDir + '\\' + PathRoots.TextDirectory + '\\' + name + ext;
        }


        public static Languages GetLangByName(string lang_name)
        {
            lang_name = lang_name.Trim().ToLower();
            switch (lang_name)
            {
                case LanguagesNames.RUNAME:
                    return Languages.ru;
                case LanguagesNames.ENNAME:
                    return Languages.en;
                case LanguagesNames.UANAME:
                    return Languages.ua;
            }

            return Languages.ru;
        }


        public static string GetNameByLangEnum(Languages language)
        {
            string lStr = string.Empty;
            switch (language)
            {
                case Languages.ru:
                    lStr = LanguagesNames.RUNAME;
                    break;
                case Languages.en:
                    lStr = LanguagesNames.ENNAME;
                    break;
                case Languages.ua:
                    lStr = LanguagesNames.UANAME;
                    break;
                default:
                    lStr = LanguagesNames.RUNAME;
                    break;
            }

            return lStr;
        }


        public static void ChangeInterfaceLanguage(Languages lang)
        {
            InfoClass.InterfaceLang = lang;
        }


        public static Dictionary<string, string> GetDictAlies()
        {
            Dictionary<string, string> l = new Dictionary<string, string>
            {
                { LanguagesAliases.ENALIES,      LanguagesNames.ENNAME },
                { LanguagesAliases.RUALIES,      LanguagesNames.RUNAME },
                { LanguagesAliases.UAALIES,      LanguagesNames.UANAME },
            };

            return l;
        }


        public static string GetLanguageNameByAlies(string alies)
        {
            alies = alies.Trim();
            Dictionary<string, string> l = LanguagesHandler.GetDictAlies();

            return l[alies];
        }


        public static List<string> GetLanguageWords(Languages lang)
        {
            return new LanguagesHandler(lang).Words;
        }


        /// <summary>
        /// Change language for MainWindow
        /// <para>Need for change language without restart the launcher</para>
        /// </summary>
        /// <param name="mw"></param>
        public static void SetLanguageToMainWindow(MainWindow mw, Languages? language = null)
        {
            if(language == null)
            {
                language = InfoClass.InterfaceLang;
            }

            List<string> words = LanguagesHandler.GetLanguageWords((Languages)language);

            mw.toGameButton.Content = words[10];
            mw.SocialMediaText.Text = words[41];
            mw.NavBarCabinetButton.Content = words[6];
            mw.NavBarForumButton.Content = words[7];
            mw.NavBarTechSupportButton.Content = words[8];
        }
    }
}
