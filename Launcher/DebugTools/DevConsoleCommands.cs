

using Launcher;
using System.Collections.Generic;
using System.Windows;

namespace SinRpLauncher.Debug
{

    public class DevConsoleCommands
    {
        public const string SPAWN_CMD = "sp";
        public const string DELETE_CMD = "del";
        public const string HIDE_CMD = "hide";
        public const string SHOW_CMD = "show";
        public const string SHUTDOWN_CMD = "shtdn";
        public const string HELP_CMD = "help";
        public const string CHANGETORRENT_CMD = "chtr";
        public const string OPEN_VIM = "vim";


        public static Dictionary<string, string> Commands 
        { 
            get 
            { 
                return new Dictionary<string, string>()
                {
                    { "spawn", SPAWN_CMD },
                    { "delete", DELETE_CMD },
                    { "hide", HIDE_CMD },
                    { "show", SHOW_CMD },
                    { "shutdown", SHUTDOWN_CMD},
                    { "help", HELP_CMD },
                    { "changetorrent", CHANGETORRENT_CMD },
                    { "vim",  OPEN_VIM }
                }; 
            } 
        }


        public static Dictionary<string, FrameworkElement> Controls(MainWindow mw) 
        {
            return new Dictionary<string, FrameworkElement>()
                {
                    { "togamebtn", mw.toGameButton },

                    { "nicknametextbox", mw.NickNameBox },

                    { "closebtn", mw.CloseButton },

                    { "minbtn", mw.MinimizeButton },

                    { "logo1", mw.ProjectLabel },

                    { "logo2", mw.SubProjectLabel },

                    { "dsbtn", mw.DiscordButton },
                    
                    { "vkbtn", mw.VkButton},

                    { "ytbtn", mw.YoutubeButton },

                    { "sv1", mw.Server_1 },

                    { "sv2", mw.Server_2 },

                    { "refrbtn", mw.RefreshCheckConnetionBorderButton },

                    { "setsbtn", mw.SettingsButton },

                    //{ "profsbtn", mw.NavBarUserProfilesButton },

                    //{ "profsimg", mw.ProfilesImg },

                    { "cabbtn", mw.NavBarCabinetButton },

                    { "cabimg", mw.CabinetImg },

                    { "techsupbtn", mw.NavBarTechSupportButton },

                    { "techsupimg", mw.TechSupportImg },

                    { "forumbtn", mw.NavBarForumButton },

                    { "forumimg", mw.ForumImg },

                    { "stopdwnbtn", mw.StopDownloadButton },

                    { "progressbar", mw.UpdateProgressBar },

                    { "progresstxt", mw.TextBlockUpdates },

                    { "subprogresstxt", mw.SubTextBlockUpdates },

                    { "updindicator", mw.UpdateAndInternetIndicator },
                };
        }
    }
}
