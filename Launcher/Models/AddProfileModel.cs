using System.Collections.Generic;
using System.IO;
using Launcher.Classes;
using System.Text.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Launcher.Handlers;
using SinRpLauncher.Util;

namespace Launcher.Models
{
    internal class AddProfileModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static readonly string _CURRENT_DIR = InfoClass.CurrentDir;

        static private List<string> LanguageText = LanguagesTexts.AllWords;

        private string _AddProfileButtonText    = LanguageText[13];
        private string _ProfNameFieldText       = LanguageText[24] + ":";
        private string _ServerFieldText         = LanguageText[25] + ":";
        private string _NickNameFieldText       = LanguageText[26] + ":";

        public string AddProfileButtonText  { get { return _AddProfileButtonText; } set { _AddProfileButtonText = value; OnPropertyChanged(); } }
        public string ProfNameFieldText     { get { return _ProfNameFieldText; } set { _ProfNameFieldText = value; OnPropertyChanged(); } }
        public string ServerFieldText       { get { return _ServerFieldText; } set {_ServerFieldText = value; OnPropertyChanged(); } }
        public string NickNameFieldText     { get { return _NickNameFieldText; } set { _NickNameFieldText = value; OnPropertyChanged(); } }


        public AddProfileModel() { ResetModel(); }


        public string[] SourceServers()
        {
            Dictionary<string, string[]> raw_servers = GetAllItemsFromJsonList(_CURRENT_DIR + "\\" + PathRoots.DataDirectory + "\\" + PathRoots.ServersDataFile);
            string allServersKey = "AllServers";
            string[] fil_servers = raw_servers[allServersKey];
            return fil_servers;
        }


        private static Dictionary<string, string[]> GetAllItemsFromJsonList(string path)
        {
            string jsonString = File.ReadAllText(path);
            Dictionary<string, string[]>? items = JsonSerializer.Deserialize<Dictionary<string, string[]>>(jsonString);
            return items;
        }


        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private void ResetModel()
        {
            LanguageText = LanguagesTexts.AllWords;
            AddProfileButtonText   = LanguageText[13];
            ProfNameFieldText      = LanguageText[24] + ":";
            ServerFieldText        = LanguageText[25] + ":";
            NickNameFieldText      = LanguageText[26] + ":";
        }  
    }      
}          
