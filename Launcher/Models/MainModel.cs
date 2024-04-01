using System.Collections.Generic;
using Launcher.Classes;
using Launcher.Handlers;
using SinRpLauncher;
using SinRpLauncher.Util;

namespace Launcher.Models
{
    internal class MainModel
    {

        readonly static private string _CURRENT_PATH = InfoClass.CurrentDir;
        readonly static private string _GamePath = GameFilesSystem.GameDir;

        readonly static private List<string> LanguageText = LanguagesTexts.AllWords;

        readonly private Dictionary<string, Dictionary<string,string>> _AllPictures = Utils.GetAllItemsFromJson<string, Dictionary<string, string>>(_CURRENT_PATH + '\\' + PathRoots.TextDirectory + '\\' + PathRoots.ImagesFile);
        
        #region private strings for text controls
        readonly private string _UpdateButtonText               = LanguagesTexts.UpdateWord;
        readonly private string _PlaceHolderNickNameBox         = "\n" + LanguageText[9];
        readonly private string _TextBlockUpdatesText           = LanguagesTexts.SubDescriprtionHasInternetAndAllUpdates;
        readonly private string _ToGameButtonText               = LanguageText[10];
        readonly private string _NavBarUserProfilesButtonText   = LanguagesTexts.ProfilesWord;
        readonly private string _NavBarCabinetButtonText        = LanguagesTexts.CabinetWord;
        readonly private string _NavBarForumButtonText          = LanguagesTexts.ForumWord;
        readonly private string _NavBarTechSupportButtonText    = LanguagesTexts.TechSupWord;
        readonly private string _PlayersOnServerText            = LanguageText[27];
        readonly private string _SocialMediaText                = LanguageText[41];
        readonly private string _LabelProjectText               = "SIN";
        readonly private string _SubLabelProjectText            = "PROJECT";
        readonly private string _VerN                           = InfoClass.curVer;
        #endregion


        #region public strings for viewmodel
        public string News1 => _AllPictures["news1"]["uri"];
        public string News2 => _AllPictures["news2"]["uri"];
        public string News3 => _AllPictures["news3"]["uri"];
        public string News4 => _AllPictures["news4"]["uri"];
        public string News5 => _AllPictures["news5"]["uri"];
        public string YouTubeIcon => _CURRENT_PATH + _AllPictures["youtubeIcon"]["uri"];
        public string DiscordIcon => _CURRENT_PATH + _AllPictures["discordIcon"]["uri"];
        public string VkIcon => _CURRENT_PATH + _AllPictures["vkIcon"]["uri"];
        public string GamepadIcon => _CURRENT_PATH + _AllPictures["gamepadIcon"]["uri"];
        public string RefreshIcon => _CURRENT_PATH + _AllPictures["refreshIcon"]["uri"];
        public string WaitRefreshIcon => _CURRENT_PATH + _AllPictures["waitRefreshIcon"]["uri"];
        public string SettingsIcon => _CURRENT_PATH + _AllPictures["settingsIcon"]["uri"];
        public string FolderIcon => _CURRENT_PATH + _AllPictures["folderIcon"]["uri"];
        public string MainBGImg => _CURRENT_PATH + _AllPictures["main_bg"]["uri"];
        public string PersonNicknameImg => _CURRENT_PATH + _AllPictures["pers_nickname_img"]["uri"];
        public string SinProjLabelImg => _CURRENT_PATH + _AllPictures["sin_proj_lb"]["uri"];
        public string WebsiteIcon => _CURRENT_PATH + _AllPictures["website_icon"]["uri"];
        public string HelpIcon => _CURRENT_PATH + _AllPictures["help_icon"]["uri"];
        public string CabinetIcon => _CURRENT_PATH + _AllPictures["cabinet_icon"]["uri"];
        public string ProfilesIcon => _CURRENT_PATH + _AllPictures["profiles_icon"]["uri"];
        public string ServerContainerImgBG => _CURRENT_PATH + _AllPictures["server_container_bg"]["uri"];
        public string PlayersIcon => _CURRENT_PATH + _AllPictures["players_icon"]["uri"];

        public string UpdateButtonText => _UpdateButtonText;
        public string PlaceHolderNickNameBox => _PlaceHolderNickNameBox;
        public string TextBlockUpdatesText => _TextBlockUpdatesText;
        public string ToGameButtonText => _ToGameButtonText;
        public string NavBarUserProfilesButtonText => _NavBarUserProfilesButtonText;
        public string NavBarTechSupportButtonText => _NavBarTechSupportButtonText;
        public string NavBarForumButtonText => _NavBarForumButtonText;
        public string NavBarCabinetButtonText => _NavBarCabinetButtonText;
        public string PlayersOnServerText => _PlayersOnServerText;
        public string SocialMediaText => _SocialMediaText;
        public string LabelProjectText => _LabelProjectText;
        public string SubLabelProjectText => _SubLabelProjectText;
        public string VerN => _VerN;
        public string GamePath => _GamePath;
        public static string CURRENT_PATH => _CURRENT_PATH;

        public string[] ServersSource()
        {
            List<ServerContainer> servsList = new List<ServerContainer>();
            Dictionary<string, string[]> raw_servers = Utils.GetAllItemsFromJson<string, string[]>(_CURRENT_PATH + "\\" + PathRoots.DataDirectory + "\\" + PathRoots.ServersDataFile);
            string allServersKey = "AllServers";
            string[] fil_servers = raw_servers[allServersKey];
            
            return fil_servers;
        }
        #endregion
    }
}
