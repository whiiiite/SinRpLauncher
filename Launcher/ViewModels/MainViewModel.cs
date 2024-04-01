using SinRpLauncher;
using System.Collections.Generic;
using System.ComponentModel;

namespace Launcher.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        Models.MainModel _model = new Models.MainModel();
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public MainViewModel() { }


        public string UpdateButtonText              { get { return _model.UpdateButtonText; } }
        public string PlaceHolderNickNameBox        { get { return _model.PlaceHolderNickNameBox; } }
        public string NewsImg1                      { get { return _model.News1; } }
        public string NewsImg2                      { get { return _model.News2; } }
        public string NewsImg3                      { get { return _model.News3; } }
        public string NewsImg4                      { get { return _model.News4; } }
        public string NewsImg5                      { get { return _model.News5; } }
        public string YouTubeIcon                   { get { return _model.YouTubeIcon; } }
        public string DiscordIcon                   { get { return _model.DiscordIcon; } }
        public string VkIcon                        { get { return _model.VkIcon; } }
        public string GamepadIcon                   { get { return _model.GamepadIcon; } }
        public string RefreshIcon                   { get { return _model.RefreshIcon; } }
        public string SettingsIcon                  { get { return _model.SettingsIcon; } }
        public string WaitRefreshIcon               { get { return _model.WaitRefreshIcon; } }
        public string FolderIcon                    { get { return _model.FolderIcon; } }
        public string MainBGImg                     { get { return _model.MainBGImg; } }
        public string PersonNickanemIng             { get { return _model.PersonNicknameImg; } }
        public string SinProjLabelImg               { get { return _model.SinProjLabelImg; } }
        public string WebsiteIcon                   { get { return _model.WebsiteIcon; } }
        public string HelpIcon                      { get { return _model.HelpIcon; } }
        public string CabinetIcon                   { get { return _model.CabinetIcon; } }
        public string ProfilesIcon                  { get { return _model.ProfilesIcon; } }
        public string ServerContainerImgBG          { get { return _model.ServerContainerImgBG; } }
        public string PlayersIcon                   { get { return _model.PlayersIcon; } }

        public string TextBlockUpdatesText          { get { return _model.TextBlockUpdatesText; } }
        public string ToGameButtonText              { get { return _model.ToGameButtonText; } }
        public string NavBarUserProfilesButtonText  { get { return _model.NavBarUserProfilesButtonText; } }
        public string NavBarTechSupportButtonText   { get { return _model.NavBarTechSupportButtonText; } }
        public string NavBarForumButtonText         { get { return _model.NavBarForumButtonText; } }
        public string NavBarCabinetButtonText       { get { return _model.NavBarCabinetButtonText; } }
        public string PlayersOnServerText           { get { return _model.PlayersOnServerText; } }
        public string SocialMediaText               { get { return _model.SocialMediaText; } }
        public string LabelProjectNameText          { get { return _model.LabelProjectText; } }
        public string SubLabelProjectText           { get { return _model.SubLabelProjectText; } }
        public string VerN                          { get { return _model.VerN; } }
        public string GamePath                      { get { return _model.GamePath; } }
        public string[] SourceArrayServers          => _model.ServersSource();
    }
}
