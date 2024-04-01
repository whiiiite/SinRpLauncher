using Launcher.Classes;
using Launcher.Handlers;
using SinRpLauncher.Util;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Launcher.Models
{
    public class SettingsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly static private string _CURRENT_PATH = InfoClass.CurrentDir;
        readonly static private string _GamePath = GameFilesSystem.GameDir;

        static private List<string> LanguageText = LanguagesTexts.AllWords;
        private string _SubmitBtnText = LanguageText[16];
        private string _CancelBtnText = LanguageText[11];

        readonly private Dictionary<string, Dictionary<string, string>> _AllPictures 
            = Utils.GetAllItemsFromJson<string, Dictionary<string, string>>(_CURRENT_PATH + '\\' + 
                PathRoots.TextDirectory + '\\' + PathRoots.ImagesFile);

        public SettingsModel() { ResetModel(); }

        public string FolderIcon    => _CURRENT_PATH + _AllPictures["folderIcon"]["uri"];
        public string GamePath      => _GamePath;
        public string MainBGImg     => _CURRENT_PATH + _AllPictures["main_bg"]["uri"];


        public string[] LangList => GetLangList(InfoClass.CurrentDir + '\\' + PathRoots.TextDirectory + '\\' + PathRoots.LangListFile);

        public string SubmitBtnText { get { return _SubmitBtnText; } set { _SubmitBtnText = value; OnPropertyChanged(); } }
        public string CancelBtnText { get { return _CancelBtnText; } set { _CancelBtnText = value; OnPropertyChanged(); } }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        private string[] GetLangList(string path)
        {
            string[] langList = File.ReadAllText(path).Split(',');
            for (int i = 0; i < langList.Length; i++)
            {
                langList[i] = langList[i].Trim();
            }
            return langList;
        }


        private void ResetModel()
        {
            LanguageText = LanguagesTexts.AllWords;
        }
    }
}
